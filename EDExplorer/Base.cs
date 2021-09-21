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
            //statusMonitor.MonitorStart();

            SQLBase.Up();

            if (Properties.Settings.Default.activarLista)
                OpenEDExplorerForm();

            logMonitor.MonitorStart();
        }
        ~Base()
        { // Destuctor del codigo
  //          this.Dispose(false);
        }

        private void StatusEvent(object source, EventArgs e)
        {
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
                    InfoStartJump();
                    break;
            }
        }

        private void InfoStartJump()
        {
            string tipoStar = "";
            string alertaStar = "";

            switch (logMonitor.nextStar.Substring(0,1))
            {
                case "N":
                    tipoStar = "Neutrones";
                    alertaStar = "saltando a estrella de Neutrones";
                    break;
                case "D":
                    tipoStar = "enana Blanca";
                    alertaStar = "saltando a estrella enana Blanca";
                    break;
                case "H":
                    tipoStar = "agujero Negro";
                    break;
                case "X":
                    tipoStar = "Exótica";
                    break;
                case "O":
                case "B":
                case "A":
                case "F":
                case "G":
                case "K":
                case "M":
                    tipoStar = "permite repostar";
                    break;
            }

            if (Properties.Settings.Default.activarAudio)
                if (alertaStar.Length > 0)
            {
                speech.Volume = Properties.Settings.Default.AudioVolumen;
                speech.SpeakSsmlAsync($"<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\"es-ES\">Atención:<break strength=\"weak\"/>{alertaStar}</speak>");
            }

            if (Properties.Settings.Default.activarNotificaciones)
            {
                // Resumen de la sesión
                StringBuilder announceText = new StringBuilder();

                announceText.AppendLine("Saltos: " + logMonitor.sesion_numeroJump.ToString());
                announceText.AppendLine("Distancia: " + Math.Round(logMonitor.sesion_acumuladoJump,1).ToString() + " al");

                // Poner el tiempo transcurrido, no la hora de inicio.
                TimeSpan difFechas = DateTime.Now - logMonitor.session_Time;
                
                string tiempoSt = "";

                if (difFechas.Hours > 0) tiempoSt = difFechas.Hours + " h ";
                if (difFechas.Minutes > 0) tiempoSt += difFechas.Minutes + " m ";
                if (difFechas.Seconds > 0) tiempoSt += difFechas.Seconds + " s";

                announceText.AppendLine("Tiempo: " + tiempoSt);
                
                OpenNotifyForm("Resumen sesión" + "\r\n" + announceText.ToString(), 10000);
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
                        if (!item.Equals(items.Last()))
                        {
                            announceText.AppendLine(", ");
                        }
                    }
                    
                    string spokenName;
                    spokenName = fullBodyName.Replace(currentSystem, string.Empty);
                    if (spokenName.Trim().Length > 0)
                    {
                        spokenName = "Cuerpo " + spokenName;
                    }

                    if (Properties.Settings.Default.activarNotificaciones)
                    {
                        OpenNotifyForm(spokenName + "\r\n" + announceText.ToString(), 10000);
                    }

                    if (Properties.Settings.Default.activarAudio)
                    {
                        speech.Volume = Properties.Settings.Default.AudioVolumen;
                        speech.SpeakSsmlAsync($"<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\"es-ES\">{spokenName}:<break strength=\"weak\"/>{announceText}</speak>");
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
            speech.SpeakAsync("Activadas Alertas Audibles.");
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

                notifyFrm = new NotifyFrm(t);
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
            string resource = "EDExplorer.Resources.elitedanger.ttf";
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
}
