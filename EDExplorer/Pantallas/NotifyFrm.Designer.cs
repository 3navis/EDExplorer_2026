using System.Drawing;
using System.Drawing.Text;
//using System.Windows.Forms;
//using System.Range;

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
            this.pictureBox_EDExplorer = new System.Windows.Forms.PictureBox();
            this.lblText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_EDExplorer)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_EDExplorer
            // 
            this.pictureBox_EDExplorer.ErrorImage = global::EDExplorer.Properties.Resources.NOTIFY_ON;
            this.pictureBox_EDExplorer.Location = new System.Drawing.Point(297, 9);
            this.pictureBox_EDExplorer.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox_EDExplorer.Name = "pictureBox_EDExplorer";
            this.pictureBox_EDExplorer.Size = new System.Drawing.Size(31, 32);
            this.pictureBox_EDExplorer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_EDExplorer.TabIndex = 0;
            this.pictureBox_EDExplorer.TabStop = false;
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblText.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblText.Location = new System.Drawing.Point(13, 9);
            this.lblText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(178, 20);
            this.lblText.TabIndex = 1;
            this.lblText.Text = "Prueba de Notificación";
            // 
            // NotifyFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(335, 126);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.pictureBox_EDExplorer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NotifyFrm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "NotifyFrm";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            //this.Load += new System.EventHandler(this.NotifyFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_EDExplorer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_EDExplorer;
        private System.Windows.Forms.Label lblText;
    }
}