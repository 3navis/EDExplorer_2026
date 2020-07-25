using EDExplorer.Properties;
using System.Windows.Forms;

namespace EDExplorer
{
     partial class NotifyFrm
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
            this.lblText = new System.Windows.Forms.Label();
            this.image = new System.Windows.Forms.PictureBox();
            this.lblTick = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
            this.SuspendLayout();
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.BackColor = System.Drawing.Color.Transparent;
            this.lblText.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblText.Location = new System.Drawing.Point(5, 36);
            this.lblText.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(176, 19);
            this.lblText.TabIndex = 1;
            this.lblText.Text = "Prueba de Notificación";
            // 
            // image
            // 
            this.image.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.image.Image = global::EDExplorer.Properties.Resources.EDExplorerPNG;
            this.image.Location = new System.Drawing.Point(259, 6);
            this.image.Name = "image";
            this.image.Size = new System.Drawing.Size(21, 16);
            this.image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.image.TabIndex = 2;
            this.image.TabStop = false;
            // 
            // lblTick
            // 
            this.lblTick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTick.AutoSize = true;
            this.lblTick.BackColor = System.Drawing.Color.Transparent;
            this.lblTick.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTick.Location = new System.Drawing.Point(258, 32);
            this.lblTick.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTick.Name = "lblTick";
            this.lblTick.Size = new System.Drawing.Size(24, 16);
            this.lblTick.TabIndex = 3;
            this.lblTick.Text = "00";
            this.lblTick.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(5, 6);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(68, 19);
            this.lblTitulo.TabIndex = 4;
            this.lblTitulo.Text = "TITULO";
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.Blue;
            this.progressBar1.Location = new System.Drawing.Point(-8, 28);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(300, 2);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 5;
            // 
            // NotifyFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(284, 113);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblTick);
            this.Controls.Add(this.image);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.lblTitulo);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Gold;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NotifyFrm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NotifyFrm";
            this.TransparencyKey = System.Drawing.Color.MidnightBlue;
            ((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.PictureBox image;
        private Label lblTick;
        private Label lblTitulo;
        private ProgressBar progressBar1;
    }
}