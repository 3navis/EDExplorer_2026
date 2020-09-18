using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace EDExplorer.About
{
    public partial class UserAbout : UserControl
    {
        public UserAbout()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Main(g);
        }

        Boolean flgDirect = true;

        void Dibujar(Graphics g, PointF[,] t)
        {
            int j, k, l;
            Pen pen = new Pen(Color.Green, 1);
            l = t.GetLength(1) - 1;

            for (j = 0; j < l; j++)
            {
                for (k = 0; k <= j; k++)
                {
                    g.DrawLine(pen, t[j, k].X, t[j, k].Y, t[j + 1, k].X, t[j + 1, k].Y);
                    g.DrawLine(pen, t[j, k].X, t[j, k].Y, t[j + 1, k + 1].X, t[j + 1, k + 1].Y);
                    g.DrawLine(pen, t[j + 1, k].X, t[j + 1, k].Y, t[j + 1, k + 1].X, t[j + 1, k + 1].Y);
                }

            }
        }

        void Dibujar2(Graphics g, PointF[,] t)
        {
            int j, k, l;
            GraphicsPath path = new GraphicsPath(); ;
            Pen pen = new Pen(Color.Green, 1);
            l = t.GetLength(1) - 1;

            for (j = 0; j < l; j++)
            {
                for (k = 0; k <= j; k++)
                {
                    path.AddLine(t[j + 1, k], t[j, k]);
                    path.AddLine(t[j, k], t[j + 1, k + 1]);
                }
                for (k = j; k > 0; k--)
                {
                    path.AddLine(t[j + 1, k + 1], t[j + 1, k]);
                }
            }

            g.DrawPath(pen, path);
        }

        float ValorM(float a, float b)
        {
            float c;
            if (a > b) { c = a; a = b; b = c; }
            c = rnd.Next(0, (int)(b - a)) + a;
            return c;
        }

        float ValorM2(float a, float b, float ff)
        {
            float c;
            if (a > b) { c = a; a = b; b = c; }
            c = a + (b - a) / 2 + ff;
            return c;
        }

        PointF PointM(PointF p1, PointF p2)
        {
            PointF pm;
            float fx, fy;

            fx = Math.Abs(p1.X - p2.X) / 2;
            fy = Math.Abs(p1.Y - p2.Y) / 2;

            fx = rnd.Next(0, (int)fx) - fx / 2;
            fy = rnd.Next(0, (int)fy) - fy / 2;

            pm = new PointF(0, 0);
            pm.X = ValorM2(p1.X, p2.X, fy);
            pm.Y = ValorM2(p1.Y, p2.Y, fx);
            return pm;
        }

        Random rnd = new Random();

        PointF Rnd(PointF p, int n)
        {
            int ff = 20;
            float fx = (ff - rnd.Next(0, 2 * ff)) / n;
            float fy = (ff - rnd.Next(0, 2 * ff)) / n;

            p.X += fx;
            p.Y += fy;

            return p;
        }

        void Main(Graphics g)
        {
            PointF[,] p1, p2;

            int n = 2;
            p1 = new PointF[n, n];
            p2 = new PointF[n, n];

            p2[0, 0] = new PointF(200, 20);
            p2[1, 0] = new PointF(30, 300);
            p2[1, 1] = new PointF(400, 300);

            int j, k, j2, k2;
            int prof = 7;
            for (int i = 1; i <= prof; i++)
            {
                p1 = p2;
                n = p1.GetLength(1) * 2 - 1;
                p2 = new PointF[n, n];

                n = p1.GetLength(1) - 1;
                for (j = 0; j <= n; j++)
                {
                    for (k = 0; k <= j; k++)
                    {
                        j2 = 2 * j;
                        k2 = 2 * k;
                        //p1[j, k] = Rnd(p1[j, k],i);
                        p2[j2, k2] = p1[j, k];
                        if (k < j)
                        {
                            p2[j2, k2 + 1] = PointM(p1[j, k], p1[j, k + 1]);
                        }
                        if (j < n)
                        {
                            p2[j2 + 1, k2] = PointM(p1[j, k], p1[j + 1, k]);
                            p2[j2 + 1, k2 + 1] = PointM(p1[j, k], p1[j + 1, k + 1]);
                        }

                    }
                }
            }

            if (flgDirect) Dibujar2(g, p2);
            else Dibujar(g, p2);
        }
    }
}
