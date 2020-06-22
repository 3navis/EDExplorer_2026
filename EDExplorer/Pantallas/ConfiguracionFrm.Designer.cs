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
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectNone = new System.Windows.Forms.Button();
            this.cbxCodex = new System.Windows.Forms.CheckBox();
            this.cbx_VeryInteresting = new System.Windows.Forms.CheckBox();
            this.groupBox_misc = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cbxBeta = new System.Windows.Forms.CheckBox();
            this.cbxAutoMonitor = new System.Windows.Forms.CheckBox();
            this.cbxAutoRead = new System.Windows.Forms.CheckBox();
            this.cbxToast = new System.Windows.Forms.CheckBox();
            this.cbxTts = new System.Windows.Forms.CheckBox();
            this.tipCopy = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox_TTS = new System.Windows.Forms.GroupBox();
            this.btn_TestVol = new System.Windows.Forms.Button();
            this.trackBar_Volume = new System.Windows.Forms.TrackBar();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.tabAlertas = new System.Windows.Forms.TabPage();
            this.checkBoxTodos = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabOculta = new System.Windows.Forms.TabPage();
            this.buttonAceptar = new System.Windows.Forms.Button();
            this.buttonCancelar = new System.Windows.Forms.Button();
            this.tabSignals = new System.Windows.Forms.TabPage();
            this.groupBox_misc.SuspendLayout();
            this.groupBox_TTS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Volume)).BeginInit();
            this.TabControl.SuspendLayout();
            this.tabAlertas.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabOculta.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(295, 345);
            this.btnSelectAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(114, 28);
            this.btnSelectAll.TabIndex = 22;
            this.btnSelectAll.Text = "Marcar Todos";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.BtnSelectAll_Click);
            // 
            // btnSelectNone
            // 
            this.btnSelectNone.Location = new System.Drawing.Point(419, 345);
            this.btnSelectNone.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(100, 28);
            this.btnSelectNone.TabIndex = 21;
            this.btnSelectNone.Text = "Ninguno";
            this.btnSelectNone.UseVisualStyleBackColor = true;
            this.btnSelectNone.Click += new System.EventHandler(this.BtnSelectNone_Click);
            // 
            // cbxCodex
            // 
            this.cbxCodex.AutoSize = true;
            this.cbxCodex.ForeColor = System.Drawing.Color.Black;
            this.cbxCodex.Location = new System.Drawing.Point(19, 302);
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
            this.cbx_VeryInteresting.Location = new System.Drawing.Point(253, 300);
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
            this.groupBox_misc.Controls.Add(this.textBox1);
            this.groupBox_misc.Controls.Add(this.cbxBeta);
            this.groupBox_misc.Controls.Add(this.cbxAutoMonitor);
            this.groupBox_misc.Controls.Add(this.cbxAutoRead);
            this.groupBox_misc.Location = new System.Drawing.Point(17, 20);
            this.groupBox_misc.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox_misc.Name = "groupBox_misc";
            this.groupBox_misc.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox_misc.Size = new System.Drawing.Size(259, 169);
            this.groupBox_misc.TabIndex = 1;
            this.groupBox_misc.TabStop = false;
            this.groupBox_misc.Text = "Diarios de Vuelo (Journal Logs)";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ForeColor = System.Drawing.Color.DarkGray;
            this.textBox1.Location = new System.Drawing.Point(141, 52);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 15);
            this.textBox1.TabIndex = 17;
            this.textBox1.Text = "x ultimos ficheros";
            // 
            // cbxBeta
            // 
            this.cbxBeta.AutoSize = true;
            this.cbxBeta.Location = new System.Drawing.Point(8, 130);
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
            this.cbxAutoMonitor.Location = new System.Drawing.Point(8, 80);
            this.cbxAutoMonitor.Margin = new System.Windows.Forms.Padding(4);
            this.cbxAutoMonitor.Name = "cbxAutoMonitor";
            this.cbxAutoMonitor.Size = new System.Drawing.Size(183, 21);
            this.cbxAutoMonitor.TabIndex = 16;
            this.cbxAutoMonitor.Text = "START Monitor al Iniciar";
            this.tipCopy.SetToolTip(this.cbxAutoMonitor, "Automaticamente comienza el monitor de logs al iniciar");
            this.cbxAutoMonitor.UseVisualStyleBackColor = true;
            this.cbxAutoMonitor.CheckedChanged += new System.EventHandler(this.CbxAutoMonitor_CheckedChanged);
            // 
            // cbxAutoRead
            // 
            this.cbxAutoRead.AutoSize = true;
            this.cbxAutoRead.Location = new System.Drawing.Point(8, 50);
            this.cbxAutoRead.Margin = new System.Windows.Forms.Padding(4);
            this.cbxAutoRead.Name = "cbxAutoRead";
            this.cbxAutoRead.Size = new System.Drawing.Size(115, 21);
            this.cbxAutoRead.TabIndex = 15;
            this.cbxAutoRead.Text = "Leer al Iniciar";
            this.tipCopy.SetToolTip(this.cbxAutoRead, "Automaticamente lee los logs al iniciar");
            this.cbxAutoRead.UseVisualStyleBackColor = true;
            this.cbxAutoRead.CheckedChanged += new System.EventHandler(this.CbxAutoRead_CheckedChanged);
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
            this.cbxTts.Location = new System.Drawing.Point(315, 70);
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
            this.groupBox_TTS.Location = new System.Drawing.Point(350, 98);
            this.groupBox_TTS.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox_TTS.Name = "groupBox_TTS";
            this.groupBox_TTS.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox_TTS.Size = new System.Drawing.Size(90, 91);
            this.groupBox_TTS.TabIndex = 2;
            this.groupBox_TTS.TabStop = false;
            this.groupBox_TTS.Text = "Volumen";
            this.groupBox_TTS.Enter += new System.EventHandler(this.groupBox_TTS_Enter);
            // 
            // btn_TestVol
            // 
            this.btn_TestVol.Location = new System.Drawing.Point(8, 52);
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
            this.trackBar_Volume.Location = new System.Drawing.Point(-20, 0);
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
            this.TabControl.Controls.Add(this.tabAlertas);
            this.TabControl.Controls.Add(this.tabSignals);
            this.TabControl.Controls.Add(this.tabPage3);
            this.TabControl.Controls.Add(this.tabOculta);
            this.TabControl.Location = new System.Drawing.Point(12, 12);
            this.TabControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(664, 409);
            this.TabControl.TabIndex = 3;
            // 
            // tabAlertas
            // 
            this.tabAlertas.AutoScroll = true;
            this.tabAlertas.BackColor = System.Drawing.Color.Transparent;
            this.tabAlertas.Controls.Add(this.checkBoxTodos);
            this.tabAlertas.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tabAlertas.Location = new System.Drawing.Point(4, 25);
            this.tabAlertas.Name = "tabAlertas";
            this.tabAlertas.Padding = new System.Windows.Forms.Padding(3);
            this.tabAlertas.Size = new System.Drawing.Size(656, 380);
            this.tabAlertas.TabIndex = 3;
            this.tabAlertas.Text = "Exploración";
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
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox_TTS);
            this.tabPage3.Controls.Add(this.cbxTts);
            this.tabPage3.Controls.Add(this.groupBox_misc);
            this.tabPage3.Controls.Add(this.cbxToast);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Size = new System.Drawing.Size(656, 380);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Varios";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabOculta
            // 
            this.tabOculta.BackColor = System.Drawing.Color.White;
            this.tabOculta.Controls.Add(this.btnSelectNone);
            this.tabOculta.Controls.Add(this.btnSelectAll);
            this.tabOculta.Controls.Add(this.cbx_VeryInteresting);
            this.tabOculta.Controls.Add(this.cbxCodex);
            this.tabOculta.Location = new System.Drawing.Point(4, 25);
            this.tabOculta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabOculta.Name = "tabOculta";
            this.tabOculta.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabOculta.Size = new System.Drawing.Size(656, 380);
            this.tabOculta.TabIndex = 0;
            this.tabOculta.Text = "Exploración (old)";
            // 
            // buttonAceptar
            // 
            this.buttonAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAceptar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAceptar.Location = new System.Drawing.Point(463, 430);
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
            this.buttonCancelar.Location = new System.Drawing.Point(568, 430);
            this.buttonCancelar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(88, 28);
            this.buttonCancelar.TabIndex = 26;
            this.buttonCancelar.Text = "Cancelar";
            this.buttonCancelar.UseVisualStyleBackColor = true;
            this.buttonCancelar.Click += new System.EventHandler(this.buttonCancelar_Click);
            // 
            // tabSignals
            // 
            this.tabSignals.Location = new System.Drawing.Point(4, 25);
            this.tabSignals.Name = "tabSignals";
            this.tabSignals.Padding = new System.Windows.Forms.Padding(3);
            this.tabSignals.Size = new System.Drawing.Size(656, 380);
            this.tabSignals.TabIndex = 4;
            this.tabSignals.Text = "Señales";
            this.tabSignals.UseVisualStyleBackColor = true;
            // 
            // ConfiguracionFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(682, 471);
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
            this.tabAlertas.ResumeLayout(false);
            this.tabAlertas.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
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
        private System.Windows.Forms.CheckBox cbxAutoRead;
        private System.Windows.Forms.CheckBox cbxAutoMonitor;
        private System.Windows.Forms.CheckBox cbxCodex;
        private System.Windows.Forms.Button btnSelectNone;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabOculta;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox cbxBeta;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabPage tabAlertas;
        private System.Windows.Forms.Button buttonAceptar;
        private System.Windows.Forms.Button buttonCancelar;
        private System.Windows.Forms.CheckBox checkBoxTodos;
        private System.Windows.Forms.TabPage tabSignals;
    }
}
