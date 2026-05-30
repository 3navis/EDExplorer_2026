using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace EDExplorer
{
    public class ShapePanel : Panel
    {
        public int BorderThickness { get; set; } = 2;

        public Color BorderColor { get; set; } = Color.Lime;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode =
                SmoothingMode.AntiAlias;

            using (Pen p = new Pen(BorderColor, BorderThickness))
            {
                e.Graphics.DrawRectangle(
                    p,
                    0,
                    0,
                    Width - BorderThickness,
                    Height - BorderThickness);
            }
        }
    }
}