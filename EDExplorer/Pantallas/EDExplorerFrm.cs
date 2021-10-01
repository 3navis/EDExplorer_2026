using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace EDExplorer
{
    using M = Properties.Textos;
    
    public partial class EDExplorerFrm : Form
    {
        private Properties.Settings settings = Properties.Settings.Default;
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

        public void AddListItem(Interes item)
        {
            ListViewItem newItem = new ListViewItem(
                    new string[] {
                    logMonitor.currentTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    logMonitor.CurrentSystem,
                    item.BodyName.Replace(logMonitor.CurrentSystem,"").Trim(),
                    item.Nombre,
                    item.Detalle,
                    string.Empty, // ? "Ꚛ🌐֍֎۞ℳ♡Ꚛ"
                    item.ValorST
                    });

            if (item.isRecord)
            {
                Font fontBold = new Font(newItem.Font, FontStyle.Bold);

                newItem.UseItemStyleForSubItems = false;
                newItem.SubItems[3].Font = fontBold;
                newItem.SubItems[4].Font = fontBold;
                newItem.SubItems[4].Text += " (" + item.RecordDesc + ")";
                newItem.SubItems[5].Font = lblRecord.Font;
                newItem.SubItems[5].Text = lblRecord.Text;
                newItem.SubItems[6].Font = fontBold;
            }

            if (!mostrarSoloRecords || item.isRecord)
            {
                listEvent.Items.Add(newItem);
            }

            if (!logMonitor.ReadAllInProgress)
            {
                listEvent.Sort();
                newItem.EnsureVisible();
                
                // ** Limnpiar el guardado si estaba realizado
                itemsTodos = null;
            }
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

            // Guardar Records despues de leer fichero.
            settings.Alertas = JsonConvert.SerializeObject(logMonitor.basi.alertas.n);
            settings.Save(); 
        }

        private void ListEvent_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listEvent.FocusedItem.Bounds.Contains(e.Location))
                {
                    contextCopy.Show(Cursor.Position);
                    contextCopy.Items[0].Enabled = listEvent.SelectedItems.Count == 1;
                    
                    this.filterNameToolStripMenuItem.Text = M.str_Filtrar_Sistema + listEvent.FocusedItem.SubItems[1].Text + "\"";
                    this.filterAlertToolStripMenuItem.Text = M.str_Filtrar_Alerta + listEvent.FocusedItem.SubItems[3].Text + "\"";
                }
            }
        }

        private void CopyNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listEvent.FocusedItem.SubItems[1].Text);
            //Clipboard.SetText(listEvent.FocusedItem.Text);
        }

        // Guardar la lista completa para poder restaurarla
        ListViewItem[] itemsTodos;

        private void FilterNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Sistema = listEvent.FocusedItem.SubItems[1].Text;
            string AlertasIncluidas = "";

            if (itemsTodos == null)
            {
                itemsTodos = new ListViewItem[listEvent.Items.Count];
                listEvent.Items.CopyTo(itemsTodos, 0);
            }

            listEvent.Items.Clear();  //Borra el ListView
            List<ListViewItem> itemsAUX = new List<ListViewItem>();  //Lista Auxiliar para el filtrado

            //Recorre todos los items
            foreach (ListViewItem lvi in itemsTodos)
            {
                //Filtra los items que comienzan con el valor de textBox1.Text
                if (lvi.SubItems[1].Text == Sistema)
                {
                    // verificar que no existe ya esa alerta antes de insertar.
                    if (!AlertasIncluidas.Contains(lvi.SubItems[2].Text + lvi.SubItems[3].Text))
                    {
                        AlertasIncluidas += lvi.SubItems[2].Text + lvi.SubItems[3].Text + ",";
                        itemsAUX.Add(lvi);
                    }
                }
            }
            listEvent.Items.AddRange(itemsAUX.ToArray()); //Recargar el ListView
        }

        private void FilterAlertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Alerta = listEvent.FocusedItem.SubItems[3].Text;
            string AlertasIncluidas = "";

            if (itemsTodos == null)
            {
                itemsTodos = new ListViewItem[listEvent.Items.Count];
                listEvent.Items.CopyTo(itemsTodos, 0);
            }

            listEvent.Items.Clear();  //Borra el ListView
            List<ListViewItem> itemsAUX = new List<ListViewItem>();  //Lista Auxiliar para el filtrado

            //Recorre todos los items
            foreach (ListViewItem lvi in itemsTodos)
            {
                if (lvi.SubItems[3].Text == Alerta)
                    // verificar que no existe ya esa alerta antes de insertar.
                    if (!AlertasIncluidas.Contains(lvi.SubItems[1].Text + lvi.SubItems[2].Text))
                    {
                        AlertasIncluidas += lvi.SubItems[1].Text + lvi.SubItems[2].Text + ",";
                        itemsAUX.Add(lvi);
                    }
            }
            listEvent.Items.AddRange(itemsAUX.ToArray()); //Recargar el ListView
        }

        private void FilterRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Record = listEvent.FocusedItem.SubItems[5].Text;
            string AlertasIncluidas = "";

            if (itemsTodos == null)
            {
                itemsTodos = new ListViewItem[listEvent.Items.Count];
                listEvent.Items.CopyTo(itemsTodos, 0);
            }

            listEvent.Items.Clear();  //Borra el ListView
            List<ListViewItem> itemsAUX = new List<ListViewItem>();  //Lista Auxiliar para el filtrado

            //Recorre todos los items
            foreach (ListViewItem lvi in itemsTodos)
            {
                //Filtra los items que comienzan con el valor de textBox1.Text
                if (lvi.SubItems[5].Text != "")
                {
                    // verificar que no existe ya esa alerta antes de insertar.
                    if (!AlertasIncluidas.Contains(lvi.SubItems[3].Text))
                    {
                        AlertasIncluidas += lvi.SubItems[3].Text + ",";
                        itemsAUX.Add(lvi);
                    }
                }
            }
            listEvent.Items.AddRange(itemsAUX.ToArray()); //Recargar el ListView
        }

        private void RemoveFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (itemsTodos != null)
            {
                listEvent.Items.Clear();
                listEvent.Items.AddRange(itemsTodos.ToArray()); //Recargar el ListView
            }
        }

        private void HistoricoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (logMonitor.ReadAllComplete)
            {
                DialogResult confirmResult;
                confirmResult = MessageBox.Show(M.str_Desea_borrar_la_lista_actual_volver, M.str_Confirmar_Acci, MessageBoxButtons.OKCancel);
                if (confirmResult == DialogResult.Cancel)
                {
                    return;
                }
            }
            DateTime start = DateTime.Now;
            ReadAllJournals();
            MessageBox.Show(M.str_Tiempo_empleado + " " + (DateTime.Now - start).TotalSeconds.ToString("0.00") + "s.");

            // ** Limpiar el guardado si estaba realizado
            itemsTodos = null;
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
                    copyText.AppendLine(item.SubItems[0].Text + M.str_Sin_Inter);
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
            ReadAllJournals(30);
#endif
        }

        void listEvent_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            // Asociado a la propiedad {OwnerDraw = true}
            //if (Should_Filter(e.Item) == false)
            //if (true)
                //e.DrawDefault = true;
            //else
            //    e.DrawDefault = false;
        }
    }
}
