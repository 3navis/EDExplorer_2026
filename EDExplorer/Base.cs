using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;

namespace EDExplorer
{
    class Base
    {
        public Alertas alertas;
        public LogMonitor logMonitor;
        public SpeechSynthesizer speech;
        public ConfiguracionFrm configuracionFrm;
        public EDExplorerFrm edexplorerFrm;
        public FontElite fontElite;

        public Base()
        {
            alertas = new Alertas();
            logMonitor = new LogMonitor();
            logMonitor.LogEntry += LogEvent;
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

                if (scan.IsInteresting())
                {
                    if (edexplorerFrm != null)
                    {
                        if (!logMonitor.ReadAllInProgress)
                            edexplorerFrm.RemoveUninteresting();

                        foreach (var item in scan.Interest)
                            edexplorerFrm.AddListItem(item);
                    }

                    if (!logMonitor.ReadAllInProgress && scan.Interest.Count > 0)
                        AnnounceItems(logMonitor.CurrentSystem, scan.Interest);
                }
                else if (!logMonitor.ReadAllInProgress)
                {

                    //ListViewItem newItem = new ListViewItem(new string[] { scan.Interest[0].BodyName, "Sin Interés", logMonitor.LastScan.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"), string.Empty, string.Empty })
                    //{
                    //    UseItemStyleForSubItems = false
                    //};

                    //Invoke((MethodInvoker)delegate ()
                    //{
                    //listEvent.BeginUpdate();
                    //RemoveUninteresting();
                    //newItem.SubItems[0].ForeColor = Color.DarkGray;
                    //newItem.SubItems[1].ForeColor = Color.DarkGray;
                    //newItem.SubItems[2].ForeColor = Color.DarkGray;
                    //listEvent.Items.Add(newItem).EnsureVisible();
                    //listEvent.EndUpdate();
                    //});
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

                if (signal.EsInteresante())
                {
                    if (edexplorerFrm != null)
                    {
                        if (!logMonitor.ReadAllInProgress)
                            edexplorerFrm.RemoveUninteresting();

                        foreach (var item in signal.Interest)
                            edexplorerFrm.AddListItem(item);
                    }

                    if (!logMonitor.ReadAllInProgress && signal.Interest.Count > 0)
                        AnnounceItems(logMonitor.CurrentSystem, signal.Interest);
                }

            }
        }

        private void AnnounceItems(string currentSystem, List<(string BodyName, string Description, string Detail)> items)
        {
            if (Properties.Settings.Default.activarNotificaciones || Properties.Settings.Default.activarAudio)
            {
                string fullBodyName = items[0].BodyName;
                StringBuilder announceText = new StringBuilder();

                foreach (var item in items)
                {
                    announceText.Append(item.Description);
                    if (!item.Equals(items.Last()))
                    {
                        announceText.AppendLine(", ");
                    }
                }

                if (Properties.Settings.Default.activarNotificaciones)
                {
                    NotifyFrm notifyFrm = new NotifyFrm(fullBodyName + "\r\n" + announceText.ToString());
                    notifyFrm.Show(5000);
                }

                if (Properties.Settings.Default.activarAudio)
                {
                    string spokenName;
                    spokenName = fullBodyName.Replace(currentSystem, string.Empty);
                    if (spokenName.Trim().Length > 0)
                    {
                        spokenName = "Cuerpo " + spokenName;
                    }
                    speech.Volume = Properties.Settings.Default.TTSVolume;
                    speech.SpeakSsmlAsync($"<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\"es-ES\">{spokenName}:<break strength=\"weak\"/>{announceText}</speak>");
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

            try
            {
                edexplorerFrm = new EDExplorerFrm(logMonitor);
                edexplorerFrm.Show();
            }
            finally
            {
                //edexplorerFrm = null;
            }
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

            try
            {
                using (configuracionFrm = new ConfiguracionFrm(speech,alertas))
                    configuracionFrm.ShowDialog();
            }
            finally
            {
                configuracionFrm = null;
            }
        }

    }
}
