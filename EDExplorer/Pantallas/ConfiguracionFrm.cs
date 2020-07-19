using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;
using System.Windows.Forms;
using EDExplorer.Pantallas;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//using System.Windows.Input;
//using System.Windows;

namespace EDExplorer
{
    public partial class ConfiguracionFrm : Form
    {
        private Base basi;

        private Properties.Settings settings;
        private bool Loading;
        private bool BulkChangeInProgress;
        private SpeechSynthesizer speech;
        private Alertas alertas;

        public ConfiguracionFrm(SpeechSynthesizer s, Base b)
        {
            InitializeComponent();
#if !DEBUG
            this.tabOculta.Parent = null;
#endif

            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            settings = Properties.Settings.Default;
            speech = s;
            alertas = b.alertas;
            basi = b;
        }
        private void ConfiguracionFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            basi.configuracionFrm = null;
        }

        private void ConfiguracionFrm_Load(object sender, EventArgs e)
        {
            Loading = true;
            BulkChangeInProgress = true;
            cbx_VeryInteresting.Checked = settings.VeryInteresting;
            cbxToast.Checked = settings.activarNotificaciones;
            trackBar_Transparencia.Value = settings.Opacidad;
            cbxTts.Checked = settings.activarAudio;
            trackBar_Volume.Value = settings.AudioVolumen;
            trackBar_Volume.BackColor = this.BackColor;
            btn_TestVol.Enabled = settings.activarAudio;
            cbxAutoMonitor.Checked = settings.AutoSTART;
            //cbxCodex.Checked = settings.IncludeCodex;
            cbxBeta.Checked = settings.JournalBeta;

            RellenarTabPage(this.tabScan, TipoEvento.Scan);
            RellenarTabPage(this.tabSignal, TipoEvento.Signal);
            RellenarTabPage(this.tabFSS, TipoEvento.FSS);
            RellenarTabPage(this.tabFSS, TipoEvento.Jump);

            Loading = false;
            BulkChangeInProgress = false;
        }

        private void RellenarTabPage(TabPage tab, TipoEvento TE)
        {
            PanelAlerta pa;
            int n = 1 + tab.Controls.OfType<PanelAlerta>().Count();

            foreach (KeyValuePair<Alerta, DetallesAlerta> alerta in alertas.n.Where(a => a.Value.tipoEvento == TE))
            {
                pa = new PanelAlerta(alerta);
                tab.Controls.Add(pa);
                pa.Location = new System.Drawing.Point(5, 5 + pa.Height * n);
                pa.Name = "panel1";
                pa.Size = new System.Drawing.Size(pa.Width, pa.Height);
                pa.TabIndex = 10 * n;
                n++;
            }
        }

        private void Cbx_VeryInteresting_CheckedChanged(object sender, EventArgs e)
        {
            settings.VeryInteresting = ((CheckBox)sender).Checked;
            Save();
        }

        private void CbxToast_CheckedChanged(object sender, EventArgs e)
        {
            settings.activarNotificaciones = ((CheckBox)sender).Checked;
            Save();
        }

