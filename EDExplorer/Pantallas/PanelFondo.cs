using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDExplorer
{
    public partial class PanelFondo : UserControl
    {
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x00000020;
                return cp;
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Do not paint background.
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.Clear(System.Drawing.Color.White);
            //e.Graphics.CopyFromScreen(this.PointToScreen(new Point(0, 0)), new Point(0, 0), new Size(this.Width, this.Height));
            e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.FromArgb(120, System.Drawing.Color.Gray)), new Rectangle(new Point(0, 0), this.Size));
        }
        public PanelFondo()
        {
            InitializeComponent();
        }
    }
}
