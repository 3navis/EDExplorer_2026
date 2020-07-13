using EDExplorer.Pantallas;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EDExplorer
{
    public partial class NotifyFrm : Form 
    {
        public NotifyFormBack nfb;
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

        //private Font fontElite = new Font(FontElite.private_fonts.Families[0], 14, FontStyle.Bold);
        public NotifyFrm(string t, NotifyFormBack n)
        {
            InitializeComponent();

            //lblText.Font = new Font(FontElite.private_fonts.Families[0], 14, FontStyle.Bold);
            //lblText.Font = fontElite;
            lblText.UseCompatibleTextRendering = true;
            lblText.Text = t;

            nfb = n;
            StartPosition = nfb.StartPosition;
            Location = nfb.Location;

            //StartPosition = FormStartPosition.Manual;
            //Rectangle desktopArea = Screen.GetWorkingArea(this);
            //Location = new Point(desktopArea.Right - Width, desktopArea.Bottom - Height);
            //this.Opacity = (double)Properties.Settings.Default.Opacidad / 100;
        }
        int nTick, maxTick;
        public void Show(int t)
        {
            timer = new Timer();
            timer.Tick += new EventHandler(frmTick);

            //timer.Tick += delegate { frm_Close(); };
            //timer.Interval = timeout;

            nTick = 0;
            maxTick = t / 1000;
            timer.Interval = 1000;

            timer.Start();
            Show();
        }

        //private void frm_Close()
        private void frmTick(object sender, EventArgs e)
        {
            nTick++;
            
            if (nTick > maxTick)
            {
                if (nfb != null)
                {
                    nfb.Close();
                    nfb = null;
                }
                timer.Stop();
                this.Hide();
                this.Close();
            }
            else
            {
                lblTick.Text = nTick.ToString("00");
                Refresh();
            }
        }
    }
}
