namespace EDExplorer.Pantallas
{
    partial class AboutFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutFrm));
            this.button1 = new System.Windows.Forms.Button();
            this.frmLienzo = new System.Windows.Forms.PictureBox();
            this.paso1 = new System.Windows.Forms.PictureBox();
            this.paso2 = new System.Windows.Forms.PictureBox();
            this.paso3 = new System.Windows.Forms.PictureBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.frmLienzo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paso1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paso2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paso3)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmLienzo
            // 
            resources.ApplyResources(this.frmLienzo, "frmLienzo");
            this.frmLienzo.BackColor = System.Drawing.Color.White;
            this.frmLienzo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.frmLienzo.Name = "frmLienzo";
            this.frmLienzo.TabStop = false;
            // 
            // paso1
            // 
            resources.ApplyResources(this.paso1, "paso1");
            this.paso1.BackColor = System.Drawing.Color.White;
            this.paso1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paso1.Name = "paso1";
            this.paso1.TabStop = false;
            // 
            // paso2
            // 
            resources.ApplyResources(this.paso2, "paso2");
            this.paso2.BackColor = System.Drawing.Color.White;
            this.paso2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paso2.Name = "paso2";
            this.paso2.TabStop = false;
            // 
            // paso3
            // 
            resources.ApplyResources(this.paso3, "paso3");
            this.paso3.BackColor = System.Drawing.Color.White;
            this.paso3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paso3.Name = "paso3";
            this.paso3.TabStop = false;
            // 
            // richTextBox1
            // 
            resources.ApplyResources(this.richTextBox1, "richTextBox1");
            this.richTextBox1.Name = "richTextBox1";
            // 
            // AboutFrm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.paso3);
            this.Controls.Add(this.paso2);
            this.Controls.Add(this.paso1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.frmLienzo);
            this.Name = "AboutFrm";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.frmLienzo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paso1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paso2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paso3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox frmLienzo;
        private System.Windows.Forms.PictureBox paso1;
        private System.Windows.Forms.PictureBox paso2;
        private System.Windows.Forms.PictureBox paso3;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}