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
            this.button1 = new System.Windows.Forms.Button();
            this.frmLienzo = new System.Windows.Forms.PictureBox();
            this.paso1 = new System.Windows.Forms.PictureBox();
            this.paso2 = new System.Windows.Forms.PictureBox();
            this.paso3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.frmLienzo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paso1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paso2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paso3)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1219, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 29);
            this.button1.TabIndex = 0;
            this.button1.Text = "Dibujar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmLienzo
            // 
            this.frmLienzo.BackColor = System.Drawing.Color.White;
            this.frmLienzo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.frmLienzo.Location = new System.Drawing.Point(340, 12);
            this.frmLienzo.Name = "frmLienzo";
            this.frmLienzo.Size = new System.Drawing.Size(869, 696);
            this.frmLienzo.TabIndex = 1;
            this.frmLienzo.TabStop = false;
            // 
            // paso1
            // 
            this.paso1.BackColor = System.Drawing.Color.White;
            this.paso1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paso1.Location = new System.Drawing.Point(12, 14);
            this.paso1.Name = "paso1";
            this.paso1.Size = new System.Drawing.Size(310, 219);
            this.paso1.TabIndex = 2;
            this.paso1.TabStop = false;
            // 
            // paso2
            // 
            this.paso2.BackColor = System.Drawing.Color.White;
            this.paso2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paso2.Location = new System.Drawing.Point(12, 251);
            this.paso2.Name = "paso2";
            this.paso2.Size = new System.Drawing.Size(310, 219);
            this.paso2.TabIndex = 3;
            this.paso2.TabStop = false;
            // 
            // paso3
            // 
            this.paso3.BackColor = System.Drawing.Color.White;
            this.paso3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paso3.Location = new System.Drawing.Point(12, 491);
            this.paso3.Name = "paso3";
            this.paso3.Size = new System.Drawing.Size(310, 219);
            this.paso3.TabIndex = 4;
            this.paso3.TabStop = false;
            // 
            // AboutFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1306, 717);
            this.Controls.Add(this.paso3);
            this.Controls.Add(this.paso2);
            this.Controls.Add(this.paso1);
            this.Controls.Add(this.frmLienzo);
            this.Controls.Add(this.button1);
            this.Name = "AboutFrm";
            this.Text = "Desarrollado por 3navis";
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
    }
}