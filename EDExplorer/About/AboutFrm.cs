using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace EDExplorer.Pantallas
{
    public partial class AboutFrm : Form
    {
        public AboutFrm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //f_Main();
        }

        void Dibujar3(System.Windows.Forms.PictureBox pb, PointF[,] t)
        {
            GraphicsPath path = new GraphicsPath(); ;
            int j, k, l;
            l = t.GetLength(1) - 1;

            for (j = 0; j < l; j++)
            {
                for (k = 0; k <= j; k++)
                {
                    path.AddLine(t[j + 1, k], t[j, k]);
                    path.AddLine(t[j, k], t[j + 1, k + 1]);
                }
                for (k = j; k >= 0; k--)
                {
                    path.AddLine(t[j + 1, k + 1], t[j + 1, k]);
                }
            }

            //////////////////////////////////////////////////////////
            Graphics g;
            float ex, ey;

            g = pb.CreateGraphics();

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            ex = pb.Size.Width / 1000f;
            ey = pb.Size.Height / 1000f;

            Matrix myMatrix = new Matrix();
            myMatrix.Scale(ex, ey);
            g.Transform = myMatrix;

            Pen pen = new Pen(Color.Green, 1);
            
            g.DrawPath(pen, path);
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

            pm = new PointF(0,0);
            pm.X = ValorM2(p1.X, p2.X, fy);
            pm.Y = ValorM2(p1.Y, p2.Y, fx);
            return pm;
        }

        readonly Random rnd = new Random();

        PointF Rnd(PointF p, int n)
        {
            int ff = 20;
            float fx = (ff - rnd.Next(0, 2 * ff)) / n;
            float fy = (ff - rnd.Next(0, 2 * ff)) / n;

            p.X += fx;
            p.Y += fy;

            return p;
        }
        
        void f_Main()
        {
            PointF[,] p1, p2;

            int n = 2;
            p1 = new PointF[n, n];
            p2 = new PointF[n, n];

            p2[0, 0] = new PointF(1000 / 2, 50);
            p2[1, 0] = new PointF(50, 1000 - 3*50);
            p2[1, 1] = new PointF(1000 - 50, 1000 - 3*50);

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

                switch (i)
                {
                    case 1: Dibujar3(paso1, p2); break;
                    case 2: Dibujar3(paso2, p2); break;
                    case 3: Dibujar3(paso3, p2); break;
                    default: break;
                }
            }

            Dibujar3(frmLienzo, p2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Refresh();

            f_Main();
        }
    }
}
