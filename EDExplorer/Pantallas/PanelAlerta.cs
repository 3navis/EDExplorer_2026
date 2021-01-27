using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EDExplorer.Pantallas
{
    public partial class PanelAlerta : UserControl
    {
        public Alerta alerta;
        public TipoParametro tipoParam;
        public PanelAlerta(KeyValuePair<Alerta, DetallesAlerta> a)
        {
            InitializeComponent();

            alerta = a.Key;

            checkAlerta.Checked = a.Value.flag;
            checkAlerta.Text = a.Value.nombreCheck;
            textAlerta.Text = a.Value.nombre;
            textDesde.Text = a.Value.desde.ToString();
            textHasta.Text = a.Value.hasta.ToString();
            tipoParam = a.Value.tipoParam;
            ActualizarImagen(tipoParam);
            labelUnidades.Text = a.Value.unidades;
        }

        private void ActualizarImagen(TipoParametro t)
        {
            switch (t)
            {
                case TipoParametro.None:
                    picIncluir.Visible = false;
                    textDesde.Visible = false;
                    textHasta.Visible = false;
                    labelUnidades.Visible = false;
                    break;
                case TipoParametro.ValorDesde:
                    picIncluir.Image = Properties.Resources.valor_desde;
                    textDesde.Visible = true;
                    textHasta.Visible = false;
                    break;
                case TipoParametro.ValorHasta:
                    picIncluir.Image = Properties.Resources.valor_hasta;
                    textDesde.Visible = false;
                    textHasta.Visible = true;
                    break;
                case TipoParametro.RangoIncluido:
                    picIncluir.Image = Properties.Resources.rango_incluido;
                    textDesde.Visible = true;
                    textHasta.Visible = true;
                    break;
                case TipoParametro.RangoExcluido:
                    picIncluir.Image = Properties.Resources.rango_excluido;
                    textDesde.Visible = true;
                    textHasta.Visible = true;
                    break;
                default:
                    break;
            }
        }

        public void Update(Alertas a)
        {
            a.n[alerta].flag = checkAlerta.Checked;
            a.n[alerta].nombre = textAlerta.Text;
            a.n[alerta].desde = Convert.ToDouble(textDesde.Text);
            a.n[alerta].hasta = Convert.ToDouble(textHasta.Text);
            a.n[alerta].tipoParam = this.tipoParam;
        }
        public void Checked(bool f)
        {
            checkAlerta.Checked = f;
        }

        private void picIncluir_Click(object sender, EventArgs e)
        {
            switch (tipoParam)
            {
                case TipoParametro.ValorHasta:
                    tipoParam = TipoParametro.ValorDesde;
                    break;
                case TipoParametro.ValorDesde:
                    tipoParam = TipoParametro.RangoIncluido;
                    break;
                case TipoParametro.RangoIncluido:
                    tipoParam = TipoParametro.RangoExcluido;
                    break;
                case TipoParametro.RangoExcluido:
                    tipoParam = TipoParametro.ValorHasta;
                    break;
                default:
                    break;
            }
            
            ActualizarImagen(tipoParam);
        }
    }
}
