using System.Drawing;
using System.Windows.Forms;

namespace EDExplorer.Pantallas
{
    public partial class NotifyFormBack : Form
    {
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
                createParams.ExStyle |= 0x00000008; // WS_EX_TOPMOST;
                return createParams;
            }
        }
        public NotifyFormBack()
        {
            InitializeComponent();
            Opacity = (double)Properties.Settings.Default.Opacidad / 100;

            Rectangle desktopArea = Screen.GetWorkingArea(this);
            Location = new Point(desktopArea.Right - Width, desktopArea.Bottom - Height);
        }
    }
}
