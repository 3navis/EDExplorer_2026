using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDExplorer.Pantallas
{
    public partial class PanelAlerta : UserControl
    {
        public Alerta alerta;
        public PanelAlerta(KeyValuePair<Alerta, DetallesAlerta> a)
        {
            InitializeComponent();

            alerta = a.Key;

            checkAlerta.Checked = a.Value.flag;
            checkAlerta.Text = a.Value.tipo;
            textAlerta.Text = a.Value.nombre;
            textDesde.Text = a.Value.desde.ToString();
            labelUnidades.Text = a.Value.unidades;
        }

        public void Update(Alertas a)
        {
            a.n[alerta].flag = checkAlerta.Checked;
            a.n[alerta].nombre = textAlerta.Text;
            a.n[alerta].desde = Convert.ToDouble(textDesde.Text);
        }
        public void Checked(bool f)
        {
            checkAlerta.Checked = f;
        }

    }
}
