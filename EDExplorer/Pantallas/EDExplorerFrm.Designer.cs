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
            this.progressReadAll = new System.Windows.Forms.ProgressBar();
            this.contextCopy = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Separador1 = new System.Windows.Forms.ToolStripSeparator();
            this.filterNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterAlertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Separador2 = new System.Windows.Forms.ToolStripSeparator();
            this.HistoricoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblRecord = new System.Windows.Forms.Label();
            this.contextCopy.SuspendLayout();
            this.SuspendLayout();
            // 
            // listEvent
            // 
            resources.ApplyResources(this.listEvent, "listEvent");
            this.listEvent.BackColor = System.Drawing.Color.Black;
            this.listEvent.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.timestamp,
            this.sistema,
            this.cuerpo,
            this.alerta,
            this.detail,
            this.record,
            this.valor});
            this.listEvent.ForeColor = System.Drawing.Color.Orange;
            this.listEvent.FullRowSelect = true;
            this.listEvent.GridLines = true;
            this.listEvent.HideSelection = false;
            this.listEvent.Name = "listEvent";
            this.listEvent.UseCompatibleStateImageBehavior = false;
            this.listEvent.View = System.Windows.Forms.View.Details;
            this.listEvent.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListEvent_ColumnClick);
            this.listEvent.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listEvent_DrawItem);
            this.listEvent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListEvent_KeyDown);
            this.listEvent.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListEvent_MouseClick);
            // 
            // timestamp
            // 
            resources.ApplyResources(this.timestamp, "timestamp");
            // 
            // sistema
            // 
            resources.ApplyResources(this.sistema, "sistema");
            // 
            // cuerpo
            // 
            resources.ApplyResources(this.cuerpo, "cuerpo");
            // 
            // alerta
            // 
            resources.ApplyResources(this.alerta, "alerta");
            // 
            // detail
            // 
            resources.ApplyResources(this.detail, "detail");
            // 
            // record
            // 
            resources.ApplyResources(this.record, "record");
            // 
            // valor
            // 
            resources.ApplyResources(this.valor, "valor");
            this.valor.Tag = "number";
            // 
            // progressReadAll
            // 
            resources.ApplyResources(this.progressReadAll, "progressReadAll");
            this.progressReadAll.Name = "progressReadAll";
            // 
            // contextCopy
            // 
            this.contextCopy.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextCopy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyNameToolStripMenuItem,
            this.Separador1,
            this.filterNameToolStripMenuItem,
            this.filterAlertToolStripMenuItem,
            this.filterRecordToolStripMenuItem,
            this.removeFilterToolStripMenuItem,
            this.Separador2,
            this.HistoricoToolStripMenuItem});
            this.contextCopy.Name = "contextCopy";
            resources.ApplyResources(this.contextCopy, "contextCopy");
            // 
            // copyNameToolStripMenuItem
            // 
            this.copyNameToolStripMenuItem.Name = "copyNameToolStripMenuItem";
            resources.ApplyResources(this.copyNameToolStripMenuItem, "copyNameToolStripMenuItem");
            this.copyNameToolStripMenuItem.Click += new System.EventHandler(this.CopyNameToolStripMenuItem_Click);
            // 
            // Separador1
            // 
            this.Separador1.Name = "Separador1";
            resources.ApplyResources(this.Separador1, "Separador1");
            // 
            // filterNameToolStripMenuItem
            // 
            this.filterNameToolStripMenuItem.Name = "filterNameToolStripMenuItem";
            resources.ApplyResources(this.filterNameToolStripMenuItem, "filterNameToolStripMenuItem");
            this.filterNameToolStripMenuItem.Click += new System.EventHandler(this.FilterNameToolStripMenuItem_Click);
            // 
            // filterAlertToolStripMenuItem
            // 
            this.filterAlertToolStripMenuItem.Name = "filterAlertToolStripMenuItem";
            resources.ApplyResources(this.filterAlertToolStripMenuItem, "filterAlertToolStripMenuItem");
            this.filterAlertToolStripMenuItem.Click += new System.EventHandler(this.FilterAlertToolStripMenuItem_Click);
            // 
            // filterRecordToolStripMenuItem
            // 
            this.filterRecordToolStripMenuItem.Name = "filterRecordToolStripMenuItem";
            resources.ApplyResources(this.filterRecordToolStripMenuItem, "filterRecordToolStripMenuItem");
            this.filterRecordToolStripMenuItem.Click += new System.EventHandler(this.FilterRecordToolStripMenuItem_Click);
            // 
            // removeFilterToolStripMenuItem
            // 
            this.removeFilterToolStripMenuItem.Name = "removeFilterToolStripMenuItem";
            resources.ApplyResources(this.removeFilterToolStripMenuItem, "removeFilterToolStripMenuItem");
            this.removeFilterToolStripMenuItem.Click += new System.EventHandler(this.RemoveFilterToolStripMenuItem_Click);
            // 
            // Separador2
            // 
            this.Separador2.Name = "Separador2";
            resources.ApplyResources(this.Separador2, "Separador2");
            // 
            // HistoricoToolStripMenuItem
            // 
            this.HistoricoToolStripMenuItem.Name = "HistoricoToolStripMenuItem";
            resources.ApplyResources(this.HistoricoToolStripMenuItem, "HistoricoToolStripMenuItem");
            this.HistoricoToolStripMenuItem.Click += new System.EventHandler(this.HistoricoToolStripMenuItem_Click);
            // 
            // lblRecord
            // 
            resources.ApplyResources(this.lblRecord, "lblRecord");
            this.lblRecord.Name = "lblRecord";
            // 
            // EDExplorerFrm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listEvent);
            this.Controls.Add(this.progressReadAll);
            this.Controls.Add(this.lblRecord);
            this.Name = "EDExplorerFrm";
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
        private ProgressBar progressReadAll;
        private ColumnHeader timestamp;
        private ColumnHeader detail;
        private ContextMenuStrip contextCopy;
        private ToolStripMenuItem copyNameToolStripMenuItem;
        private ToolStripMenuItem filterNameToolStripMenuItem;
        private ToolStripMenuItem filterAlertToolStripMenuItem;
        private ToolStripMenuItem filterRecordToolStripMenuItem;
        private ToolStripMenuItem removeFilterToolStripMenuItem;
        private ToolStripMenuItem HistoricoToolStripMenuItem;
        private ToolStripSeparator Separador1;
        private ToolStripSeparator Separador2;
        private ColumnHeader sistema;
        private ColumnHeader record;
        private Label lblRecord;
        private ColumnHeader valor;
    }
}

