using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

/* Flags

0000 0001	Docked, (on a landing pad)
0000 0002	Landed, (on planet surface)
0000 0004	Landing Gear Down
0000 0008	Shields Up
0000 0010	Supercruise
0000 0020	FlightAssist Off
0000 0040	Hardpoints Deployed
0000 0080	In Wing
0000 0100	LightsOn
0000 0200	Cargo Scoop Deployed
0000 0400	Silent Running,
0000 0800	Scooping Fuel
0000 1000	Srv Handbrake
0000 2000	Srv using Turret view
0000 4000	Srv Turret retracted (close to ship)
0000 8000	Srv DriveAssist
0001 0000	Fsd MassLocked
0002 0000	Fsd Charging
0004 0000	Fsd Cooldown
0008 0000	Low Fuel ( < 25% )
0010 0000	Over Heating ( > 100% )
0020 0000	Has Lat Long
0040 0000	IsInDanger
0080 0000	Being Interdicted
0100 0000	In MainShip
0200 0000	In Fighter
0400 0000	In SRV
0800 0000	Hud in Analysis mode
1000 0000	Night Vision
2000 0000	Altitude from Average radius
4000 0000	fsdJump
8000 0000	srvHighBeam

*/

namespace EDExplorer
{
    public class StatusMonitor
    {
        private readonly FileSystemWatcher statusWatcher;
        public string CurrentLogPath { get; private set; }
        private string StatusDirectory;
        private string StatusName = "Status.json";
        public StatusMonitor()
        {
            StatusDirectory = Properties.Settings.Default.JournalPath;
            //LogDirectory = CheckLogPath();
            //Properties.Settings.Default.JournalPath = LogDirectory;
            //Properties.Settings.Default.Save();

            // Examina cambios producidos en el directorio indicado
            statusWatcher = new FileSystemWatcher(StatusDirectory, StatusName)
            {
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName
            };
            statusWatcher.Changed += StatusChanged;
        }

        public void MonitorStart()
        {
            if (!IsMonitoring())
            {
                statusWatcher.EnableRaisingEvents = true;
            }
        }

        public void MonitorStop()
        {
            if (IsMonitoring())
            {
                statusWatcher.EnableRaisingEvents = false;
            }
        }
        public bool IsMonitoring()
        {
            return statusWatcher.EnableRaisingEvents;
        }

