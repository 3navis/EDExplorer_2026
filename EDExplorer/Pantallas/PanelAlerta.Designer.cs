namespace EDExplorer.Pantallas
{
    partial class PanelAlerta
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PanelAlerta));
            this.labelUnidades = new System.Windows.Forms.Label();
            this.textDesde = new System.Windows.Forms.TextBox();
            this.textAlerta = new System.Windows.Forms.TextBox();
            this.checkAlerta = new System.Windows.Forms.CheckBox();
            this.textHasta = new System.Windows.Forms.TextBox();
            this.picIncluir = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picIncluir)).BeginInit();
            this.SuspendLayout();
            // 
            // labelUnidades
            // 
            this.labelUnidades.AutoSize = true;
            this.labelUnidades.ForeColor = System.Drawing.Color.Black;
            this.labelUnidades.Location = new System.Drawing.Point(379, 5);
            this.labelUnidades.Name = "labelUnidades";
            this.labelUnidades.Size = new System.Drawing.Size(74, 17);
            this.labelUnidades.TabIndex = 41;
            this.labelUnidades.Text = "Kilometros";
            // 
            // textDesde
            // 
            this.textDesde.ForeColor = System.Drawing.Color.Black;
            this.textDesde.Location = new System.Drawing.Point(510, 1);
            this.textDesde.Name = "textDesde";
            this.textDesde.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textDesde.Size = new System.Drawing.Size(68, 22);
            this.textDesde.TabIndex = 40;
            // 
            // textAlerta
            // 
            this.textAlerta.ForeColor = System.Drawing.Color.Black;
            this.textAlerta.Location = new System.Drawing.Point(189, 1);
            this.textAlerta.Name = "textAlerta";
            this.textAlerta.Size = new System.Drawing.Size(184, 22);
            this.textAlerta.TabIndex = 39;
            // 
            // checkAlerta
            // 
            this.checkAlerta.AutoSize = true;
            this.checkAlerta.ForeColor = System.Drawing.Color.Black;
            this.checkAlerta.Location = new System.Drawing.Point(7, 4);
            this.checkAlerta.Margin = new System.Windows.Forms.Padding(4);
            this.checkAlerta.Name = "checkAlerta";
            this.checkAlerta.Size = new System.Drawing.Size(121, 21);
            this.checkAlerta.TabIndex = 38;
            this.checkAlerta.Text = "Alta Gravedad";
            this.checkAlerta.UseVisualStyleBackColor = true;
            // 
            // textHasta
            // 
            this.textHasta.ForeColor = System.Drawing.Color.Black;
            this.textHasta.Location = new System.Drawing.Point(637, 1);
            this.textHasta.Name = "textHasta";
            this.textHasta.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textHasta.Size = new System.Drawing.Size(68, 22);
            this.textHasta.TabIndex = 42;
            this.textHasta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // picIncluir
            // 
            this.picIncluir.Image = ((System.Drawing.Image)(resources.GetObject("picIncluir.Image")));
            this.picIncluir.Location = new System.Drawing.Point(584, 0);
            this.picIncluir.Name = "picIncluir";
            this.picIncluir.Size = new System.Drawing.Size(48, 22);
            this.picIncluir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picIncluir.TabIndex = 43;
            this.picIncluir.TabStop = false;
            this.picIncluir.Click += new System.EventHandler(this.picIncluir_Click);
            // 
            // PanelAlerta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.Controls.Add(this.picIncluir);
            this.Controls.Add(this.textHasta);
            this.Controls.Add(this.labelUnidades);
            this.Controls.Add(this.textDesde);
            this.Controls.Add(this.textAlerta);
            this.Controls.Add(this.checkAlerta);
            this.Name = "PanelAlerta";
            this.Size = new System.Drawing.Size(711, 26);
            ((System.ComponentModel.ISupportInitialize)(this.picIncluir)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUnidades;
        private System.Windows.Forms.TextBox textDesde;
        private System.Windows.Forms.TextBox textAlerta;
        private System.Windows.Forms.CheckBox checkAlerta;
        private System.Windows.Forms.TextBox textHasta;
        private System.Windows.Forms.PictureBox picIncluir;
    }
}
