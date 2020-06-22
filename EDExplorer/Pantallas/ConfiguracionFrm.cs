using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Windows.Forms;
using EDExplorer.Pantallas;
using System.Collections.Generic;

namespace EDExplorer
{
    public partial class ConfiguracionFrm : Form
    {
        private Properties.Settings settings;
        private bool Loading;
        private bool BulkChangeInProgress;
        private SpeechSynthesizer speech;
        private Alertas alertas;

        public ConfiguracionFrm(SpeechSynthesizer s, Alertas a)
        {
            InitializeComponent();
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            settings = Properties.Settings.Default;
            this.speech = s;
            this.alertas = a;
        }

        private void ConfiguracionFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //mainForm.settingsOpen = false;

        }

        private void ConfiguracionFrm_Load(object sender, EventArgs e)
        {
            Loading = true;
            BulkChangeInProgress = true;
            cbx_VeryInteresting.Checked = settings.VeryInteresting;
            cbxToast.Checked = settings.activarNotificaciones;
            cbxTts.Checked = settings.activarAudio;
            trackBar_Volume.Value = settings.TTSVolume;
            trackBar_Volume.BackColor = this.BackColor;
            btn_TestVol.Enabled = settings.activarAudio;
            cbxAutoMonitor.Checked = settings.AutoMonitor;
            cbxAutoRead.Checked = settings.AutoRead;
            cbxCodex.Checked = settings.IncludeCodex;
            cbxBeta.Checked = settings.JournalBeta;

            PanelAlerta pa;
            int n = 1;

            foreach (KeyValuePair<Alerta, DetallesAlerta> alerta in alertas.n)
            {
                pa = new PanelAlerta(alerta);
                this.tabAlertas.Controls.Add(pa);
                pa.Location = new System.Drawing.Point(5, 5 + pa.Height * n);
                pa.Name = "panel1";
                pa.Size = new System.Drawing.Size(pa.Width, pa.Height);
                pa.TabIndex = 10 * n ;
                n++;
            }

            Loading = false;
            BulkChangeInProgress = false;
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
            if (!Loading && settings.activarNotificaciones)
            {
                NotifyFrm notifyFrm = new NotifyFrm("Mostrar Notificaciones");
                notifyFrm.Show(5000);
            }
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
            settings.TTSVolume = ((TrackBar)sender).Value;
            Save();
        }

        private void Btn_TestVol_Click(object sender, EventArgs e)
        {
            speech.Volume = settings.TTSVolume;
            speech.SpeakAsync("Probando el volumen del Locutor.");
        }

        private void CbxAutoRead_CheckedChanged(object sender, EventArgs e)
        {
            settings.AutoRead = ((CheckBox)sender).Checked;
            Save();
        }

        private void CbxAutoMonitor_CheckedChanged(object sender, EventArgs e)
        {
            settings.AutoMonitor = ((CheckBox)sender).Checked;
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
                settings.Save();
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
            BulkChangeInProgress = true;
            //foreach (var checkBox in TabControl.SelectedTab.Controls.OfType<CheckBox>())
            foreach (var pa in this.tabAlertas.Controls.OfType<PanelAlerta>())
            {
                pa.Update(alertas);
            }
            BulkChangeInProgress = false;

            settings.Alertas = JsonConvert.SerializeObject(alertas.n);
            //settings.Save();
            Save();
            this.Close();
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var pa in TabControl.SelectedTab.Controls.OfType<PanelAlerta>())
            {
                pa.Checked(((CheckBox)sender).Checked);
            }
        }
    }
}
