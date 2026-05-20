using EDExplorer.Pantallas;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EDExplorer
{
    public partial class NotifyFrm : Form
    {
        public NotifyFormBack fondo;
        private Timer timer;

        //Use this property if you want to show a top-level window, 
        //but don't want to interrupt a user's work by taking the input focus 
        //away from the current window.
        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x00000008; // WS_EX_TOPMOST
                return createParams;
            }
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    // Call the OnPaint method of the base class.  
        //    base.OnPaint(e);
        //    TransparencyKey = BackColor;
        //}

        private Font fontElite = new Font(FontElite.private_fonts.Families[0], 11, FontStyle.Regular);
        public NotifyFrm(string t, TipoEvento e)
        {
            InitializeComponent();
            fondo = new NotifyFormBack();
            fondo.StartPosition = FormStartPosition.Manual;
            fondo.ShowInTaskbar = false;
            fondo.Owner = this;

            /////////////////////////////////////////////////////////////
            // Recolocar en funcion de la pantalla en la que se encuentra
            Rectangle desktopArea = Screen.GetWorkingArea(this);
            int dx, dy, margen = 8;

            dx = desktopArea.Right - Width;
            dy = desktopArea.Bottom / 2 - Height / 2;

            switch (e)
            {
                case TipoEvento.Hyperspace:
                case TipoEvento.Jump:
                    dy = desktopArea.Bottom / 2;
                    dx = desktopArea.Right - Width - margen;
                    break;
                default:
                    dy = desktopArea.Bottom / 2 - Height / 2;
                    break;
            }

            t = t.ToUpper();

            Location = new Point(dx, dy);
            fondo.Location = Location;

            // Mostrar el fondo después de fijar la posición para evitar parpadeo
            fondo.Show();

            lblTitulo.Font = fontElite;
            lblText.Font = fontElite;
            lblText.UseCompatibleTextRendering = true;

            int p = t.IndexOf("\r\n");
            if (p > 0)
            {
                lblTitulo.Text = t.Substring(0, p);
                lblText.Text = t.Substring(p + 2);
            }
            else
            {
                lblTitulo.Text = t;
                lblText.Text = String.Empty;
            }
        }
        int nTick, maxTick;
        public void Show(int t)
        {
            timer = new Timer();
            //timer.Tick += new EventHandler(frmTick);
            //timer.Tick += delegate { frmTick(); };
            timer.Tick += frmTick;

            nTick = 0;
            maxTick = t / 1000;
            timer.Interval = 1000;

            timer.Start();
            Show();
        }
        private void frmTick(object sender, EventArgs e)
        {
            nTick++;

            if (nTick > maxTick)
            {
                timer.Stop();
                //Hide();

                if (fondo != null)
                {
                    fondo.Close();
                    fondo = null;
                }

                Close();
            }
            else
            {
                lblTick.Text = nTick.ToString("00");
                lblTick.Refresh();
            }
        }

        private void lblText_Click(object sender, EventArgs e)
        {

        }

        private void NotifyFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //basi.configuracionFrm = null;
        }
    }
}
