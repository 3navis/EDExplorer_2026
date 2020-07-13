using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using EDExplorer.Pantallas;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDExplorer
{
    //using ListaInteres = List<(string BodyName, string Description, string Detail)>;
    public class Base
    {
        public Alertas alertas;
        //public Records records;
        public LogMonitor logMonitor;
        public SpeechSynthesizer speech;
        public ConfiguracionFrm configuracionFrm;
        public EDExplorerFrm edexplorerFrm;
        public FontElite fontElite;
        public NotifyFrm notifyFrm;
        public NotifyFormBack fondo;

        public Base()
        {
            alertas = new Alertas();
            
            logMonitor = new LogMonitor();
            logMonitor.basi = this;
            logMonitor.LogEntry += LogEvent; 
            
            // Crear la Fuente una unica vez.
            fontElite = new FontElite();       
            
            ActivarAudio();
        }
        ~Base()
        { // Destuctor del codigo
  //          this.Dispose(false);
        }

        private void LogEvent(object source, EventArgs e)
        {

            if (logMonitor.LastScanValid)
            {
                ScanReader scan = new ScanReader(this);

                if (scan.hayAlertas())
                {
                    AnnounceItems(logMonitor.CurrentSystem, scan.Interest);
                }
            }
            else if (logMonitor.LastCodexValid)
            {
                if (Properties.Settings.Default.IncludeCodex)
                {
                    //Invoke((MethodInvoker)delegate ()
                    //{
                    //    string location;
                    //    listEvent.BeginUpdate();

                    //    if (logMonitor.LastCodex.Category != "$Codex_Category_StellarBodies;" && logMonitor.LastCodex.NearestDestinationLocalised?.Length > 0 && logMonitor.LastCodex.Body?.Length > 0)
                    //        location = logMonitor.LastCodex.Body;
                    //    else
                    //        location = logMonitor.LastCodex.System;

                    //    ListViewItem newItem = new ListViewItem(new string[] { location, logMonitor.LastCodex.NearestDestinationLocalised?.Length > 0 ? logMonitor.LastCodex.NearestDestinationLocalised : "Codex Entry", logMonitor.LastCodex.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"), logMonitor.LastCodex.NameLocalised, string.Empty });
                    //    listEvent.Items.Add(newItem).EnsureVisible();
                    //    listEvent.EndUpdate();
                    //});
                }
            }
            else if (logMonitor.LastSignalValid)
            {
                SignalReader signal = new SignalReader(this);

                if (signal.hayAlertas())
                {
                    AnnounceItems(logMonitor.CurrentSystem, signal.Interest);
                }
            }
            else if (logMonitor.LastFSSValid)
            {
                FSSReader fss = new FSSReader(this);

                if (fss.hayAlertas())
                {
                    AnnounceItems(logMonitor.CurrentSystem, fss.Interest);
                }

            }
        }
        private void AnnounceItems(string currentSystem, List<Interes> items)
        {
            if (edexplorerFrm != null)
            {
                //if (!logMonitor.ReadAllInProgress)
                //    edexplorerFrm.RemoveUninteresting();

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
                        announceText.Append(item.Descripcion);
                        if (!item.Equals(items.Last()))
                        {
                            announceText.AppendLine(", ");
                        }
                    }

                    if (Properties.Settings.Default.activarNotificaciones)
                    {
                        OpenNotifyForm(fullBodyName + "\r\n" + announceText.ToString(), 10000);
                    }

                    if (Properties.Settings.Default.activarAudio)
                    {
                        string spokenName;
                        spokenName = fullBodyName.Replace(currentSystem, string.Empty);
                        if (spokenName.Trim().Length > 0)
                        {
                            spokenName = "Cuerpo " + spokenName;
                        }
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
            edexplorerFrm.Show();
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
                fondo = new NotifyFormBack();
                fondo.Show();

                notifyFrm = new NotifyFrm(t, fondo);
                notifyFrm.Show(mls);
                notifyFrm.Refresh();

                // Manejador de eventos para esa ventana
                Application.Run(notifyFrm);
            });

            //popupNotifier = new PopupNotifier();
            //popupNotifier.TitleFont = new Font(FontElite.private_fonts.Families[0], 12, FontStyle.Bold);
            //popupNotifier.ContentFont = popupNotifier.TitleFont;

            //popupNotifier.TitleText = "Alerta EDExplorer";
            //popupNotifier.ContentText = t;
            //popupNotifier.Delay = mls;
            //popupNotifier.Popup();

            //return await true;
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
