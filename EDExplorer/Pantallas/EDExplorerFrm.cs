using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace EDExplorer
{
    public partial class EDExplorerFrm : Form
    {
        private ListViewColumnSorter columnSorter;
        private LogMonitor logMonitor;
        private bool mostrarSoloRecords = false;
        public EDExplorerFrm(LogMonitor l)
        {
            InitializeComponent();

            Text = $"{Text} - v{Assembly.GetExecutingAssembly().GetName().Version}";
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            columnSorter = new ListViewColumnSorter();
            columnSorter.Order = SortOrder.Ascending;
            listEvent.ListViewItemSorter = columnSorter;
            logMonitor = l;
        }

        //public void AddListItem((string BodyName, string Description, string Detail) item)
        public void AddListItem(Interes item)
        {
            ListViewItem newItem = new ListViewItem(
                    new string[] {
                    //logMonitor.LastScan.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"),
                    logMonitor.currentTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    logMonitor.CurrentSystem,
                    item.BodyName.Replace(logMonitor.CurrentSystem,"").Trim(),
                    item.Nombre,
                    item.Detalle,
                    (item.isRecord) ? "R" : string.Empty, // ? "Ꚛ🌐֍֎۞ℳ♡Ꚛ"
                    item.ValorST
                    });

            if (item.Nombre.Contains("Criterios Múltiples") || item.Nombre.Contains("Record Personal"))
            {
                newItem.UseItemStyleForSubItems = false;
                newItem.SubItems[3].Font = new Font(newItem.Font, FontStyle.Bold);
                newItem.SubItems[4].Font = new Font(newItem.Font, FontStyle.Bold);
                newItem.SubItems[5].Font = lblRecord.Font;
                newItem.SubItems[5].Text = lblRecord.Text;
            }

            if (!mostrarSoloRecords || item.isRecord)
            {
                listEvent.Items.Add(newItem);
            }

            if (!logMonitor.ReadAllInProgress)
            {
                listEvent.Sort();
                newItem.EnsureVisible();
            }
        }

        private void BtnReadAll_Click(object sender, EventArgs e)
        {
            if (logMonitor.ReadAllComplete)
            {
                DialogResult confirmResult;
                confirmResult = MessageBox.Show("Desea borrar la lista actual y volver a leer el diario de vuelo?", "Confirmar Refresco", MessageBoxButtons.OKCancel);
                if (confirmResult == DialogResult.Cancel)
                {
                    return;
                }
            }
            DateTime start = DateTime.Now;
            ReadAllJournals();
            lblTime.Text = (DateTime.Now - start).TotalSeconds.ToString("0.00")+"s.";
        }

        private void ReadAllJournals(int ultimos = 0, bool soloRecords = false)
        {
            mostrarSoloRecords = soloRecords;

            listEvent.BeginUpdate();
            listEvent.Items.Clear();
            listEvent.ListViewItemSorter = null;
            logMonitor.ReadAll(progressReadAll, ultimos);
            listEvent.ListViewItemSorter = columnSorter;
            listEvent.Sort();
            listEvent.EndUpdate();

            mostrarSoloRecords = false;
        }

        private void ListEvent_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listEvent.FocusedItem.Bounds.Contains(e.Location))
                {
                    contextCopy.Show(Cursor.Position);
                    contextCopy.Items[0].Enabled = listEvent.SelectedItems.Count == 1;
                }
            }
        }

        private void CopyNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listEvent.FocusedItem.SubItems[1].Text);
            //Clipboard.SetText(listEvent.FocusedItem.Text);
        }

        private void CopyAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyAllSelected();
        }

        private void ListEvent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Control)
            {
                CopyAllSelected();
            }
        }

        private void CopyAllSelected()
        {
            StringBuilder copyText = new StringBuilder();
            foreach (ListViewItem item in listEvent.SelectedItems)
            {
                if (item.SubItems.Count == 5)
                {
                    copyText.AppendLine(
                        item.SubItems[0].Text + ";" +
                        item.SubItems[1].Text + ";" +
                        item.SubItems[2].Text + ";" +
                        item.SubItems[3].Text + ";" +
                        item.SubItems[4].Text
                        );
                }
                else
                {
                    copyText.AppendLine(item.SubItems[0].Text + " - Sin Interés");
                }

            }
            Clipboard.SetText(copyText.ToString());
        }

        private void EDExplorerFrm_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.WindowSize.Height != 0)
            {
                bool offScreen = true;
                Screen[] screens = Screen.AllScreens;
                foreach (Screen screen in screens)
                {
                    Rectangle formRectangle = new Rectangle(Properties.Settings.Default.WindowLocation, Properties.Settings.Default.WindowSize);

                    if (screen.WorkingArea.Contains(formRectangle))
                    {
                        offScreen = false;
                    }
                }

                if (!offScreen)
                {
                    Location = Properties.Settings.Default.WindowLocation;
                    Size = Properties.Settings.Default.WindowSize;
                }
            }

            // Precarga de datos al abrir
            //ReadAll(ProgressBar progressBar, int ultimos = 30);
        }

        private void EDExplorerFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.WindowSize = Size;
            Properties.Settings.Default.WindowLocation = Location;
            Properties.Settings.Default.Save();

            // Intento cerrar la aplicacion al cerrar la ventana.
            Application.Exit();
        }

        private void ListEvent_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            bool isNumber;

            if (e.Column == columnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (columnSorter.Order == SortOrder.Ascending)
                {
                    columnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    columnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                columnSorter.SortColumn = e.Column;
                columnSorter.Order = SortOrder.Ascending;
            }

            isNumber = (sender as ListView).Columns[e.Column].Tag == "number";

            listEvent.ListViewItemSorter = new DoubleComparer(e.Column, isNumber, columnSorter.Order);     
            listEvent.Sort();
        }

        public class DoubleComparer : IComparer
        {
            private int _colIndex = 0;
            private SortOrder _order = SortOrder.Ascending;
            private bool _tipoNumber = false;
            public DoubleComparer(int colIndex, bool tipoNumber, SortOrder order)
            {
                _colIndex = colIndex;
                _order = order;
                _tipoNumber = tipoNumber;
            }
            public int Compare(object x, object y)
            {
                if (_tipoNumber)
                {
                    double nx, ny;
                    if (!double.TryParse((x as ListViewItem).SubItems[_colIndex].Text, out nx)) nx = -1;
                    if (!double.TryParse((y as ListViewItem).SubItems[_colIndex].Text, out ny)) ny = -1;
                    return (_order == SortOrder.Ascending) ? nx.CompareTo(ny) : ny.CompareTo(nx);
                }
                else
                {
                    string s1 = (x as ListViewItem).SubItems[_colIndex].Text;
                    string s2 = (y as ListViewItem).SubItems[_colIndex].Text;
                    return (_order == SortOrder.Ascending) ? s1.CompareTo(s2) : s2.CompareTo(s1);
                }
            }
        }

        private void EDExplorerFrm_Shown(object sender, EventArgs e)
        {
#if !DEBUG
            ReadAllJournals(30);
#else
            ReadAllJournals(3);
#endif
        }

        private void CopyJournalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder copyText = new StringBuilder();
            foreach (ListViewItem item in listEvent.SelectedItems)
            {
                string bodyName = item.SubItems[1].Text + " " + item.SubItems[2].Text;

                var bodyData = logMonitor.SystemBody.Where(body => body.Value.BodyName == bodyName);
                if (bodyData.Count() == 0)
                {
                    bodyName = item.SubItems[2].Text;
                    bodyData = logMonitor.SystemBody.Where(body => body.Value.BodyName == bodyName);
                }
                if (bodyData.Count() > 0)
                    copyText.AppendLine(logMonitor.SystemBody.Where(body => body.Value.BodyName == bodyName).First().Value.JournalEntry);
            }
            if (copyText.Length == 0)
            {
                MessageBox.Show("No hay entradas seleccionadas en el Journal para copiar.", "Sin Datos", MessageBoxButtons.OK);
            }
            else
            {
                Clipboard.SetText(copyText.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (logMonitor.ReadAllComplete)
            {
                DialogResult confirmResult;
                confirmResult = MessageBox.Show("Desea borrar la lista actual y volver a leer el diario de vuelo?", "Confirmar Refresco", MessageBoxButtons.OKCancel);
                if (confirmResult == DialogResult.Cancel)
                {
                    return;
                }
            }
            DateTime start = DateTime.Now;
            ReadAllJournals(soloRecords : true);
            lblTime.Text = (DateTime.Now - start).TotalSeconds.ToString("0.00") + "s.";
        }
    }
}
