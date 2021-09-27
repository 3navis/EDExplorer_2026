using System.Windows.Forms;

namespace EDExplorer
{
    partial class EDExplorerFrm
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
            //            if (speech != null) speech.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EDExplorerFrm));
            this.listEvent = new System.Windows.Forms.ListView();
            this.timestamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sistema = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cuerpo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.alerta = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.detail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.record = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnReadAll = new System.Windows.Forms.Button();
            this.progressReadAll = new System.Windows.Forms.ProgressBar();
            this.contextCopy = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterAlertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblRecord = new System.Windows.Forms.Label();
            this.contextCopy.SuspendLayout();
            this.SuspendLayout();
            // 
            // listEvent
            // 
            this.listEvent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listEvent.BackColor = System.Drawing.Color.Black;
            this.listEvent.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.timestamp,
            this.sistema,
            this.cuerpo,
            this.alerta,
            this.detail,
            this.record,
            this.valor});
            this.listEvent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listEvent.ForeColor = System.Drawing.Color.Orange;
            this.listEvent.FullRowSelect = true;
            this.listEvent.GridLines = true;
            this.listEvent.HideSelection = false;
            this.listEvent.Location = new System.Drawing.Point(3, 31);
            this.listEvent.Name = "listEvent";
            this.listEvent.Size = new System.Drawing.Size(991, 394);
            this.listEvent.TabIndex = 2;
            this.listEvent.UseCompatibleStateImageBehavior = false;
            this.listEvent.View = System.Windows.Forms.View.Details;
            this.listEvent.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListEvent_ColumnClick);
            this.listEvent.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listEvent_DrawItem);
            this.listEvent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListEvent_KeyDown);
            this.listEvent.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListEvent_MouseClick);
            // 
            // timestamp
            // 
            this.timestamp.Text = "Fecha / Hora";
            this.timestamp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.timestamp.Width = 117;
            // 
            // sistema
            // 
            this.sistema.Text = "Sistema";
            this.sistema.Width = 207;
            // 
            // cuerpo
            // 
            this.cuerpo.Text = "Cuerpo";
            this.cuerpo.Width = 90;
            // 
            // alerta
            // 
            this.alerta.Text = "Alerta";
            this.alerta.Width = 147;
            // 
            // detail
            // 
            this.detail.DisplayIndex = 6;
            this.detail.Text = "Descripción";
            this.detail.Width = 302;
            // 
            // record
            // 
            this.record.DisplayIndex = 4;
            this.record.Text = "R";
            this.record.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.record.Width = 27;
            // 
            // valor
            // 
            this.valor.DisplayIndex = 5;
            this.valor.Tag = "number";
            this.valor.Text = "Valor";
            this.valor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.valor.Width = 80;
            // 
            // btnReadAll
            // 
            this.btnReadAll.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReadAll.Location = new System.Drawing.Point(3, 2);
            this.btnReadAll.Name = "btnReadAll";
            this.btnReadAll.Size = new System.Drawing.Size(145, 25);
            this.btnReadAll.TabIndex = 3;
            this.btnReadAll.Text = "Analizar Histórico";
            this.btnReadAll.UseVisualStyleBackColor = true;
            this.btnReadAll.Click += new System.EventHandler(this.BtnReadAll_Click);
            // 
            // progressReadAll
            // 
            this.progressReadAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressReadAll.Location = new System.Drawing.Point(3, 27);
            this.progressReadAll.Name = "progressReadAll";
            this.progressReadAll.Size = new System.Drawing.Size(991, 10);
            this.progressReadAll.TabIndex = 4;
            this.progressReadAll.Visible = false;
            // 
            // contextCopy
            // 
            this.contextCopy.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextCopy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyNameToolStripMenuItem,
            this.filterNameToolStripMenuItem,
            this.filterAlertToolStripMenuItem,
            this.filterRecordToolStripMenuItem,
            this.removeFilterToolStripMenuItem});
            this.contextCopy.Name = "contextCopy";
            this.contextCopy.Size = new System.Drawing.Size(260, 124);
            // 
            // copyNameToolStripMenuItem
            // 
            this.copyNameToolStripMenuItem.Name = "copyNameToolStripMenuItem";
            this.copyNameToolStripMenuItem.Size = new System.Drawing.Size(259, 24);
            this.copyNameToolStripMenuItem.Text = "Copiar nombre del Sistema";
            this.copyNameToolStripMenuItem.Click += new System.EventHandler(this.CopyNameToolStripMenuItem_Click);
            // 
            // filterNameToolStripMenuItem
            // 
            this.filterNameToolStripMenuItem.Name = "filterNameToolStripMenuItem";
            this.filterNameToolStripMenuItem.Size = new System.Drawing.Size(259, 24);
            this.filterNameToolStripMenuItem.Text = "Filtrar Sistema";
            this.filterNameToolStripMenuItem.Click += new System.EventHandler(this.FilterNameToolStripMenuItem_Click);
            // 
            // filterAlertToolStripMenuItem
            // 
            this.filterAlertToolStripMenuItem.Name = "filterAlertToolStripMenuItem";
            this.filterAlertToolStripMenuItem.Size = new System.Drawing.Size(259, 24);
            this.filterAlertToolStripMenuItem.Text = "Filtrar Alerta";
            this.filterAlertToolStripMenuItem.Click += new System.EventHandler(this.FilterAlertToolStripMenuItem_Click);
            // 
            // filterRecordToolStripMenuItem
            // 
            this.filterRecordToolStripMenuItem.Name = "filterRecordToolStripMenuItem";
            this.filterRecordToolStripMenuItem.Size = new System.Drawing.Size(259, 24);
            this.filterRecordToolStripMenuItem.Text = "Filtrar Records";
            this.filterRecordToolStripMenuItem.Click += new System.EventHandler(this.FilterRecordToolStripMenuItem_Click);
            // 
            // removeFilterToolStripMenuItem
            // 
            this.removeFilterToolStripMenuItem.Name = "removeFilterToolStripMenuItem";
            this.removeFilterToolStripMenuItem.Size = new System.Drawing.Size(259, 24);
            this.removeFilterToolStripMenuItem.Text = "Eliminar Filtro";
            this.removeFilterToolStripMenuItem.Click += new System.EventHandler(this.RemoveFilterToolStripMenuItem_Click);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(163, 8);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(0, 19);
            this.lblTime.TabIndex = 5;
            // 
            // lblRecord
            // 
            this.lblRecord.AutoSize = true;
            this.lblRecord.Font = new System.Drawing.Font("Webdings", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lblRecord.Location = new System.Drawing.Point(948, 2);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Size = new System.Drawing.Size(27, 22);
            this.lblRecord.TabIndex = 6;
            this.lblRecord.Text = "";
            this.lblRecord.Visible = false;
            // 
            // EDExplorerFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 429);
            this.Controls.Add(this.lblRecord);
            this.Controls.Add(this.btnReadAll);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.listEvent);
            this.Controls.Add(this.progressReadAll);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EDExplorerFrm";
            this.Text = "Elite EDExplorer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EDExplorerFrm_FormClosing);
            this.Load += new System.EventHandler(this.EDExplorerFrm_Load);
            this.Shown += new System.EventHandler(this.EDExplorerFrm_Shown);
            this.contextCopy.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ListView listEvent;
        private ColumnHeader cuerpo;
        private ColumnHeader alerta;
        private Button btnReadAll;
        private ProgressBar progressReadAll;
        private ColumnHeader timestamp;
        private ColumnHeader detail;
        private ContextMenuStrip contextCopy;
        private ToolStripMenuItem copyNameToolStripMenuItem;
        private ToolStripMenuItem filterNameToolStripMenuItem;
        private ToolStripMenuItem filterAlertToolStripMenuItem;
        private ToolStripMenuItem filterRecordToolStripMenuItem;
        private ToolStripMenuItem removeFilterToolStripMenuItem;
        private ColumnHeader sistema;
        private Label lblTime;
        private ColumnHeader record;
        private Label lblRecord;
        private ColumnHeader valor;
    }
}

