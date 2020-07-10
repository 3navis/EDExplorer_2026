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
            this.cbxCodex = new System.Windows.Forms.CheckBox();
            this.cbx_VeryInteresting = new System.Windows.Forms.CheckBox();
            this.groupBox_misc = new System.Windows.Forms.GroupBox();
            this.cbxBeta = new System.Windows.Forms.CheckBox();
            this.cbxAutoMonitor = new System.Windows.Forms.CheckBox();
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.trackBar_Transparencia = new System.Windows.Forms.TrackBar();
            this.tabOculta = new System.Windows.Forms.TabPage();
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
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Transparencia)).BeginInit();
            this.tabOculta.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxCodex
            // 
            this.cbxCodex.AutoSize = true;
            this.cbxCodex.ForeColor = System.Drawing.Color.Black;
            this.cbxCodex.Location = new System.Drawing.Point(34, 28);
            this.cbxCodex.Margin = new System.Windows.Forms.Padding(4);
            this.cbxCodex.Name = "cbxCodex";
            this.cbxCodex.Size = new System.Drawing.Size(178, 21);
            this.cbxCodex.TabIndex = 20;
            this.cbxCodex.Text = "Descubrimientos Codex";
            this.cbxCodex.UseVisualStyleBackColor = true;
            this.cbxCodex.CheckedChanged += new System.EventHandler(this.CbxCodex_CheckedChanged);
            // 
            // cbx_VeryInteresting
            // 
            this.cbx_VeryInteresting.AutoSize = true;
            this.cbx_VeryInteresting.ForeColor = System.Drawing.Color.Black;
            this.cbx_VeryInteresting.Location = new System.Drawing.Point(34, 57);
            this.cbx_VeryInteresting.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_VeryInteresting.Name = "cbx_VeryInteresting";
            this.cbx_VeryInteresting.Size = new System.Drawing.Size(197, 21);
            this.cbx_VeryInteresting.TabIndex = 15;
            this.cbx_VeryInteresting.Text = "Notificar Criterios Multiples";
            this.cbx_VeryInteresting.UseVisualStyleBackColor = true;
            this.cbx_VeryInteresting.CheckedChanged += new System.EventHandler(this.Cbx_VeryInteresting_CheckedChanged);
            // 
            // groupBox_misc
            // 
            this.groupBox_misc.Controls.Add(this.cbxBeta);
            this.groupBox_misc.Controls.Add(this.cbxAutoMonitor);
            this.groupBox_misc.Location = new System.Drawing.Point(17, 20);
            this.groupBox_misc.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox_misc.Name = "groupBox_misc";
            this.groupBox_misc.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox_misc.Size = new System.Drawing.Size(259, 94);
            this.groupBox_misc.TabIndex = 1;
            this.groupBox_misc.TabStop = false;
            this.groupBox_misc.Text = "Diarios de Vuelo (Journal Logs)";
            // 
            // cbxBeta
            // 
            this.cbxBeta.AutoSize = true;
            this.cbxBeta.Location = new System.Drawing.Point(8, 58);
            this.cbxBeta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxBeta.Name = "cbxBeta";
            this.cbxBeta.Size = new System.Drawing.Size(143, 21);
            this.cbxBeta.TabIndex = 2;
            this.cbxBeta.Text = "Leer Journal Beta";
            this.cbxBeta.UseVisualStyleBackColor = true;
            this.cbxBeta.CheckedChanged += new System.EventHandler(this.cbxBeta_CheckedChanged);
            // 
            // cbxAutoMonitor
            // 
            this.cbxAutoMonitor.AutoSize = true;
            this.cbxAutoMonitor.Location = new System.Drawing.Point(8, 28);
            this.cbxAutoMonitor.Margin = new System.Windows.Forms.Padding(4);
            this.cbxAutoMonitor.Name = "cbxAutoMonitor";
            this.cbxAutoMonitor.Size = new System.Drawing.Size(183, 21);
            this.cbxAutoMonitor.TabIndex = 16;
            this.cbxAutoMonitor.Text = "START Monitor al Iniciar";
            this.tipCopy.SetToolTip(this.cbxAutoMonitor, "Automaticamente comienza el monitor de logs al iniciar");
            this.cbxAutoMonitor.UseVisualStyleBackColor = true;
            this.cbxAutoMonitor.CheckedChanged += new System.EventHandler(this.CbxAutoMonitor_CheckedChanged);
            // 
            // cbxToast
            // 
            this.cbxToast.AutoSize = true;
            this.cbxToast.Location = new System.Drawing.Point(315, 41);
            this.cbxToast.Margin = new System.Windows.Forms.Padding(4);
            this.cbxToast.Name = "cbxToast";
            this.cbxToast.Size = new System.Drawing.Size(170, 21);
            this.cbxToast.TabIndex = 8;
            this.cbxToast.Text = "Mostrar Notificaciones";
            this.cbxToast.UseVisualStyleBackColor = true;
            this.cbxToast.CheckedChanged += new System.EventHandler(this.CbxToast_CheckedChanged);
            // 
            // cbxTts
            // 
            this.cbxTts.AutoSize = true;
            this.cbxTts.Location = new System.Drawing.Point(315, 167);
            this.cbxTts.Margin = new System.Windows.Forms.Padding(4);
            this.cbxTts.Name = "cbxTts";
            this.cbxTts.Size = new System.Drawing.Size(125, 21);
            this.cbxTts.TabIndex = 9;
            this.cbxTts.Text = "Activar Locutor";
            this.cbxTts.UseVisualStyleBackColor = true;
            this.cbxTts.CheckedChanged += new System.EventHandler(this.CbxTts_CheckedChanged);
            // 
            // tipCopy
            // 
            this.tipCopy.ShowAlways = true;
            // 
            // groupBox_TTS
            // 
            this.groupBox_TTS.Controls.Add(this.btn_TestVol);
            this.groupBox_TTS.Controls.Add(this.trackBar_Volume);
            this.groupBox_TTS.Location = new System.Drawing.Point(327, 196);
            this.groupBox_TTS.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox_TTS.Name = "groupBox_TTS";
            this.groupBox_TTS.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox_TTS.Size = new System.Drawing.Size(242, 85);
            this.groupBox_TTS.TabIndex = 2;
            this.groupBox_TTS.TabStop = false;
            this.groupBox_TTS.Text = "Volumen";
            this.groupBox_TTS.Enter += new System.EventHandler(this.groupBox_TTS_Enter);
            // 
            // btn_TestVol
            // 
            this.btn_TestVol.Location = new System.Drawing.Point(165, 33);
            this.btn_TestVol.Margin = new System.Windows.Forms.Padding(4);
            this.btn_TestVol.Name = "btn_TestVol";
            this.btn_TestVol.Size = new System.Drawing.Size(64, 28);
            this.btn_TestVol.TabIndex = 1;
            this.btn_TestVol.Text = "Probar";
            this.btn_TestVol.UseVisualStyleBackColor = true;
            this.btn_TestVol.Click += new System.EventHandler(this.Btn_TestVol_Click);
            // 
            // trackBar_Volume
            // 
            this.trackBar_Volume.BackColor = System.Drawing.Color.White;
            this.trackBar_Volume.LargeChange = 20;
            this.trackBar_Volume.Location = new System.Drawing.Point(22, 23);
            this.trackBar_Volume.Margin = new System.Windows.Forms.Padding(4);
            this.trackBar_Volume.Maximum = 100;
            this.trackBar_Volume.Name = "trackBar_Volume";
            this.trackBar_Volume.Size = new System.Drawing.Size(123, 56);
            this.trackBar_Volume.TabIndex = 0;
            this.trackBar_Volume.TickFrequency = 10;
            this.trackBar_Volume.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar_Volume.Scroll += new System.EventHandler(this.TrackBar_Volume_Scroll);
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.tabFSS);
            this.TabControl.Controls.Add(this.tabScan);
            this.TabControl.Controls.Add(this.tabSignal);
            this.TabControl.Controls.Add(this.tabPage3);
            this.TabControl.Controls.Add(this.tabOculta);
            this.TabControl.Location = new System.Drawing.Point(12, 12);
            this.TabControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(855, 409);
            this.TabControl.TabIndex = 3;
            // 
            // tabFSS
            // 
            this.tabFSS.Location = new System.Drawing.Point(4, 25);
            this.tabFSS.Name = "tabFSS";
            this.tabFSS.Padding = new System.Windows.Forms.Padding(3);
            this.tabFSS.Size = new System.Drawing.Size(847, 380);
            this.tabFSS.TabIndex = 5;
            this.tabFSS.Text = "FSS";
            this.tabFSS.UseVisualStyleBackColor = true;
            // 
            // tabScan
            // 
            this.tabScan.AutoScroll = true;
            this.tabScan.BackColor = System.Drawing.Color.Transparent;
            this.tabScan.Controls.Add(this.checkBoxTodos);
            this.tabScan.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tabScan.Location = new System.Drawing.Point(4, 25);
            this.tabScan.Name = "tabScan";
            this.tabScan.Padding = new System.Windows.Forms.Padding(3);
            this.tabScan.Size = new System.Drawing.Size(847, 380);
            this.tabScan.TabIndex = 3;
            this.tabScan.Text = "Exploración";
            this.tabScan.UseVisualStyleBackColor = true;
            // 
            // checkBoxTodos
            // 
            this.checkBoxTodos.AutoSize = true;
            this.checkBoxTodos.Location = new System.Drawing.Point(38, 6);
            this.checkBoxTodos.Name = "checkBoxTodos";
            this.checkBoxTodos.Size = new System.Drawing.Size(126, 21);
            this.checkBoxTodos.TabIndex = 27;
            this.checkBoxTodos.Text = "Cambiar Todos";
            this.checkBoxTodos.UseVisualStyleBackColor = true;
            this.checkBoxTodos.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // tabSignal
            // 
            this.tabSignal.AutoScroll = true;
            this.tabSignal.Controls.Add(this.checkBox1);
            this.tabSignal.Location = new System.Drawing.Point(4, 25);
            this.tabSignal.Name = "tabSignal";
            this.tabSignal.Padding = new System.Windows.Forms.Padding(3);
            this.tabSignal.Size = new System.Drawing.Size(847, 380);
            this.tabSignal.TabIndex = 4;
            this.tabSignal.Text = "Señales";
            this.tabSignal.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(38, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(126, 21);
            this.checkBox1.TabIndex = 28;
            this.checkBox1.Text = "Cambiar Todos";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.groupBox_TTS);
            this.tabPage3.Controls.Add(this.cbxTts);
            this.tabPage3.Controls.Add(this.groupBox_misc);
            this.tabPage3.Controls.Add(this.cbxToast);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Size = new System.Drawing.Size(847, 380);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Varios";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.trackBar_Transparencia);
            this.groupBox1.Location = new System.Drawing.Point(327, 70);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(242, 85);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opacidad";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(165, 30);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "Probar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // trackBar_Transparencia
            // 
            this.trackBar_Transparencia.BackColor = System.Drawing.Color.White;
            this.trackBar_Transparencia.LargeChange = 20;
            this.trackBar_Transparencia.Location = new System.Drawing.Point(22, 23);
            this.trackBar_Transparencia.Margin = new System.Windows.Forms.Padding(4);
            this.trackBar_Transparencia.Maximum = 100;
            this.trackBar_Transparencia.Name = "trackBar_Transparencia";
            this.trackBar_Transparencia.Size = new System.Drawing.Size(123, 56);
            this.trackBar_Transparencia.TabIndex = 0;
            this.trackBar_Transparencia.TickFrequency = 10;
            this.trackBar_Transparencia.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar_Transparencia.Scroll += new System.EventHandler(this.trackBar_Transparencia_Scroll);
            // 
            // tabOculta
            // 
            this.tabOculta.BackColor = System.Drawing.Color.White;
            this.tabOculta.Controls.Add(this.lblPos);
            this.tabOculta.Controls.Add(this.button2);
            this.tabOculta.Controls.Add(this.cbx_VeryInteresting);
            this.tabOculta.Controls.Add(this.cbxCodex);
            this.tabOculta.Location = new System.Drawing.Point(4, 25);
            this.tabOculta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabOculta.Name = "tabOculta";
            this.tabOculta.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabOculta.Size = new System.Drawing.Size(847, 380);
            this.tabOculta.TabIndex = 0;
            this.tabOculta.Text = "Exploración (old)";
            // 
            // lblPos
            // 
            this.lblPos.AutoSize = true;
            this.lblPos.Location = new System.Drawing.Point(546, 56);
            this.lblPos.Name = "lblPos";
            this.lblPos.Size = new System.Drawing.Size(46, 17);
            this.lblPos.TabIndex = 24;
            this.lblPos.Text = "label1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(464, 52);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(64, 28);
            this.button2.TabIndex = 23;
            this.button2.Text = "Mover";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // buttonAceptar
            // 
            this.buttonAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAceptar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAceptar.Location = new System.Drawing.Point(655, 430);
            this.buttonAceptar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonAceptar.Name = "buttonAceptar";
            this.buttonAceptar.Size = new System.Drawing.Size(88, 28);
            this.buttonAceptar.TabIndex = 25;
            this.buttonAceptar.Text = "Aceptar";
            this.buttonAceptar.UseVisualStyleBackColor = true;
            this.buttonAceptar.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonCancelar
            // 
            this.buttonCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancelar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancelar.Location = new System.Drawing.Point(760, 430);
            this.buttonCancelar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(88, 28);
            this.buttonCancelar.TabIndex = 26;
            this.buttonCancelar.Text = "Cancelar";
            this.buttonCancelar.UseVisualStyleBackColor = true;
            this.buttonCancelar.Click += new System.EventHandler(this.buttonCancelar_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(552, 430);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(88, 28);
            this.button3.TabIndex = 27;
            this.button3.Text = "Aplicar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ConfiguracionFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(874, 471);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buttonCancelar);
            this.Controls.Add(this.buttonAceptar);
            this.Controls.Add(this.TabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfiguracionFrm";
            this.Text = "EDExplorer Configuración";
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
        private System.Windows.Forms.CheckBox cbxAutoMonitor;
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
    }
}