        private void CbxTts_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxTts.Checked)
            {
                try
                {
                    speech = new System.Speech.Synthesis.SpeechSynthesizer();
                    speech.SetOutputToDefaultAudioDevice();
                    //cbxTtsDetail.Visible = true;
                }
                catch
                {
                    MessageBox.Show("There was an error while initializing Text-To-Speech. Windows Text-To-Speech services may not be available on this system.");
                    cbxTts.Checked = false;
                }
            }
            else
            {
                speech.Dispose();
                //cbxTtsDetail.Visible = false;
            }
            btn_TestVol.Enabled = cbxTts.Checked;
            settings.activarAudio = cbxTts.Checked;
            Save();
        }

        /*        private void TxtCopy_TextChanged(object sender, EventArgs e)
                {
                    settings.CopyTemplate = txtCopy.Text;
                    Save();
                }*/

        private void TrackBar_Volume_Scroll(object sender, EventArgs e)
        {
            settings.AudioVolumen = ((TrackBar)sender).Value;
            Save();
        }

        private void Btn_TestVol_Click(object sender, EventArgs e)
        {
            speech.Volume = settings.AudioVolumen;
            speech.SpeakAsync("Probando el volumen del Locutor.");
        }

        private void CbxAutoMonitor_CheckedChanged(object sender, EventArgs e)
        {
            settings.AutoSTART = ((CheckBox)sender).Checked;
            Save();
        }

        private void CbxCodex_CheckedChanged(object sender, EventArgs e)
        {
            settings.IncludeCodex = ((CheckBox)sender).Checked;
            Save();
        }

        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            BulkChangeInProgress = true;
            foreach (var checkBox in TabControl.SelectedTab.Controls.OfType<CheckBox>())
            {
                checkBox.Checked = true;
            }
            BulkChangeInProgress = false;
            Save();
        }

        private void BtnSelectNone_Click(object sender, EventArgs e)
        {
            BulkChangeInProgress = true;
            foreach (var checkBox in TabControl.SelectedTab.Controls.OfType<CheckBox>())
            {
                checkBox.Checked = false;
            }
            BulkChangeInProgress = false;
            Save();
        }

        private void Save()
        {
            if (!BulkChangeInProgress)
            {
                BulkChangeInProgress = true;

                foreach (var pa in this.tabScan.Controls.OfType<PanelAlerta>())
                { pa.Update(alertas); }
                foreach (var pa in this.tabSignal.Controls.OfType<PanelAlerta>())
                { pa.Update(alertas); }
                foreach (var pa in this.tabFSS.Controls.OfType<PanelAlerta>())
                { pa.Update(alertas); }

                settings.Alertas = JsonConvert.SerializeObject(alertas.n);
                settings.Save();
                
                BulkChangeInProgress = false;
            }
        }

        private void ConfiguracionFrm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox_TTS_Enter(object sender, EventArgs e)
        {

        }

        private void cbxBeta_CheckedChanged(object sender, EventArgs e)
        {
            settings.JournalBeta = ((CheckBox)sender).Checked;
            if (settings.JournalBeta)
            {
                settings.JournalName = "JournalBeta.????????????.??.log";
                Save();
            }
            else
            {
                settings.JournalName = "Journal.????????????.??.log";
                Save();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Guardar y Salir
            Save();
            Close();
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            // Cancelar cambios y Salir
            Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var pa in TabControl.SelectedTab.Controls.OfType<PanelAlerta>())
            {
                pa.Checked(((CheckBox)sender).Checked);
            }
        }

        private void trackBar_Transparencia_Scroll(object sender, EventArgs e)
        {
            settings.Opacidad = ((TrackBar)sender).Value;
            Save();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!Loading)
            {
                basi.OpenNotifyForm("Prueba de Notificaciones\r\ndos líneas.", 3000);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            foreach (var pa in TabControl.SelectedTab.Controls.OfType<PanelAlerta>())
            {
                pa.Checked(((CheckBox)sender).Checked);
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            //Mouse.Capture(this);
            //Window mw = new Window();

            //System.Windows.Point pointToWindow = Mouse.GetPosition(mw);
            //System.Windows.Point pointToScreen = PointToScreen(pointToWindow);
            //lblPos.Text = "X: " + pointToScreen.X +
            //    "\n" +
            //    "Y: " + pointToScreen.Y;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Save();
        }

        PopupNotifier popupNotifier;
        private void button4_Click(object sender, EventArgs e)
        {
            popupNotifier = new PopupNotifier();
            popupNotifier.TitleText = "This is the notification title";
            popupNotifier.ContentText = "This is the notification text";
            popupNotifier.Popup();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            var t = Task.Run(() => 
            {
                basi.OpenNotifyForm("Prueba de Notificaciones", 3000);
                //Console.WriteLine("Task thread ID: {0}",
                //   Thread.CurrentThread.ManagedThreadId);
            });
            //t.Wait();
        }
    }
}
