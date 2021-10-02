namespace EDExplorer
{
    partial class ConfiguracionFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfiguracionFrm));
            this.cbxCodex = new System.Windows.Forms.CheckBox();
            this.cbx_VeryInteresting = new System.Windows.Forms.CheckBox();
            this.groupBox_misc = new System.Windows.Forms.GroupBox();
            this.cbxBeta = new System.Windows.Forms.CheckBox();
            this.cbxToast = new System.Windows.Forms.CheckBox();
            this.cbxTts = new System.Windows.Forms.CheckBox();
            this.tipCopy = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox_TTS = new System.Windows.Forms.GroupBox();
            this.btn_TestVol = new System.Windows.Forms.Button();
            this.trackBar_Volume = new System.Windows.Forms.TrackBar();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.tabFSS = new System.Windows.Forms.TabPage();
            this.tabScan = new System.Windows.Forms.TabPage();
            this.checkBoxTodos = new System.Windows.Forms.CheckBox();
            this.tabSignal = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbx_idioma = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.trackBar_Transparencia = new System.Windows.Forms.TrackBar();
            this.tabOculta = new System.Windows.Forms.TabPage();
            this.button4 = new System.Windows.Forms.Button();
            this.lblPos = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonAceptar = new System.Windows.Forms.Button();
            this.buttonCancelar = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox_misc.SuspendLayout();
            this.groupBox_TTS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Volume)).BeginInit();
            this.TabControl.SuspendLayout();
            this.tabScan.SuspendLayout();
            this.tabSignal.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Transparencia)).BeginInit();
            this.tabOculta.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxCodex
            // 
            resources.ApplyResources(this.cbxCodex, "cbxCodex");
            this.cbxCodex.ForeColor = System.Drawing.Color.Black;
            this.cbxCodex.Name = "cbxCodex";
            this.tipCopy.SetToolTip(this.cbxCodex, resources.GetString("cbxCodex.ToolTip"));
            this.cbxCodex.UseVisualStyleBackColor = true;
            this.cbxCodex.CheckedChanged += new System.EventHandler(this.CbxCodex_CheckedChanged);
            // 
            // cbx_VeryInteresting
            // 
            resources.ApplyResources(this.cbx_VeryInteresting, "cbx_VeryInteresting");
            this.cbx_VeryInteresting.ForeColor = System.Drawing.Color.Black;
            this.cbx_VeryInteresting.Name = "cbx_VeryInteresting";
            this.tipCopy.SetToolTip(this.cbx_VeryInteresting, resources.GetString("cbx_VeryInteresting.ToolTip"));
            this.cbx_VeryInteresting.UseVisualStyleBackColor = true;
            this.cbx_VeryInteresting.CheckedChanged += new System.EventHandler(this.Cbx_VeryInteresting_CheckedChanged);
            // 
            // groupBox_misc
            // 
            resources.ApplyResources(this.groupBox_misc, "groupBox_misc");
            this.groupBox_misc.Controls.Add(this.cbxBeta);
            this.groupBox_misc.Name = "groupBox_misc";
            this.groupBox_misc.TabStop = false;
            this.tipCopy.SetToolTip(this.groupBox_misc, resources.GetString("groupBox_misc.ToolTip"));
            // 
            // cbxBeta
            // 
            resources.ApplyResources(this.cbxBeta, "cbxBeta");
            this.cbxBeta.Name = "cbxBeta";
            this.tipCopy.SetToolTip(this.cbxBeta, resources.GetString("cbxBeta.ToolTip"));
            this.cbxBeta.UseVisualStyleBackColor = true;
            this.cbxBeta.CheckedChanged += new System.EventHandler(this.cbxBeta_CheckedChanged);
            // 
            // cbxToast
            // 
            resources.ApplyResources(this.cbxToast, "cbxToast");
            this.cbxToast.Name = "cbxToast";
            this.tipCopy.SetToolTip(this.cbxToast, resources.GetString("cbxToast.ToolTip"));
            this.cbxToast.UseVisualStyleBackColor = true;
            this.cbxToast.CheckedChanged += new System.EventHandler(this.CbxToast_CheckedChanged);
            // 
            // cbxTts
            // 
            resources.ApplyResources(this.cbxTts, "cbxTts");
            this.cbxTts.Name = "cbxTts";
            this.tipCopy.SetToolTip(this.cbxTts, resources.GetString("cbxTts.ToolTip"));
            this.cbxTts.UseVisualStyleBackColor = true;
            this.cbxTts.CheckedChanged += new System.EventHandler(this.CbxTts_CheckedChanged);
            // 
            // tipCopy
            // 
            this.tipCopy.ShowAlways = true;
            // 
            // groupBox_TTS
            // 
            resources.ApplyResources(this.groupBox_TTS, "groupBox_TTS");
            this.groupBox_TTS.Controls.Add(this.btn_TestVol);
            this.groupBox_TTS.Controls.Add(this.trackBar_Volume);
            this.groupBox_TTS.Name = "groupBox_TTS";
            this.groupBox_TTS.TabStop = false;
            this.tipCopy.SetToolTip(this.groupBox_TTS, resources.GetString("groupBox_TTS.ToolTip"));
            this.groupBox_TTS.Enter += new System.EventHandler(this.groupBox_TTS_Enter);
            // 
            // btn_TestVol
            // 
            resources.ApplyResources(this.btn_TestVol, "btn_TestVol");
            this.btn_TestVol.Name = "btn_TestVol";
            this.tipCopy.SetToolTip(this.btn_TestVol, resources.GetString("btn_TestVol.ToolTip"));
            this.btn_TestVol.UseVisualStyleBackColor = true;
            this.btn_TestVol.Click += new System.EventHandler(this.Btn_TestVol_Click);
            // 
            // trackBar_Volume
            // 
            resources.ApplyResources(this.trackBar_Volume, "trackBar_Volume");
            this.trackBar_Volume.BackColor = System.Drawing.Color.White;
            this.trackBar_Volume.LargeChange = 20;
            this.trackBar_Volume.Maximum = 100;
            this.trackBar_Volume.Name = "trackBar_Volume";
            this.trackBar_Volume.TickFrequency = 10;
            this.trackBar_Volume.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tipCopy.SetToolTip(this.trackBar_Volume, resources.GetString("trackBar_Volume.ToolTip"));
            this.trackBar_Volume.Scroll += new System.EventHandler(this.TrackBar_Volume_Scroll);
            // 
            // TabControl
            // 
            resources.ApplyResources(this.TabControl, "TabControl");
            this.TabControl.Controls.Add(this.tabFSS);
            this.TabControl.Controls.Add(this.tabScan);
            this.TabControl.Controls.Add(this.tabSignal);
            this.TabControl.Controls.Add(this.tabPage3);
            this.TabControl.Controls.Add(this.tabOculta);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.tipCopy.SetToolTip(this.TabControl, resources.GetString("TabControl.ToolTip"));
            // 
            // tabFSS
            // 
            resources.ApplyResources(this.tabFSS, "tabFSS");
            this.tabFSS.Name = "tabFSS";
            this.tipCopy.SetToolTip(this.tabFSS, resources.GetString("tabFSS.ToolTip"));
            this.tabFSS.UseVisualStyleBackColor = true;
            // 
            // tabScan
            // 
            resources.ApplyResources(this.tabScan, "tabScan");
            this.tabScan.BackColor = System.Drawing.Color.Transparent;
            this.tabScan.Controls.Add(this.checkBoxTodos);
            this.tabScan.Name = "tabScan";
            this.tipCopy.SetToolTip(this.tabScan, resources.GetString("tabScan.ToolTip"));
            this.tabScan.UseVisualStyleBackColor = true;
            // 
            // checkBoxTodos
            // 
            resources.ApplyResources(this.checkBoxTodos, "checkBoxTodos");
            this.checkBoxTodos.Name = "checkBoxTodos";
            this.tipCopy.SetToolTip(this.checkBoxTodos, resources.GetString("checkBoxTodos.ToolTip"));
            this.checkBoxTodos.UseVisualStyleBackColor = true;
            this.checkBoxTodos.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // tabSignal
            // 
            resources.ApplyResources(this.tabSignal, "tabSignal");
            this.tabSignal.Controls.Add(this.checkBox1);
            this.tabSignal.Name = "tabSignal";
            this.tipCopy.SetToolTip(this.tabSignal, resources.GetString("tabSignal.ToolTip"));
            this.tabSignal.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.tipCopy.SetToolTip(this.checkBox1, resources.GetString("checkBox1.ToolTip"));
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // tabPage3
            // 
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.groupBox_TTS);
            this.tabPage3.Controls.Add(this.cbxTts);
            this.tabPage3.Controls.Add(this.groupBox_misc);
            this.tabPage3.Controls.Add(this.cbxToast);
            this.tabPage3.Name = "tabPage3";
            this.tipCopy.SetToolTip(this.tabPage3, resources.GetString("tabPage3.ToolTip"));
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.cbx_idioma);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.tipCopy.SetToolTip(this.groupBox2, resources.GetString("groupBox2.ToolTip"));
            // 
            // cbx_idioma
            // 
            resources.ApplyResources(this.cbx_idioma, "cbx_idioma");
            this.cbx_idioma.FormattingEnabled = true;
            this.cbx_idioma.Name = "cbx_idioma";
            this.tipCopy.SetToolTip(this.cbx_idioma, resources.GetString("cbx_idioma.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.tipCopy.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.trackBar_Transparencia);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.tipCopy.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.tipCopy.SetToolTip(this.button1, resources.GetString("button1.ToolTip"));
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // trackBar_Transparencia
            // 
            resources.ApplyResources(this.trackBar_Transparencia, "trackBar_Transparencia");
            this.trackBar_Transparencia.BackColor = System.Drawing.Color.White;
            this.trackBar_Transparencia.LargeChange = 20;
            this.trackBar_Transparencia.Maximum = 100;
            this.trackBar_Transparencia.Name = "trackBar_Transparencia";
            this.trackBar_Transparencia.TickFrequency = 10;
            this.trackBar_Transparencia.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tipCopy.SetToolTip(this.trackBar_Transparencia, resources.GetString("trackBar_Transparencia.ToolTip"));
            this.trackBar_Transparencia.Scroll += new System.EventHandler(this.trackBar_Transparencia_Scroll);
            // 
            // tabOculta
            // 
            resources.ApplyResources(this.tabOculta, "tabOculta");
            this.tabOculta.BackColor = System.Drawing.Color.White;
            this.tabOculta.Controls.Add(this.button4);
            this.tabOculta.Controls.Add(this.lblPos);
            this.tabOculta.Controls.Add(this.button2);
            this.tabOculta.Controls.Add(this.cbx_VeryInteresting);
            this.tabOculta.Controls.Add(this.cbxCodex);
            this.tabOculta.Name = "tabOculta";
            this.tipCopy.SetToolTip(this.tabOculta, resources.GetString("tabOculta.ToolTip"));
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.tipCopy.SetToolTip(this.button4, resources.GetString("button4.ToolTip"));
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // lblPos
            // 
            resources.ApplyResources(this.lblPos, "lblPos");
            this.lblPos.Name = "lblPos";
            this.tipCopy.SetToolTip(this.lblPos, resources.GetString("lblPos.ToolTip"));
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.tipCopy.SetToolTip(this.button2, resources.GetString("button2.ToolTip"));
            this.button2.UseVisualStyleBackColor = true;
            // 
            // buttonAceptar
            // 
            resources.ApplyResources(this.buttonAceptar, "buttonAceptar");
            this.buttonAceptar.Name = "buttonAceptar";
            this.tipCopy.SetToolTip(this.buttonAceptar, resources.GetString("buttonAceptar.ToolTip"));
            this.buttonAceptar.UseVisualStyleBackColor = true;
            this.buttonAceptar.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonCancelar
            // 
            resources.ApplyResources(this.buttonCancelar, "buttonCancelar");
            this.buttonCancelar.Name = "buttonCancelar";
            this.tipCopy.SetToolTip(this.buttonCancelar, resources.GetString("buttonCancelar.ToolTip"));
            this.buttonCancelar.UseVisualStyleBackColor = true;
            this.buttonCancelar.Click += new System.EventHandler(this.buttonCancelar_Click);
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.tipCopy.SetToolTip(this.button3, resources.GetString("button3.ToolTip"));
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ConfiguracionFrm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buttonCancelar);
            this.Controls.Add(this.buttonAceptar);
            this.Controls.Add(this.TabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfiguracionFrm";
            this.tipCopy.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfiguracionFrm_FormClosed);
            this.Load += new System.EventHandler(this.ConfiguracionFrm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ConfiguracionFrm_Paint);
            this.groupBox_misc.ResumeLayout(false);
            this.groupBox_misc.PerformLayout();
            this.groupBox_TTS.ResumeLayout(false);
            this.groupBox_TTS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Volume)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.tabScan.ResumeLayout(false);
            this.tabScan.PerformLayout();
            this.tabSignal.ResumeLayout(false);
            this.tabSignal.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Transparencia)).EndInit();
            this.tabOculta.ResumeLayout(false);
            this.tabOculta.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox cbx_VeryInteresting;
        private System.Windows.Forms.GroupBox groupBox_misc;
        private System.Windows.Forms.CheckBox cbxTts;
        private System.Windows.Forms.CheckBox cbxToast;
        private System.Windows.Forms.ToolTip tipCopy;
        private System.Windows.Forms.GroupBox groupBox_TTS;
        private System.Windows.Forms.Button btn_TestVol;
        private System.Windows.Forms.TrackBar trackBar_Volume;
        private System.Windows.Forms.CheckBox cbxCodex;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabOculta;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox cbxBeta;
        private System.Windows.Forms.TabPage tabScan;
        private System.Windows.Forms.Button buttonAceptar;
        private System.Windows.Forms.Button buttonCancelar;
        private System.Windows.Forms.CheckBox checkBoxTodos;
        private System.Windows.Forms.TabPage tabSignal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TrackBar trackBar_Transparencia;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TabPage tabFSS;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label lblPos;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox cbx_idioma;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}
