namespace EDExplorer
{
    partial class SettingsFrm
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
            this.cbxGold = new System.Windows.Forms.CheckBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectNone = new System.Windows.Forms.Button();
            this.cbxCodex = new System.Windows.Forms.CheckBox();
            this.cbxLandRing = new System.Windows.Forms.CheckBox();
            this.cbxRinghugger = new System.Windows.Forms.CheckBox();
            this.cbx_WideRing = new System.Windows.Forms.CheckBox();
            this.cbx_LandLarge = new System.Windows.Forms.CheckBox();
            this.cbx_VeryInteresting = new System.Windows.Forms.CheckBox();
            this.cbx_Jumporium = new System.Windows.Forms.CheckBox();
            this.cbx_HighEccentric = new System.Windows.Forms.CheckBox();
            this.cbx_FastOrbit = new System.Windows.Forms.CheckBox();
            this.cbx_FastRotate = new System.Windows.Forms.CheckBox();
            this.cbx_TinyObject = new System.Windows.Forms.CheckBox();
            this.cbx_NestedMoon = new System.Windows.Forms.CheckBox();
            this.cbx_CollidingBinary = new System.Windows.Forms.CheckBox();
            this.cbx_CloseBinary = new System.Windows.Forms.CheckBox();
            this.cbx_ShepherdMoon = new System.Windows.Forms.CheckBox();
            this.cbx_LandHighG = new System.Windows.Forms.CheckBox();
            this.cbx_LandWithAtmo = new System.Windows.Forms.CheckBox();
            this.cbx_LandWithTerra = new System.Windows.Forms.CheckBox();
            this.cbx_CloseOrbit = new System.Windows.Forms.CheckBox();
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnReadAll = new System.Windows.Forms.Button();
            this.groupBox_misc.SuspendLayout();
            this.groupBox_TTS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Volume)).BeginInit();
            this.TabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxGold
            // 
            this.cbxGold.AutoSize = true;
            this.cbxGold.Location = new System.Drawing.Point(253, 272);
            this.cbxGold.Margin = new System.Windows.Forms.Padding(4);
            this.cbxGold.Name = "cbxGold";
            this.cbxGold.Size = new System.Drawing.Size(226, 21);
            this.cbxGold.TabIndex = 23;
            this.cbxGold.Text = "Materiales Superficie (Sistema)";
            this.cbxGold.UseVisualStyleBackColor = true;
            this.cbxGold.CheckedChanged += new System.EventHandler(this.CbxGold_CheckedChanged);
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
            this.cbxCodex.Location = new System.Drawing.Point(19, 302);
            this.cbxCodex.Margin = new System.Windows.Forms.Padding(4);
            this.cbxCodex.Name = "cbxCodex";
            this.cbxCodex.Size = new System.Drawing.Size(178, 21);
            this.cbxCodex.TabIndex = 20;
            this.cbxCodex.Text = "Descubrimientos Codex";
            this.cbxCodex.UseVisualStyleBackColor = true;
            this.cbxCodex.CheckedChanged += new System.EventHandler(this.CbxCodex_CheckedChanged);
            // 
            // cbxLandRing
            // 
            this.cbxLandRing.AutoSize = true;
            this.cbxLandRing.Location = new System.Drawing.Point(19, 130);
            this.cbxLandRing.Margin = new System.Windows.Forms.Padding(4);
            this.cbxLandRing.Name = "cbxLandRing";
            this.cbxLandRing.Size = new System.Drawing.Size(163, 21);
            this.cbxLandRing.TabIndex = 19;
            this.cbxLandRing.Text = "Aterrizable con Anillo";
            this.cbxLandRing.UseVisualStyleBackColor = true;
            this.cbxLandRing.CheckedChanged += new System.EventHandler(this.CbxLandRing_CheckedChanged);
            // 
            // cbxRinghugger
            // 
            this.cbxRinghugger.AutoSize = true;
            this.cbxRinghugger.Location = new System.Drawing.Point(19, 272);
            this.cbxRinghugger.Margin = new System.Windows.Forms.Padding(4);
            this.cbxRinghugger.Name = "cbxRinghugger";
            this.cbxRinghugger.Size = new System.Drawing.Size(133, 21);
            this.cbxRinghugger.TabIndex = 18;
            this.cbxRinghugger.Text = "Proximo al Anillo";
            this.cbxRinghugger.UseVisualStyleBackColor = true;
            this.cbxRinghugger.CheckedChanged += new System.EventHandler(this.CbxRinghugger_CheckedChanged);
            // 
            // cbx_WideRing
            // 
            this.cbx_WideRing.AutoSize = true;
            this.cbx_WideRing.Location = new System.Drawing.Point(19, 187);
            this.cbx_WideRing.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_WideRing.Name = "cbx_WideRing";
            this.cbx_WideRing.Size = new System.Drawing.Size(108, 21);
            this.cbx_WideRing.TabIndex = 17;
            this.cbx_WideRing.Text = "Anillo Ancho";
            this.cbx_WideRing.UseVisualStyleBackColor = true;
            this.cbx_WideRing.CheckedChanged += new System.EventHandler(this.Cbx_WideRing_CheckedChanged);
            // 
            // cbx_LandLarge
            // 
            this.cbx_LandLarge.AutoSize = true;
            this.cbx_LandLarge.Location = new System.Drawing.Point(19, 102);
            this.cbx_LandLarge.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_LandLarge.Name = "cbx_LandLarge";
            this.cbx_LandLarge.Size = new System.Drawing.Size(150, 21);
            this.cbx_LandLarge.TabIndex = 16;
            this.cbx_LandLarge.Text = "Aterrizable Grande";
            this.cbx_LandLarge.UseVisualStyleBackColor = true;
            this.cbx_LandLarge.CheckedChanged += new System.EventHandler(this.Cbx_LandLarge_CheckedChanged);
            // 
            // cbx_VeryInteresting
            // 
            this.cbx_VeryInteresting.AutoSize = true;
            this.cbx_VeryInteresting.Location = new System.Drawing.Point(253, 300);
            this.cbx_VeryInteresting.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_VeryInteresting.Name = "cbx_VeryInteresting";
            this.cbx_VeryInteresting.Size = new System.Drawing.Size(197, 21);
            this.cbx_VeryInteresting.TabIndex = 15;
            this.cbx_VeryInteresting.Text = "Notificar Criterios Multiples";
            this.cbx_VeryInteresting.UseVisualStyleBackColor = true;
            this.cbx_VeryInteresting.CheckedChanged += new System.EventHandler(this.Cbx_VeryInteresting_CheckedChanged);
            // 
            // cbx_Jumporium
            // 
            this.cbx_Jumporium.AutoSize = true;
            this.cbx_Jumporium.Location = new System.Drawing.Point(253, 242);
            this.cbx_Jumporium.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_Jumporium.Name = "cbx_Jumporium";
            this.cbx_Jumporium.Size = new System.Drawing.Size(171, 21);
            this.cbx_Jumporium.TabIndex = 12;
            this.cbx_Jumporium.Text = "Materiales Jumponium";
            this.cbx_Jumporium.UseVisualStyleBackColor = true;
            this.cbx_Jumporium.CheckedChanged += new System.EventHandler(this.Cbx_Jumporium_CheckedChanged);
            // 
            // cbx_HighEccentric
            // 
            this.cbx_HighEccentric.AutoSize = true;
            this.cbx_HighEccentric.Location = new System.Drawing.Point(253, 190);
            this.cbx_HighEccentric.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_HighEccentric.Name = "cbx_HighEccentric";
            this.cbx_HighEccentric.Size = new System.Drawing.Size(138, 21);
            this.cbx_HighEccentric.TabIndex = 11;
            this.cbx_HighEccentric.Text = "Órbita Excéntrica";
            this.cbx_HighEccentric.UseVisualStyleBackColor = true;
            this.cbx_HighEccentric.CheckedChanged += new System.EventHandler(this.Cbx_HighEccentric_CheckedChanged);
            // 
            // cbx_FastOrbit
            // 
            this.cbx_FastOrbit.AutoSize = true;
            this.cbx_FastOrbit.Location = new System.Drawing.Point(253, 130);
            this.cbx_FastOrbit.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_FastOrbit.Name = "cbx_FastOrbit";
            this.cbx_FastOrbit.Size = new System.Drawing.Size(118, 21);
            this.cbx_FastOrbit.TabIndex = 10;
            this.cbx_FastOrbit.Text = "Órbita Rápida";
            this.cbx_FastOrbit.UseVisualStyleBackColor = true;
            this.cbx_FastOrbit.CheckedChanged += new System.EventHandler(this.Cbx_FastOrbit_CheckedChanged);
            // 
            // cbx_FastRotate
            // 
            this.cbx_FastRotate.AutoSize = true;
            this.cbx_FastRotate.Location = new System.Drawing.Point(253, 102);
            this.cbx_FastRotate.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_FastRotate.Name = "cbx_FastRotate";
            this.cbx_FastRotate.Size = new System.Drawing.Size(135, 21);
            this.cbx_FastRotate.TabIndex = 9;
            this.cbx_FastRotate.Text = "Rotación Rápida";
            this.cbx_FastRotate.UseVisualStyleBackColor = true;
            this.cbx_FastRotate.CheckedChanged += new System.EventHandler(this.Cbx_FastRotate_CheckedChanged);
            // 
            // cbx_TinyObject
            // 
            this.cbx_TinyObject.AutoSize = true;
            this.cbx_TinyObject.Location = new System.Drawing.Point(253, 75);
            this.cbx_TinyObject.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_TinyObject.Name = "cbx_TinyObject";
            this.cbx_TinyObject.Size = new System.Drawing.Size(137, 21);
            this.cbx_TinyObject.TabIndex = 8;
            this.cbx_TinyObject.Text = "Cuerpo Pequeño";
            this.cbx_TinyObject.UseVisualStyleBackColor = true;
            this.cbx_TinyObject.CheckedChanged += new System.EventHandler(this.Cbx_TinyObject_CheckedChanged);
            // 
            // cbx_NestedMoon
            // 
            this.cbx_NestedMoon.AutoSize = true;
            this.cbx_NestedMoon.Location = new System.Drawing.Point(253, 47);
            this.cbx_NestedMoon.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_NestedMoon.Name = "cbx_NestedMoon";
            this.cbx_NestedMoon.Size = new System.Drawing.Size(118, 21);
            this.cbx_NestedMoon.TabIndex = 7;
            this.cbx_NestedMoon.Text = "Luna Anidada";
            this.cbx_NestedMoon.UseVisualStyleBackColor = true;
            this.cbx_NestedMoon.CheckedChanged += new System.EventHandler(this.Cbx_NestedMoon_CheckedChanged);
            // 
            // cbx_CollidingBinary
            // 
            this.cbx_CollidingBinary.AutoSize = true;
            this.cbx_CollidingBinary.Location = new System.Drawing.Point(19, 244);
            this.cbx_CollidingBinary.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_CollidingBinary.Name = "cbx_CollidingBinary";
            this.cbx_CollidingBinary.Size = new System.Drawing.Size(127, 21);
            this.cbx_CollidingBinary.TabIndex = 6;
            this.cbx_CollidingBinary.Text = "Choque Binario";
            this.cbx_CollidingBinary.UseVisualStyleBackColor = true;
            this.cbx_CollidingBinary.CheckedChanged += new System.EventHandler(this.Cbx_CollidingBinary_CheckedChanged);
            // 
            // cbx_CloseBinary
            // 
            this.cbx_CloseBinary.AutoSize = true;
            this.cbx_CloseBinary.Location = new System.Drawing.Point(19, 217);
            this.cbx_CloseBinary.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_CloseBinary.Name = "cbx_CloseBinary";
            this.cbx_CloseBinary.Size = new System.Drawing.Size(131, 21);
            this.cbx_CloseBinary.TabIndex = 5;
            this.cbx_CloseBinary.Text = "Binario Cercano";
            this.cbx_CloseBinary.UseVisualStyleBackColor = true;
            this.cbx_CloseBinary.CheckedChanged += new System.EventHandler(this.Cbx_CloseBinary_CheckedChanged);
            // 
            // cbx_ShepherdMoon
            // 
            this.cbx_ShepherdMoon.AutoSize = true;
            this.cbx_ShepherdMoon.Location = new System.Drawing.Point(253, 18);
            this.cbx_ShepherdMoon.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_ShepherdMoon.Name = "cbx_ShepherdMoon";
            this.cbx_ShepherdMoon.Size = new System.Drawing.Size(127, 21);
            this.cbx_ShepherdMoon.TabIndex = 4;
            this.cbx_ShepherdMoon.Text = "Luna de Pastor";
            this.cbx_ShepherdMoon.UseVisualStyleBackColor = true;
            this.cbx_ShepherdMoon.CheckedChanged += new System.EventHandler(this.Cbx_ShepherdMoon_CheckedChanged);
            // 
            // cbx_LandHighG
            // 
            this.cbx_LandHighG.AutoSize = true;
            this.cbx_LandHighG.Location = new System.Drawing.Point(19, 75);
            this.cbx_LandHighG.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_LandHighG.Name = "cbx_LandHighG";
            this.cbx_LandHighG.Size = new System.Drawing.Size(142, 21);
            this.cbx_LandHighG.TabIndex = 2;
            this.cbx_LandHighG.Text = "Aterrizable Alto-G";
            this.cbx_LandHighG.UseVisualStyleBackColor = true;
            this.cbx_LandHighG.CheckedChanged += new System.EventHandler(this.Cbx_LandHighG_CheckedChanged);
            // 
            // cbx_LandWithAtmo
            // 
            this.cbx_LandWithAtmo.AutoSize = true;
            this.cbx_LandWithAtmo.Location = new System.Drawing.Point(19, 48);
            this.cbx_LandWithAtmo.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_LandWithAtmo.Name = "cbx_LandWithAtmo";
            this.cbx_LandWithAtmo.Size = new System.Drawing.Size(193, 21);
            this.cbx_LandWithAtmo.TabIndex = 1;
            this.cbx_LandWithAtmo.Text = "Aterrizable con Atmosfera";
            this.cbx_LandWithAtmo.UseVisualStyleBackColor = true;
            this.cbx_LandWithAtmo.CheckedChanged += new System.EventHandler(this.Cbx_LandWithAtmo_CheckedChanged);
            // 
            // cbx_LandWithTerra
            // 
            this.cbx_LandWithTerra.AutoSize = true;
            this.cbx_LandWithTerra.Location = new System.Drawing.Point(19, 18);
            this.cbx_LandWithTerra.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_LandWithTerra.Name = "cbx_LandWithTerra";
            this.cbx_LandWithTerra.Size = new System.Drawing.Size(203, 21);
            this.cbx_LandWithTerra.TabIndex = 0;
            this.cbx_LandWithTerra.Text = "Aterrizable y Terraformable";
            this.cbx_LandWithTerra.UseVisualStyleBackColor = true;
            this.cbx_LandWithTerra.CheckedChanged += new System.EventHandler(this.Cbx_LandWithTerra_CheckedChanged);
            // 
            // cbx_CloseOrbit
            // 
            this.cbx_CloseOrbit.AutoSize = true;
            this.cbx_CloseOrbit.Location = new System.Drawing.Point(253, 160);
            this.cbx_CloseOrbit.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_CloseOrbit.Name = "cbx_CloseOrbit";
            this.cbx_CloseOrbit.Size = new System.Drawing.Size(126, 21);
            this.cbx_CloseOrbit.TabIndex = 3;
            this.cbx_CloseOrbit.Text = "Orbita Cercana";
            this.cbx_CloseOrbit.UseVisualStyleBackColor = true;
            this.cbx_CloseOrbit.CheckedChanged += new System.EventHandler(this.Cbx_CloseOrbit_CheckedChanged);
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
            this.cbxBeta.Size = new System.Drawing.Size(148, 21);
            this.cbxBeta.TabIndex = 2;
            this.cbxBeta.Text = "Activar con la Beta";
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
            this.groupBox_TTS.Location = new System.Drawing.Point(348, 98);
            this.groupBox_TTS.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox_TTS.Name = "groupBox_TTS";
            this.groupBox_TTS.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox_TTS.Size = new System.Drawing.Size(92, 162);
            this.groupBox_TTS.TabIndex = 2;
            this.groupBox_TTS.TabStop = false;
            this.groupBox_TTS.Text = "Volumen";
            this.groupBox_TTS.Enter += new System.EventHandler(this.groupBox_TTS_Enter);
            // 
            // btn_TestVol
            // 
            this.btn_TestVol.Location = new System.Drawing.Point(12, 126);
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
            this.trackBar_Volume.LargeChange = 20;
            this.trackBar_Volume.Location = new System.Drawing.Point(20, 28);
            this.trackBar_Volume.Margin = new System.Windows.Forms.Padding(4);
            this.trackBar_Volume.Maximum = 100;
            this.trackBar_Volume.Name = "trackBar_Volume";
            this.trackBar_Volume.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_Volume.Size = new System.Drawing.Size(56, 89);
            this.trackBar_Volume.TabIndex = 0;
            this.trackBar_Volume.TickFrequency = 10;
            this.trackBar_Volume.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar_Volume.Scroll += new System.EventHandler(this.TrackBar_Volume_Scroll);
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.tabPage1);
            this.TabControl.Controls.Add(this.tabPage3);
            this.TabControl.Controls.Add(this.tabPage2);
            this.TabControl.Location = new System.Drawing.Point(12, 12);
            this.TabControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(579, 409);
            this.TabControl.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnSelectNone);
            this.tabPage1.Controls.Add(this.btnSelectAll);
            this.tabPage1.Controls.Add(this.cbxGold);
            this.tabPage1.Controls.Add(this.cbx_CloseOrbit);
            this.tabPage1.Controls.Add(this.cbx_LandLarge);
            this.tabPage1.Controls.Add(this.cbx_LandHighG);
            this.tabPage1.Controls.Add(this.cbxLandRing);
            this.tabPage1.Controls.Add(this.cbx_VeryInteresting);
            this.tabPage1.Controls.Add(this.cbxCodex);
            this.tabPage1.Controls.Add(this.cbx_LandWithAtmo);
            this.tabPage1.Controls.Add(this.cbx_ShepherdMoon);
            this.tabPage1.Controls.Add(this.cbx_Jumporium);
            this.tabPage1.Controls.Add(this.cbxRinghugger);
            this.tabPage1.Controls.Add(this.cbx_HighEccentric);
            this.tabPage1.Controls.Add(this.cbx_WideRing);
            this.tabPage1.Controls.Add(this.cbx_FastOrbit);
            this.tabPage1.Controls.Add(this.cbx_CloseBinary);
            this.tabPage1.Controls.Add(this.cbx_FastRotate);
            this.tabPage1.Controls.Add(this.cbx_CollidingBinary);
            this.tabPage1.Controls.Add(this.cbx_TinyObject);
            this.tabPage1.Controls.Add(this.cbx_LandWithTerra);
            this.tabPage1.Controls.Add(this.cbx_NestedMoon);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(571, 380);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Exploración";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            this.tabPage3.Size = new System.Drawing.Size(571, 380);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Varios";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.textBox6);
            this.tabPage2.Controls.Add(this.textBox7);
            this.tabPage2.Controls.Add(this.checkBox3);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.textBox4);
            this.tabPage2.Controls.Add(this.textBox5);
            this.tabPage2.Controls.Add(this.checkBox2);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.textBox3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Controls.Add(this.checkBox1);
            this.tabPage2.Controls.Add(this.btnReadAll);
            this.tabPage2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(571, 380);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Journal Log";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(374, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 17);
            this.label5.TabIndex = 38;
            this.label5.Text = "horas";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(295, 150);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(68, 22);
            this.textBox6.TabIndex = 37;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(209, 150);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(68, 22);
            this.textBox7.TabIndex = 36;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(55, 150);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(138, 21);
            this.checkBox3.TabIndex = 35;
            this.checkBox3.Text = "Velocidad Orbital";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(374, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 17);
            this.label4.TabIndex = 34;
            this.label4.Text = "g";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(295, 120);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(68, 22);
            this.textBox4.TabIndex = 33;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(209, 120);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(68, 22);
            this.textBox5.TabIndex = 32;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(55, 121);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(93, 21);
            this.checkBox2.TabIndex = 31;
            this.checkBox2.Text = "Gravedad";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(374, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 17);
            this.label3.TabIndex = 30;
            this.label3.Text = "Kilometros";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(295, 90);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(68, 22);
            this.textBox3.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(304, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 28;
            this.label2.Text = "Mayor";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(220, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 17);
            this.label1.TabIndex = 27;
            this.label1.Text = "Menor";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(209, 90);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(68, 22);
            this.textBox2.TabIndex = 26;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(55, 90);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(82, 21);
            this.checkBox1.TabIndex = 25;
            this.checkBox1.Text = "Tamaño";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnReadAll
            // 
            this.btnReadAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadAll.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReadAll.Location = new System.Drawing.Point(451, 330);
            this.btnReadAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReadAll.Name = "btnReadAll";
            this.btnReadAll.Size = new System.Drawing.Size(88, 28);
            this.btnReadAll.TabIndex = 24;
            this.btnReadAll.Text = "Leer Logs";
            this.btnReadAll.UseVisualStyleBackColor = true;
            this.btnReadAll.Click += new System.EventHandler(this.btnReadAll_Click);
            // 
            // SettingsFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(599, 428);
            this.Controls.Add(this.TabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsFrm";
            this.Text = "EDExplorer Configuración";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsFrm_FormClosed);
            this.Load += new System.EventHandler(this.SettingsFrm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SettingsFrm_Paint);
            this.groupBox_misc.ResumeLayout(false);
            this.groupBox_misc.PerformLayout();
            this.groupBox_TTS.ResumeLayout(false);
            this.groupBox_TTS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Volume)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox cbx_LandWithTerra;
        private System.Windows.Forms.CheckBox cbx_VeryInteresting;
        private System.Windows.Forms.CheckBox cbx_Jumporium;
        private System.Windows.Forms.CheckBox cbx_HighEccentric;
        private System.Windows.Forms.CheckBox cbx_FastOrbit;
        private System.Windows.Forms.CheckBox cbx_FastRotate;
        private System.Windows.Forms.CheckBox cbx_TinyObject;
        private System.Windows.Forms.CheckBox cbx_NestedMoon;
        private System.Windows.Forms.CheckBox cbx_CollidingBinary;
        private System.Windows.Forms.CheckBox cbx_CloseBinary;
        private System.Windows.Forms.CheckBox cbx_ShepherdMoon;
        private System.Windows.Forms.CheckBox cbx_CloseOrbit;
        private System.Windows.Forms.CheckBox cbx_LandHighG;
        private System.Windows.Forms.CheckBox cbx_LandWithAtmo;
        private System.Windows.Forms.GroupBox groupBox_misc;
        private System.Windows.Forms.CheckBox cbxTts;
        private System.Windows.Forms.CheckBox cbxToast;
        private System.Windows.Forms.ToolTip tipCopy;
        private System.Windows.Forms.CheckBox cbx_WideRing;
        private System.Windows.Forms.CheckBox cbx_LandLarge;
        private System.Windows.Forms.GroupBox groupBox_TTS;
        private System.Windows.Forms.Button btn_TestVol;
        private System.Windows.Forms.TrackBar trackBar_Volume;
        private System.Windows.Forms.CheckBox cbxLandRing;
        private System.Windows.Forms.CheckBox cbxRinghugger;
        private System.Windows.Forms.CheckBox cbxAutoRead;
        private System.Windows.Forms.CheckBox cbxAutoMonitor;
        private System.Windows.Forms.CheckBox cbxCodex;
        private System.Windows.Forms.Button btnSelectNone;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.CheckBox cbxGold;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox cbxBeta;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnReadAll;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.CheckBox checkBox3;
    }
}
