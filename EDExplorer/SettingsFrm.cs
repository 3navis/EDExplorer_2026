using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Windows.Forms;


namespace EDExplorer
{
    public partial class SettingsFrm : Form
    {
        private Properties.Settings settings;
        //private EDExplorerFrm mainForm;
        private bool Loading;
        private bool BulkChangeInProgress;
        public SpeechSynthesizer speech;

        public SettingsFrm(SpeechSynthesizer s)
        {
            InitializeComponent();
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            settings = Properties.Settings.Default;
            this.speech = s;
        }

        private void SettingsFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //mainForm.settingsOpen = false;

        }

        private void SettingsFrm_Load(object sender, EventArgs e)
        {
            Loading = true;
            BulkChangeInProgress = true;
            cbx_LandWithTerra.Checked = settings.LandWithTerra;
            cbx_LandWithAtmo.Checked = settings.LandWithAtmo;
            cbx_LandHighG.Checked = settings.LandHighG;
            cbx_Jumporium.Checked = settings.Jumporium;
            cbx_CloseBinary.Checked = settings.CloseBinary;
            cbx_CloseOrbit.Checked = settings.CloseOrbit;
            cbx_CollidingBinary.Checked = settings.CollidingBinary;
            cbx_FastOrbit.Checked = settings.FastOrbit;
            cbx_FastRotate.Checked = settings.FastRotate;
            cbx_HighEccentric.Checked = settings.HighEccentric;
            cbx_NestedMoon.Checked = settings.NestedMoon;
            cbx_ShepherdMoon.Checked = settings.ShepherdMoon;
            cbx_TinyObject.Checked = settings.TinyObject;
            cbx_VeryInteresting.Checked = settings.VeryInteresting;
            cbxToast.Checked = settings.Notify;
            cbxTts.Checked = settings.TTS;
            cbx_LandLarge.Checked = settings.LandLarge;
            cbx_WideRing.Checked = settings.WideRing;
            trackBar_Volume.Value = settings.TTSVolume;
            trackBar_Volume.BackColor = this.BackColor;
            btn_TestVol.Enabled = settings.TTS;
            cbxRinghugger.Checked = settings.RingHugger;
            cbxLandRing.Checked = settings.RingLandable;
            cbxAutoMonitor.Checked = settings.AutoMonitor;
            cbxAutoRead.Checked = settings.AutoRead;
            cbxCodex.Checked = settings.IncludeCodex;
            cbxGold.Checked = settings.AllMaterialSystem;
            cbxBeta.Checked = settings.JournalBeta;
            Loading = false;
            BulkChangeInProgress = false;
        }

        private void Cbx_LandWithTerra_CheckedChanged(object sender, EventArgs e)
        {
            settings.LandWithTerra = ((CheckBox)sender).Checked;
            Save();
        }

        private void Cbx_LandWithAtmo_CheckedChanged(object sender, EventArgs e)
        {
            settings.LandWithAtmo = ((CheckBox)sender).Checked;
            Save();
        }

        private void Cbx_LandHighG_CheckedChanged(object sender, EventArgs e)
        {
            settings.LandHighG = ((CheckBox)sender).Checked;
            Save();
        }

        private void Cbx_CloseOrbit_CheckedChanged(object sender, EventArgs e)
        {
            settings.CloseOrbit = ((CheckBox)sender).Checked;
            Save();
        }

        private void Cbx_ShepherdMoon_CheckedChanged(object sender, EventArgs e)
        {
            settings.ShepherdMoon = ((CheckBox)sender).Checked;
            Save();
        }

        private void Cbx_CloseBinary_CheckedChanged(object sender, EventArgs e)
        {
            settings.CloseBinary = ((CheckBox)sender).Checked;
            Save();
        }

        private void Cbx_CollidingBinary_CheckedChanged(object sender, EventArgs e)
        {
            settings.CollidingBinary = ((CheckBox)sender).Checked;
            Save();
        }

        private void Cbx_NestedMoon_CheckedChanged(object sender, EventArgs e)
        {
            settings.NestedMoon = ((CheckBox)sender).Checked;
            Save();
        }

        private void Cbx_TinyObject_CheckedChanged(object sender, EventArgs e)
        {
            settings.TinyObject = ((CheckBox)sender).Checked;
            Save();
        }

        private void Cbx_FastRotate_CheckedChanged(object sender, EventArgs e)
        {
            settings.FastRotate = ((CheckBox)sender).Checked;
            Save();
        }

        private void Cbx_FastOrbit_CheckedChanged(object sender, EventArgs e)
        {
            settings.FastOrbit = ((CheckBox)sender).Checked;
            Save();
        }

        private void Cbx_HighEccentric_CheckedChanged(object sender, EventArgs e)
        {
            settings.HighEccentric = ((CheckBox)sender).Checked;
            Save();
        }

        private void Cbx_Jumporium_CheckedChanged(object sender, EventArgs e)
        {
            settings.Jumporium = ((CheckBox)sender).Checked;
            Save();
        }

        private void Cbx_VeryInteresting_CheckedChanged(object sender, EventArgs e)
        {
            settings.VeryInteresting = ((CheckBox)sender).Checked;
            Save();
        }

        private void CbxToast_CheckedChanged(object sender, EventArgs e)
        {
            settings.Notify = ((CheckBox)sender).Checked;
            Save();
            if (!Loading && settings.Notify)
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
            settings.TTS = cbxTts.Checked;
            Save();
        }

        /*        private void TxtCopy_TextChanged(object sender, EventArgs e)
                {
                    settings.CopyTemplate = txtCopy.Text;
                    Save();
                }*/

        private void Cbx_LandLarge_CheckedChanged(object sender, EventArgs e)
        {
            settings.LandLarge = ((CheckBox)sender).Checked;
            Save();
        }

        private void Cbx_WideRing_CheckedChanged(object sender, EventArgs e)
        {
            settings.WideRing = ((CheckBox)sender).Checked;
            Save();
        }

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

        private void CbxLandRing_CheckedChanged(object sender, EventArgs e)
        {
            settings.RingLandable = ((CheckBox)sender).Checked;
            Save();
        }

        private void CbxRinghugger_CheckedChanged(object sender, EventArgs e)
        {
            settings.RingHugger = ((CheckBox)sender).Checked;
            Save();
        }

        private void CbxGold_CheckedChanged(object sender, EventArgs e)
        {
            settings.AllMaterialSystem = ((CheckBox)sender).Checked;
            Save();
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

        private void SettingsFrm_Paint(object sender, PaintEventArgs e)
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

        private void btnReadAll_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
