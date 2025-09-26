using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDExplorer
{
    using M = Properties.Textos;
    
    //using ListaInteres = List<(string BodyName, string Description, string Detail)>;
    public class Base
    {
        public bool autoStart = true;
        
        public Alertas alertas;
        
        public LogMonitor logMonitor;
        public StatusMonitor statusMonitor;

        public SpeechSynthesizer speech;
        public ConfiguracionFrm configuracionFrm;
        public EDExplorerFrm edexplorerFrm;
        public FontElite fontElite;
        // public NotifyFrm notifyFrm;

        //private Properties.Settings settings;
        //settings = Properties.Settings.Default;

        public Base()
        {
            alertas = new Alertas();
            
            logMonitor = new LogMonitor();
            logMonitor.basi = this;
            logMonitor.LogEntry += LogEvent; 
            
            // Crear la Fuente una unica vez.
            fontElite = new FontElite();       
            
            ActivarAudio();

            statusMonitor = new StatusMonitor();
            statusMonitor.StatusEntry += StatusEvent;
            statusMonitor.MonitorStart();

            //SQLBase.Up();

            if (Properties.Settings.Default.activarLista)
                OpenEDExplorerForm();

            logMonitor.MonitorStart();
        }
        ~Base()
        { // Destuctor del codigo
  //          this.Dispose(false);
        }

        private void StatusEvent(object source, EventArgs e)
        { // Si se encuentra en Supercrucero abrir ventana supercrucero
            //switch (statusMonitor.lastEvent["BodyName"])
            {
            }
        }

        private void LogEvent(object source, EventArgs e)
        {
            switch (logMonitor.tipoEvento)
            {
                case TipoEvento.Scan:
                    ScanReader scan = new ScanReader(this);

                    if (scan.hayAlertas())
                    {
                        AnnounceItems(scan.Interest);
                    }
                    break;
                case TipoEvento.Signal:
                    SignalReader signal = new SignalReader(this);

                    if (signal.hayAlertas())
                    {
                        AnnounceItems(signal.Interest);
                    }
                    break;
                case TipoEvento.FSS:
                    FSSReader fss = new FSSReader(this);

                    if (fss.hayAlertas())
                    {
                        AnnounceItems(fss.Interest);
                    }
                    break;
                case TipoEvento.Jump:
                    JumpReader jump = new JumpReader(this);

                    if (jump.hayAlertas())
                    {
                        AnnounceItems(jump.Interest);
                    }
                    break;
                case TipoEvento.Codex:
                    if (Properties.Settings.Default.IncludeCodex)
                    {
                    }
                    break;
                case TipoEvento.Hyperspace:
                    // aprovecha la preparación del salto para mostrar info
                    ResumenSesion();
                    break;
            }
        }

        private void ResumenSesion()
        {
            string tipoStar = "";
            string alertaStar = "";
            string repostable = "";

            switch (logMonitor.claseStar.Substring(0,1))
            {
                case "N":
                    tipoStar = M.str_estrella_de_Neutrones;
                    alertaStar = M.str_saltando_estrella_de_Neutrones;
                    break;
                case "D":
                    tipoStar = M.str_enana_Blanca;
                    alertaStar = M.str_saltando_estrella_enana_Blanca;
                    break;
                case "H":
                    tipoStar = M.str_agujero_Negro; break;
                case "C":
                    tipoStar = M.str_estrella_de_carbono; break;
                case "X":
                    tipoStar = M.str_Ex_tica; break;
                case "W":
                    tipoStar = M.str_Wolf_Rayet; break;
                case "O":
                    repostable = "{R} "; tipoStar = M.str_masiva_luminosa; break;
                case "B":
                    repostable = "{R} "; tipoStar = M.str_azul_blanco_luminosa; break;
                case "A":
                    repostable = "{R} "; tipoStar = M.str_caliente_blanca_azulada; break;
                case "F":
                    repostable = "{R} "; tipoStar = M.str_blanca; break;
                case "G":
                    repostable = "{R} "; tipoStar = M.str_blanco_amarilla; break;
                case "K":
                    repostable = "{R} "; tipoStar = M.str_amarillo_naranja; break;
                case "M":
                    repostable = "{R} "; tipoStar = M.str_roja; break;
                case "L":
                    tipoStar = M.str_enana_roja; break;
                case "T":
                    if (logMonitor.claseStar == "TTS") tipoStar = M.str_Tauri;
                    else tipoStar = M.str_enana_marron; break;
                case "Y":
                    tipoStar = M.str_enana_marron1; break;
            }

            if (tipoStar != "") tipoStar = repostable + tipoStar + " (" + logMonitor.claseStar + ")";

            if (Properties.Settings.Default.activarAudio)
                if (alertaStar.Length > 0)
            {
                speech.Volume = Properties.Settings.Default.AudioVolumen;
                //speech.SpeakSsmlAsync($"<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\""+settings.Idioma+"\">"+M.str_atencion+$"<break strength=\"weak\"/>{alertaStar}</speak>");
                speech.SpeakSsmlAsync($"<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\"" + Properties.Settings.Default.Idioma + "\">" + M.str_atencion + $"<break strength=\"weak\"/>{alertaStar}</speak>");
                }

            if (Properties.Settings.Default.activarNotificaciones)
            {
                // Resumen de la sesión
                StringBuilder announceText = new StringBuilder();
                string remainingJUMPsST = "";

                if (logMonitor.remainingJUMPs > 0)
                    remainingJUMPsST = M.str_Ruta + logMonitor.remainingJUMPs.ToString() + "]";

                announceText.AppendLine(M.str_Saltos + logMonitor.sesion_numeroJump.ToString() + remainingJUMPsST);
                announceText.AppendLine(M.str_Distancia + Math.Round(logMonitor.sesion_acumuladoJump).ToString() + M.str_al);

                // Poner el tiempo transcurrido en la sesion.
                TimeSpan difFechas = DateTime.Now - logMonitor.session_Time;
                string tiempoSt = "";

                if (difFechas.Days > 0) tiempoSt = difFechas.Days + " d ";
                if (difFechas.Hours > 0) tiempoSt += difFechas.Hours + " h ";
                if (difFechas.Minutes > 0) tiempoSt += difFechas.Minutes + " m ";
                if (difFechas.Seconds > 0) tiempoSt += difFechas.Seconds + " s";

                announceText.AppendLine(M.str_Tiempo + tiempoSt);

                announceText.AppendLine("-------------"); 
                announceText.AppendLine(tipoStar);

                OpenNotifyForm(M.str_Resumen_sesi_actual + "\r\n" + announceText.ToString(), 15000);
            }
        }

        private void AnnounceItems(List<Interes> items)
        {
            string currentSystem = logMonitor.CurrentSystem;

            if (edexplorerFrm != null)
            {
                foreach (var item in items)
                    edexplorerFrm.AddListItem(item);
            }

            if (!logMonitor.ReadAllInProgress && items.Count > 0)
            {
                if (Properties.Settings.Default.activarNotificaciones || Properties.Settings.Default.activarAudio)
                {
                    string fullBodyName = items[0].BodyName;
                    StringBuilder announceText = new StringBuilder();

                    foreach (var item in items)
                    {
                        announceText.Append(item.Nombre);
                        if (item.isRecord) announceText.Append(" - " + item.RecordDesc);
                        
                        if (!item.Equals(items.Last()))
                        {
                            announceText.AppendLine(", ");
                        }
                    }
                    
                    string spokenName;
                    spokenName = fullBodyName.Replace(currentSystem, string.Empty);
                    
                    if (spokenName.Trim().Length > 0) 
                         spokenName = M.str_Cuerpo + spokenName;
                    else spokenName = M.str_Sistema + currentSystem;

                    if (Properties.Settings.Default.activarNotificaciones)
                    {
                        OpenNotifyForm(spokenName + "\r\n" + announceText.ToString(), 10000);
                    }

                    if (Properties.Settings.Default.activarAudio)
                    {
                        speech.Volume = Properties.Settings.Default.AudioVolumen;
                        //speech.SpeakSsmlAsync($"<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\"" + settings.Idioma + $"\">{spokenName}:<break strength=\"weak\"/>{announceText}</speak>");
                        speech.SpeakSsmlAsync($"<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\"" + Properties.Settings.Default.Idioma + $"\">{spokenName}:<break strength=\"weak\"/>{announceText}</speak>");
                    }
                }
            }
        }
        public void ActivarAudio()
        {
            if (speech == null && Properties.Settings.Default.activarAudio)
            {
                speech = new SpeechSynthesizer();
                speech.SetOutputToDefaultAudioDevice();
            }
        }
        public void TestSound()
        {
            //speech.Volume = settings.TTSVolume;
            speech.SpeakAsync(M.str_Activadas_Alertas_Audibles);
        }
        public void OpenEDExplorerForm()
        {
            if (edexplorerFrm != null)
            {
                edexplorerFrm.BringToFront();
                return;
            }

            edexplorerFrm = new EDExplorerFrm(logMonitor);
            edexplorerFrm.Show(); // Carga ReadAllJournals(30);
        }
        public void CloseEDExplorerForm()
        {
            if (edexplorerFrm != null)
            {
                edexplorerFrm.Close();
                edexplorerFrm = null;
                return;
            }
        }
        public void OpenConfiguracionFrm(Alertas alertas)
        {
            if (configuracionFrm != null)
            {
                configuracionFrm.BringToFront();
                return;
            }

            configuracionFrm = new ConfiguracionFrm(speech, this);
            configuracionFrm.Show();
        }

        public void OpenNotifyForm(string t, int mls)
        {
            //if (notifyFrm != null && notifyFrm.nfb != null)
            //{
            //    notifyFrm.nfb.Close();
            //    notifyFrm.Close();
            //}

            var task = Task.Run(() =>
            {
                NotifyFrm notifyFrm;

                notifyFrm = new NotifyFrm(t, logMonitor.tipoEvento);
                notifyFrm.Show(mls);
                notifyFrm.Refresh();

                // Manejador de eventos para esa ventana
                Application.Run(notifyFrm);
            });

            //task.Wait();
        }
    }
    /// <summary>
    /// Instanciar una Fuente no instalada
    /// </summary>
    public class FontElite
    {
        public static PrivateFontCollection private_fonts = new PrivateFontCollection();
        public FontElite()
        {
            // Use this if you can not find your resource System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();
            //string resource = "EDExplorer.Resources.elitedanger.ttf";
            string resource = "EDExplorer.Resources.Eurostar.ttf";
            // receive resource stream
            Stream fontStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);

            //create an unsafe memory block for the data
            System.IntPtr data = Marshal.AllocCoTaskMem((int)fontStream.Length);
            //create a buffer to read in to
            Byte[] fontData = new Byte[fontStream.Length];
            //fetch the font program from the resource
            fontStream.Read(fontData, 0, (int)fontStream.Length);
            //copy the bytes to the unsafe memory block
            Marshal.Copy(fontData, 0, data, (int)fontStream.Length);

            // We HAVE to do this to register the font to the system (Weird .NET bug !)
            //uint cFonts = 0;
            //AddFontMemResourceEx(data, (uint)fontData.Length, IntPtr.Zero, ref cFonts);

            //pass the font to the font collection
            private_fonts.AddMemoryFont(data, (int)fontStream.Length);
            //close the resource stream
            fontStream.Close();
            //free the unsafe memory
            Marshal.FreeCoTaskMem(data);
        }
    }

    public class Idioma
    {
        public String Nombre { get; set; }
        public String Abreviacion { get; set; }
        public String Pais { get; set; }
        public String AbreviacionPais { get; set; }

        public String NombrePais {get { return Nombre + " (" + Pais + ")"; }}
        public String CultureInfo {get { return Abreviacion + "-" + AbreviacionPais; }}

        public static List<Idioma> ObtenerIdiomas()
        {
            return new List<Idioma> {
                new Idioma
                {
                    Nombre = "Español",
                    Abreviacion = "es",
                    Pais = "España",
                    AbreviacionPais = "ES"
                },
                 new Idioma
                {
                    Nombre = "English",
                    Abreviacion = "en",
                    Pais = "United Kingdom",
                    AbreviacionPais = "GB"
                },
                 new Idioma
                {
                    Nombre = "French",
                    Abreviacion = "fr",
                    Pais = "France",
                    AbreviacionPais = "FR"
                },
                  new Idioma
                {
                    Nombre = "português",
                    Abreviacion = "pt",
                    Pais = "Portugal",
                    AbreviacionPais = "PT"
                },
                  new Idioma
                {
                    Nombre = "euskera",
                    Abreviacion = "eu",
                    Pais = "Euskadi",
                    AbreviacionPais = "ES"
                },
                  new Idioma
                {
                    Nombre = "català",
                    Abreviacion = "ca",
                    Pais = "Catalunya",
                    AbreviacionPais = "ES"
                },
                  new Idioma
                {
                    Nombre = "日本",
                    Abreviacion = "ja",
                    Pais = "日本",
                    AbreviacionPais = "JP"
                }

            };
        }

        //public static void CambiarTexto(Control.ControlCollection controls)
        //{
        //    foreach (Control c in controls)
        //    {
        //        if (c is Panel)
        //        {
        //            CambiarTexto(c.Controls);
        //        }
        //        else
        //        {
        //            String text = Strings.ResourceManager.GetString(c.Name);
        //            if (text != null)
        //            {
        //                c.Text = text;
        //            }
        //        }

        //    }
        //}

    }
}
