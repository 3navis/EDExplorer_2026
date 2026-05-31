using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EDExplorer
{
    using M = Properties.Textos;

    public class LogMonitor
    {
        public Base basi;
        public string evento;
        public TipoEvento tipoEvento;

        private readonly FileSystemWatcher logWatcher;
        private string currentSystem;
        public double[] posInicial; // XYZ
        public double acumuladoJump = 0;
        public double sesion_acumuladoJump = 0;
        public int numeroJump = 0;
        public int sesion_numeroJump = 0;
        public long remainingJUMPs = 0;
        public string claseStar;
        private string currentBody;
        public DateTime currentTime;
        public DateTime session_Time = DateTime.Now;
        public string CurrentLogPath { get; private set; }
        public string CurrentLogLine { get; private set; }
        public int bytesRead { get; private set; }

        public bool JumponiumReported;
        public bool GoldSystemReported;
        private List<string> LinesToProcess;
        private string LogDirectory;
        private string LogName;
        public bool ReadAllInProgress { get; private set; }
        public bool ReadAllComplete { get; private set; }
        public JournalEventStats EventStats { get; private set; }
        public ScanEvent LastScan { get; private set; }
        public CodexEntry LastCodex { get; private set; }
        public SaaSignalsFound LastSignal { get; private set; }
        public FSSDiscoveryScan LastFSS { get; private set; }
        public FsdJump LastJump { get; private set; }
        public Dictionary<(string System, long Body), ScanEvent> SystemBody { get; private set; }
        public Dictionary<(string System, long Body), bool> SystemBodySignal { get; private set; }

        public Dictionary<(string System, long Body), List<long>> EsperandoPadre;

        private JournalPoker Poker;
        private ulong? CurrentAddress = 0;
        public string CurrentSystem
        {
            get
            {
                return currentSystem;
            }
            private set
            {
                if (value != currentSystem)
                {
                    JumponiumReported = false;
                    GoldSystemReported = false;
                }
                currentSystem = value;
            }
        }

        public LogMonitor()
        {
            LogDirectory = Properties.Settings.Default.JournalPath;
            LogDirectory = CheckLogPath();
            Properties.Settings.Default.JournalPath = LogDirectory;
            LogName = Properties.Settings.Default.JournalName;

            // conserva los cambios de configuración entre sesiones de aplicación
            Properties.Settings.Default.Save();

            // Examina cambios producidos en el directorio indicado
            logWatcher = new FileSystemWatcher(LogDirectory, LogName)
            {
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName
            };
            logWatcher.Changed += LogChanged;
            logWatcher.Created += LogChanged;
            ReadAllInProgress = false;
            ReadAllComplete = false;
            EventStats = JournalEventStats.Load(Properties.Settings.Default.EventStats);
            CurrentSystem = string.Empty;
            JumponiumReported = false;
            SystemBody = new Dictionary<(string, long), ScanEvent>();
            //SystemBodySignal = new Dictionary<(string, long), SaaSignalsFound>();
            SystemBodySignal = new Dictionary<(string, long), bool>();
            EsperandoPadre = new Dictionary<(string, long), List<long>>();
        }

        public void MonitorStart()
        {
            if (!IsMonitoring())
            {
                //PopulatePastScans();
                logWatcher.EnableRaisingEvents = true;
                Poker = new JournalPoker(LogDirectory);
                Poker.Start();
            }
        }

        public void MonitorStop()
        {
            if (IsMonitoring())
            {
                logWatcher.EnableRaisingEvents = false;
                Poker.Stop();
                Poker = null;
            }
        }
        public bool IsMonitoring()
        {
            return logWatcher.EnableRaisingEvents;
        }
        public void ReadAll(ProgressBar progressBar, int ultimos = 1000)
        {
            MonitorStop();
            //////////////

            ReadAllInProgress = true;
            //EventStats = new JournalEventStats();
            progressBar.Visible = true;
            SystemBody.Clear();
            SystemBodySignal.Clear();
            EsperandoPadre.Clear();
            DirectoryInfo logDir = new DirectoryInfo(CheckLogPath());

            int progress = 0;

            // new DateTime(2017, 04, 12)
            FileInfo[] aux = logDir.GetFiles(Properties.Settings.Default.JournalName)
                .Where(f => f.CreationTime > new DateTime(2017, 04, 12))
                .OrderBy(f => f.CreationTime).ToArray();

            if (ultimos > aux.Count() || ultimos <= 0) ultimos = aux.Count();

            FileInfo[] allJournals = new FileInfo[ultimos];
            Array.Copy(aux, aux.Count() - ultimos, allJournals, 0, ultimos);

            try
            {
                progress = 0;

                foreach (var journalFile in allJournals)
                {
                    using (StreamReader currentLog = new StreamReader(File.Open(journalFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                    {
                        while (!currentLog.EndOfStream)
                        {
                            CurrentLogLine = currentLog.ReadLine();
                            ProcessEvent(CurrentLogLine);

                            // IDEA: Se podria salir cuando lleve XX Alertas?
                            // Quizas un Count de la lista en pantalla?

                        }
                    }

                    progressBar.Value = (progress++ * 100) / allJournals.Count();
                    progressBar.Refresh();
                }

                CurrentLogPath = allJournals[allJournals.Count() - 1].FullName;
                bytesRead = (int)allJournals[allJournals.Count() - 1].Length;
            }
            catch (Exception ex)
            {
                DialogResult response = MessageBox.Show(M.str_Ha_ocurrido_un_error_al_leer_los, M.str_Error_Leyendo_Logs, MessageBoxButtons.YesNo);
                if (response == DialogResult.Yes)
                {
                    MessageBox.Show($"Journal Line: {CurrentLogLine}\r\nException message: {ex.Message}\r\n\r\nStack trace: {ex.StackTrace}", "Detalle del Error", MessageBoxButtons.OK);
                }
            }
            progressBar.Visible = false;
            ReadAllInProgress = false;
            ReadAllComplete = true;

            ///////////////
            MonitorStart();
        }

        private string CheckLogPath()
        {
            LogDirectory = string.IsNullOrEmpty(LogDirectory) ? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Saved Games\\Frontier Developments\\Elite Dangerous" : LogDirectory;
            if (!Directory.Exists(LogDirectory) || new DirectoryInfo(LogDirectory).GetFiles(Properties.Settings.Default.JournalName).Count() == 0)
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
                {
                    RootFolder = Environment.SpecialFolder.MyComputer,
                    ShowNewFolderButton = false,
                    Description = M.str_seleccionar_carpeta
                };
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    LogDirectory = folderBrowserDialog.SelectedPath;
                }
            }
            return LogDirectory;
        }

        private void LogChanged(object source, FileSystemEventArgs e)
        {
            if (Poker != null) Poker.Reset = true;

            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Created:
                    CurrentLogPath = e.FullPath;
                    CurrentLogPath = string.Empty;
                    bytesRead = 0;
                    session_Time = DateTime.Now;
                    break;

                case WatcherChangeTypes.Changed:
                    if (CurrentLogPath != e.FullPath)
                    {
                        CurrentLogPath = e.FullPath;
                        bytesRead = 0;
                        session_Time = DateTime.Now;
                    }

                    //////////////////////////////////////////////////////////////////////////////
                    int newLineBytes = System.Environment.NewLine.Length;
                    string linea;

                    using (StreamReader currentLog = new StreamReader(File.Open(CurrentLogPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                    {
                        // Primero salta las lineas ya procesadas anteriormente
                        currentLog.BaseStream.Seek(bytesRead, SeekOrigin.Begin);

                        // Segundo actualiza la posicion con las nuevas lineas leidas del fichero
                        for (LinesToProcess = new List<string>(); !currentLog.EndOfStream;)
                        {
                            linea = currentLog.ReadLine();
                            LinesToProcess.Add(linea);
                            //bytesRead += linea.Length + newLineBytes;
                        }
                        bytesRead = (int)currentLog.BaseStream.Length;
                    }

                    foreach (string line in LinesToProcess)
                        ProcessEvent(line);

                    break;

                default:
                    break;
            }
        }

        private void ProcessEvent(in string logLine)
        {
            /////////////////////////////////////////////////////////
            /// Esta funcion se utiliza para verificar que existe un
            /// evento procesable, antes de empezar a manipularlo.
            /// Sirve para optimizar velocidad del procesado.

            int pa = logLine.IndexOf("\"event\":");
            if (pa > 0)
            {
                pa += 9;
                int pb = logLine.IndexOf("\"", pa + 1);
                evento = logLine.Substring(pa, pb - pa);

                EventStats.RegisterEvent(evento, ExtractTimestamp(logLine));

                const string eventos = "Scan,Location,FSDJump,CarrierJump,SAASignalsFound," +
                    "FSSDiscoveryScan,SupercruiseExit,StartJump,FSDTarget,Commander," +
                    "FSSBodySignals,";

                if (eventos.Contains(evento))
                {
                    // Proteccion contra lineas incompletas
                    if (logLine.StartsWith("{") && logLine.EndsWith("}"))
                        ProcessLine(evento, logLine);
                }
            }
        }

        public void SaveEventStats()
        {
            Properties.Settings.Default.EventStats = EventStats.ToJson();
            Properties.Settings.Default.Save();
        }

        private DateTime ExtractTimestamp(string logLine)
        {
            const string timestampToken = "\"timestamp\":\"";
            int pa = logLine.IndexOf(timestampToken);
            if (pa < 0)
                return DateTime.Now;

            pa += timestampToken.Length;
            int pb = logLine.IndexOf("\"", pa);
            if (pb <= pa)
                return DateTime.Now;

            string timestamp = logLine.Substring(pa, pb - pa);
            if (DateTime.TryParse(timestamp, out DateTime parsed))
                return parsed;

            return DateTime.Now;
        }

        private void ProcessLine(in string evento, in string logLine)
        {
            try
            {
                JObject lastEvent = (JObject)JsonConvert.DeserializeObject(logLine, new JsonSerializerSettings() { DateParseHandling = DateParseHandling.None });
                currentTime = (DateTime)lastEvent["timestamp"];
                tipoEvento = TipoEvento.None;

                switch (evento)
                {
                    case "Scan":
                        if (!lastEvent["BodyName"].ToString().Contains("Belt Cluster"))
                        {
                            LastScan = lastEvent.ToObject<ScanEvent>();
                            LastScan.JournalEntry = logLine;

                            if (!(CurrentAddress > 0)) // Si en JUMP no estaba el systemID
                            {
                                CurrentAddress = LastScan.SystemAddress ?? (ulong)0;
                                //                               SQLBase.AddSystem((ulong)CurrentAddress, LastScan.StarSystem);
                            }

                            //                           SQLBase.AddCuerpo((ulong)CurrentAddress,LastScan.BodyId, LastScan.BodyName);

                            if (!SystemBody.ContainsKey((CurrentSystem, LastScan.BodyId)))
                            {
                                SystemBody[(CurrentSystem, LastScan.BodyId)] = LastScan;
                                tipoEvento = TipoEvento.Scan;
                            }
                        }
                        break;
                    case "SAASignalsFound":
                    case "FSSBodySignals":
                        LastSignal = lastEvent.ToObject<SaaSignalsFound>();
                        LastSignal.Body = currentBody;

                        if (!SystemBodySignal.ContainsKey((CurrentSystem, (long)LastSignal.BodyId)))
                        {
                            SystemBodySignal[(CurrentSystem, (long)LastSignal.BodyId)] = true;
                            tipoEvento = TipoEvento.Signal;
                        }
                        break;
                    case "FSDTarget":
                        if (!ReadAllInProgress)
                        {
                            remainingJUMPs = (long?)lastEvent["RemainingJumpsInRoute"] ?? (long)0;
                        }
                        break;
                    case "StartJump":
                        if (!ReadAllInProgress)
                            if (lastEvent["JumpType"].ToString() == "Hyperspace")
                            {
                                //Mostrar mensaje resumen mientras prepara el salto
                                claseStar = lastEvent["StarClass"].ToString();
                                tipoEvento = TipoEvento.Hyperspace;
                            }
                        break;
                    case "FSDJump":
                        LastJump = lastEvent.ToObject<FsdJump>();
                        CurrentSystem = lastEvent["StarSystem"].ToString();
                        CurrentAddress = (ulong?)lastEvent["SystemAddress"];

                        // Solo es necesario cachear el sistema actual
                        SystemBody.Clear();
                        SystemBodySignal.Clear();
                        EsperandoPadre.Clear();

                        //SQLBase.AddSystem(LastJump.SystemAddress??(ulong)0, LastJump.StarSystem, LastJump.Timestamp);

                        // No contabilizar dentro de la Sesion
                        if (!ReadAllInProgress) tipoEvento = TipoEvento.Jump;
                        break;
                    case "CarrierJump":
                        // Al entrar los Carriers no se actualizaba el nombre en el salto
                        CurrentSystem = lastEvent["StarSystem"].ToString();
                        break;
                    case "Location":
                        CurrentSystem = lastEvent["StarSystem"].ToString();
                        break;
                    case "FSSDiscoveryScan":
                        LastFSS = lastEvent.ToObject<FSSDiscoveryScan>();
                        if (LastFSS.SystemName == null) LastFSS.SystemName = CurrentSystem;  // Location
                        CurrentSystem = LastFSS.SystemName;
                        tipoEvento = TipoEvento.FSS;
                        break;
                    case "FSSAllBodiesFound":
                        if (lastEvent["SystemName"] != null)
                        {
                            CurrentSystem = lastEvent["SystemName"].ToString();
                        }
                        break;
                    case "CodexEntry":
                        if (LastCodex?.Timestamp != lastEvent.ToObject<CodexEntry>().Timestamp)
                        {
                            LastCodex = lastEvent.ToObject<CodexEntry>();
                            LastCodex.Body = currentBody;
                            LocaliseLastCodex();
                            //LastCodexValid = true;
                            tipoEvento = TipoEvento.Codex;
                        }
                        break;
                    case "SupercruiseExit":
                        if (lastEvent["Body"]?.ToString().Length > 0)
                            currentBody = lastEvent["Body"].ToString();
                        else
                            currentBody = null;
                        break;
                    case "ProspectedAsteroid":
                        break;
                    case "Commander":
                        break;
                    default:
                        break;
                }

                if (tipoEvento != TipoEvento.None)
                {
                    /////////////////////////////////////////
                    // La idea es que cada evento se procese en esta funcion, y si es un evento de interes, se lance el evento LogEntry para que se actualice la informacion en pantalla.

                    // segun GPT puede generar excepciones si el evento se procesa muy rapido, aunque no he podido reproducirlo,
                    // asi que lo dejo comentado por ahora. Si se vuelve a producir, se podria volver a activar y si genera excepcion,
                    // mostrar un mensaje de error con la linea que lo ha provocado.

                    //LogEntry?.Invoke(this, EventArgs.Empty); // => Base.LogEvent
                    LogEntry(this, EventArgs.Empty); // => Base.LogEvent
                    ////////////////////////////////////////
                }

            }
            catch (Exception ex)
            {
                DialogResult response = MessageBox.Show(M.str_Ha_ocurrido_un_error_en_ProcessLine, M.str_Error_Procesando_Linea, MessageBoxButtons.YesNo);
                if (response == DialogResult.Yes)
                {
                    MessageBox.Show($"Evento: {evento}\r\nLinea: {logLine}\r\nException message: {ex.Message}\r\n\r\nStack trace: {ex.StackTrace}", M.str_Detalle_del_Error, MessageBoxButtons.OK);
                }
            }
        }

        //private void PopulatePastScans()
        //{
        //    FileInfo fileToRead = null;

        //    foreach (var file in new DirectoryInfo(LogDirectory).GetFiles(Properties.Settings.Default.JournalName))
        //    {
        //        if (fileToRead == null || string.Compare(file.Name, fileToRead.Name) > 0)
        //        {
        //            fileToRead = file;
        //        }
        //    }

        //    if (fileToRead != null)
        //        using (StreamReader currentLog = new StreamReader(fileToRead.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
        //        {
        //            while (!currentLog.EndOfStream)
        //            {
        //                string logLine = currentLog.ReadLine();
        //                if (logLine.Trim().StartsWith("{") && logLine.Trim().EndsWith("}") && logLine.Contains("\"event\":\"Scan\"") || logLine.Contains("\"event\":\"Location\"") || logLine.Contains("\"event\":\"FSDJump\""))
        //                {

        //                    JObject scanEvent = (JObject)JsonConvert.DeserializeObject(logLine, new JsonSerializerSettings() { DateParseHandling = DateParseHandling.None });

        //                    switch (scanEvent["event"].ToString())
        //                    {
        //                        case "Scan":

        //                            if (!scanEvent["BodyName"].ToString().Contains("Belt Cluster"))
        //                            {
        //                                ScanEvent scan = scanEvent.ToObject<ScanEvent>();
        //                                if (!SystemBody.ContainsKey((CurrentSystem, scan.BodyId)))
        //                                {
        //                                    SystemBody[(CurrentSystem, scan.BodyId)] = scan;
        //                                }
        //                            }
        //                            break;
        //                        case "SAASignalsFound":
        //                            SaaSignalsFound signal = scanEvent.ToObject<SaaSignalsFound>();

        //                            if (!SystemBodySignal.ContainsKey((CurrentSystem, (long)signal.BodyId)))
        //                            {
        //                                SystemBodySignal[(CurrentSystem, (long)signal.BodyId)] = signal;
        //                            }
        //                            break;
        //                        case "FSDJump":
        //                        case "Location":
        //                            CurrentSystem = scanEvent["StarSystem"].ToString();
        //                            break;
        //                    }
        //                }
        //            }
        //        }
        //}

        //Frontier's codex name localisations are frequently lacking detail or otherwise unhelpful.
        //Need to fix that to display useful descriptions.
        private void LocaliseLastCodex()
        {
            switch (LastCodex.NameLocalised)
            {
                case "Standard gas giant":
                case "Green gas giant":
                    LastCodex.NameLocalised =
                        LastCodex.NameLocalised.Replace("gas giant", "Gas Giant:").Replace("Standard ", "") +
                        LastCodex.Name
                            .Replace("$Codex_Ent_Standard_", " ")
                            .Replace("Sudarsky_", "")
                            .Replace("_Name;", "")
                            .Replace("Giant_With_", "")
                            .Replace("_", " ");
                    break;

                default:

                    switch (LastCodex.Name)
                    {
                        case "$Codex_Ent_Standard_Ammonia_Worlds_Name;":
                            LastCodex.NameLocalised = "Ammonia World";
                            break;
                        case "$Codex_Ent_TRF_Ammonia_Worlds_Name;":
                            LastCodex.NameLocalised = "Terraformable Ammonia World";
                            break;
                        case "$Codex_Ent_Standard_High_Metal_Content_No_Atmos_Name;":
                            LastCodex.NameLocalised = "High Metal Content w/o Atmosphere";
                            break;
                        case "$Codex_Ent_Standard_Ter_High_Metal_Content_Name;":
                            LastCodex.NameLocalised = "High Metal Content w/ Atmosphere";
                            break;
                        case "$Codex_Ent_TRF_High_Metal_Content_No_Atmos_Name;":
                            LastCodex.NameLocalised = "Terraformable High Metal Content w/o Atmosphere";
                            break;
                        case "$Codex_Ent_TRF_Ter_High_Metal_Content_Name;":
                            LastCodex.NameLocalised = "Terraformable High Metal Content w/ Atmosphere";
                            break;
                        case "$Codex_Ent_Standard_Ice_No_Atmos_Name;":
                            LastCodex.NameLocalised = "Icy Body w/o Atmosphere";
                            break;
                        case "$Codex_Ent_Standard_Ter_Ice_Name;":
                            LastCodex.NameLocalised = "Icy Body w/ Atmosphere";
                            break;
                        case "$Codex_Ent_TRF_Ice_No_Atmos_Name;":
                            LastCodex.NameLocalised = "Terraformable Icy Body w/o Atmosphere";
                            break;
                        case "$Codex_Ent_TRF_Ter_Ice_Name;":
                            LastCodex.NameLocalised = "Terraformable Icy Body w/ Atmosphere";
                            break;
                        case "$Codex_Ent_Standard_Rocky_Ice_No_Atmos_Name;":
                            LastCodex.NameLocalised = "Rocky Ice Body w/o Atmosphere";
                            break;
                        case "$Codex_Ent_Standard_Ter_Rocky_Ice_Name;":
                            LastCodex.NameLocalised = "Rocky Ice Body w/ Atmosphere";
                            break;
                        case "$Codex_Ent_TRF_Rocky_Ice_No_Atmos_Name;":
                            LastCodex.NameLocalised = "Terraformable Rocky Ice Body w/o Atmosphere";
                            break;
                        case "$Codex_Ent_TRF_Ter_Rocky_Ice_Name;":
                            LastCodex.NameLocalised = "Terraformable Rocky Ice Body w/ Atmosphere";
                            break;
                        case "$Codex_Ent_Standard_Rocky_No_Atmos_Name;":
                            LastCodex.NameLocalised = "Rocky Body w/o Atmosphere";
                            break;
                        case "$Codex_Ent_Standard_Ter_Rocky_Name;":
                            LastCodex.NameLocalised = "Rocky Body w/ Atmosphere";
                            break;
                        case "$Codex_Ent_TRF_Rocky_No_Atmos_Name;":
                            LastCodex.NameLocalised = "Terraformable Rocky Body w/o Atmosphere";
                            break;
                        case "$Codex_Ent_TRF_Ter_Rocky_Name;":
                            LastCodex.NameLocalised = "Terraformable Rocky Body w/ Atmosphere";
                            break;
                        case "$Codex_Ent_Standard_Metal_Rich_No_Atmos_Name;":
                            LastCodex.NameLocalised = "Metal Rich Body w/o Atmosphere";
                            break;
                        case "$Codex_Ent_Standard_Ter_Metal_Rich_Name;":
                            LastCodex.NameLocalised = "Metal Rich Body w/ Atmosphere";
                            break;
                        case "$Codex_Ent_TRF_Metal_Rich_No_Atmos_Name;":
                            LastCodex.NameLocalised = "Terraformable Metal Rich Body w/o Atmosphere";
                            break;
                        case "$Codex_Ent_TRF_Ter_Metal_Rich_Name;":
                            LastCodex.NameLocalised = "Terraformable Metal Rich Body w/ Atmosphere";
                            break;
                        case "$Codex_Ent_Standard_Water_Worlds_Name;":
                            LastCodex.NameLocalised = "Water World";
                            break;
                        case "$Codex_Ent_TRF_Water_Worlds_Name;":
                            LastCodex.NameLocalised = "Terraformable Water World";
                            break;
                    }
                    break;
            }
        }

        public event EventHandler LogEntry;
    }
}
