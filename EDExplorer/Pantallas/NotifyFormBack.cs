using System.Drawing;
using System.Windows.Forms;

namespace EDExplorer.Pantallas
{
    public partial class NotifyFormBack : Form
    {
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
        public NotifyFormBack()
        {
            InitializeComponent();

            Opacity = (double)Properties.Settings.Default.Opacidad / 100;
            StartPosition = FormStartPosition.Manual;
            Rectangle desktopArea = Screen.GetWorkingArea(this);
            Location = new Point(desktopArea.Right - Width, desktopArea.Bottom - Height);
        }
    }
}
