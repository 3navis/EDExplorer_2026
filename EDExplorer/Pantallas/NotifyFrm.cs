using EDExplorer.Pantallas;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EDExplorer
{
    public partial class NotifyFrm : Form 
    {
        public NotifyFormBack nfb;
        private Timer timer;
        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        private const int WS_EX_TOPMOST = 0x00000008;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= WS_EX_TOPMOST;
                return createParams;
            }
        }

        private Font fontElite = new Font(FontElite.private_fonts.Families[0], 14, FontStyle.Bold);
        public NotifyFrm(string text, NotifyFormBack n)
        {
            InitializeComponent();
            this.Closing += form_Closing;

            //lblText.Font = new Font(FontElite.private_fonts.Families[0], 14, FontStyle.Bold);
            lblText.Font = fontElite;
            lblText.UseCompatibleTextRendering = true;
            lblText.Text = text;

            nfb = n;
            StartPosition = nfb.StartPosition;
            Location = nfb.Location;

            //StartPosition = FormStartPosition.Manual;
            //Rectangle desktopArea = Screen.GetWorkingArea(this);
            //Location = new Point(desktopArea.Right - Width, desktopArea.Bottom - Height);
            //this.Opacity = (double)Properties.Settings.Default.Opacidad / 100;
        }

        public void Texto(string t)
        {
            lblText.Text = t;
        }
        public void Show(int timeout)
        {
            timer = new Timer();
            timer.Tick += delegate { Close(); };
            //timer.Tick += delegate { Visible = false; nfb.Visible = false; };
            timer.Interval = timeout;
            timer.Start();
            Show();
            //nfb.Show();
            //nfb.Refresh();
        }
        private void form_Closing(object sender, CancelEventArgs e)
        {
            if (nfb != null)
            {
                nfb.Close();
                nfb = null;
            }
        }

    }
}