        private string CheckLogPath()
        {
            StatusDirectory = string.IsNullOrEmpty(StatusDirectory) ? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Saved Games\\Frontier Developments\\Elite Dangerous" : StatusDirectory;
            if (!Directory.Exists(StatusDirectory) || new DirectoryInfo(StatusDirectory).GetFiles(Properties.Settings.Default.JournalName).Count() == 0)
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
                {
                    RootFolder = Environment.SpecialFolder.MyComputer,
                    ShowNewFolderButton = false,
                    Description = "Seleccionar la carpeta de Elite Dangerous Journal"
                };
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    StatusDirectory = folderBrowserDialog.SelectedPath;
                }
            }
            return StatusDirectory;
        }

        private void StatusChanged(object source, FileSystemEventArgs e)
        {
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Created:
                    CurrentLogPath = e.FullPath;
                    break;

                case WatcherChangeTypes.Changed:
                    if (CurrentLogPath != e.FullPath)
                    {
                        CurrentLogPath = e.FullPath;
                    }

                    string linea;

                    using (StreamReader currentLog = new StreamReader(File.Open(CurrentLogPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                    {
                        linea = currentLog.ReadLine();
                    }

                    ProcessEvent(linea);
                    
                    break;

                default:
                    break;
            }
        }

        private void ProcessEvent(string logLine)
        {
            // Proteccion contra lineas incompletas
            if (logLine != null && logLine.Trim().StartsWith("{") && logLine.Trim().EndsWith("}"))
            {
                try
                {
                    JObject lastEvent = (JObject)JsonConvert.DeserializeObject(logLine, new JsonSerializerSettings() { DateParseHandling = DateParseHandling.None });

                    /////////////////////////////////////////
                    StatusEntry.Invoke(this, EventArgs.Empty);
                    ////////////////////////////////////////
                }
                catch (Exception ex)
                {
                    DialogResult response = MessageBox.Show("Ha ocurrido un error en ProcessLine. ¿Quiere ver información de Detalle adicional?", "Error Procesando Linea", MessageBoxButtons.YesNo);
                    if (response == DialogResult.Yes)
                    {
                        MessageBox.Show($"Status.json\r\nLinea: {logLine}\r\nException message: {ex.Message}\r\n\r\nStack trace: {ex.StackTrace}", "Detalle del Error", MessageBoxButtons.OK);
                    }
                }
            }
        }

        public event EventHandler StatusEntry;

        public struct Coordinate
        {
            public double x, y, z;
        }
        public Coordinate grados(double lat, double lon, double alt, double R = 6378137)
        {
            // Para convertir lat / lon / alt (lat en grados norte, lon en grados este, alt en metros) 
            // a coordenadas fijas centradas en tierra (x, y, z), haga lo siguiente:
            double Re = R; // 6378137;          // radio ecuatorial
            double Rp = R; // 6356752.31424518; // radio polar
            double latrad = lat / 180.0 * Math.PI;
            double lonrad = lon / 180.0 * Math.PI;
            double coslat = Math.Cos(latrad);
            double sinlat = Math.Sin(latrad);
            double coslon = Math.Cos(lonrad);
            double sinlon = Math.Sin(lonrad);
            double term1 = (Re * Re * coslat) / Math.Sqrt(Re * Re * coslat * coslat + Rp * Rp * sinlat * sinlat);
            double term2 = alt * coslat + term1;

            Coordinate c;
            c.x = coslon * term2;
            c.y = sinlon * term2;
            c.z = alt * sinlat + (Rp * Rp * sinlat) / Math.Sqrt(Re * Re * coslat * coslat + Rp * Rp * sinlat * sinlat);

            return c;
        }
    }

    // Distancia entre dos coordenadas (latitud, longitud)
    public class Haversine
    {
        private const float RADS_PER_DEGREE = 0.0174532925199433f;
        private const float KM_RADIUS = 6371.0f;
        private const float M_PER_KM = 1000.0f;

        private float lat1;
        private float lon1;
        private float lat2;
        private float lon2;

        public Haversine(float newLat1, float newLon1, float newLat2, float newLon2)
        {
            this.lat1 = newLat1;
            this.lon1 = newLon1;
            this.lat2 = newLat2;
            this.lon2 = newLon2;
        }

        public double Distance()
        {
            double lat1Rad;
            double lat2Rad;
            double dLonRad;
            double dLatRad;
            double a;

            lat1Rad = lat1 * RADS_PER_DEGREE;
            lat2Rad = lat2 * RADS_PER_DEGREE;
            dLonRad = ((lon2 - lon1) * RADS_PER_DEGREE);
            dLatRad = ((lat2 - lat1) * RADS_PER_DEGREE);

            a = Math.Pow(Math.Sin(dLatRad / 2), 2) + Math.Cos(lat1Rad) * Math.Cos(lat2Rad) * Math.Pow(Math.Sin(dLonRad / 2), 2);
            return (2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a)));
        }

        public double ToKilometers()
        {
            return Distance() * KM_RADIUS;
        }

        public double ToMeters()
        {
            return ToKilometers() * M_PER_KM;
        }

        /**
    * Params: lat1, long1 => Latitude and Longitude of current point
    *         lat2, long2 => Latitude and Longitude of target  point
    *         
    *         headX       => x-Value of built-in phone-compass
    * 
    * Returns the degree of a direction from current point to target point
    *
    */
        public double getGrados(double lat1, double long1, double lat2, double long2, double headX)
        {

            double dLat = (lat2 - lat1) * RADS_PER_DEGREE;
            double dLon = (lon2 - lon1) * RADS_PER_DEGREE;

            lat1 = lat1 * RADS_PER_DEGREE;
            lat2 = lat2 * RADS_PER_DEGREE;

            double y = Math.Sin(dLon) * Math.Cos(lat2);
            double x = Math.Cos(lat1) * Math.Sin(lat2) -
                       Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(dLon);
            double brng = (Math.Atan2(y, x)) / RADS_PER_DEGREE;

            // fix negative degrees
            if (brng < 0)
            {
                brng = 360 + brng;
            }

            return brng - headX;
        }
    }
}
