using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EDExplorer
{
    public class JumpSummaryFrm : Form
    {
        private Label lblTitle;
        private Label lblBody;
        private Label lblTick;
        private Timer timer;
        private int nTick, maxTick;
        private Font fontElite = new Font(FontElite.private_fonts.Families[0], 11, FontStyle.Regular);

        protected override bool ShowWithoutActivation => true;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000008; // WS_EX_TOPMOST
                return cp;
            }
        }

        public JumpSummaryFrm(string title, string body)
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            Width = 420;
            Height = 160;

            lblTitle = new Label { Left = 12, Top = 8, Width = Width - 24, Height = 28, Font = new Font(fontElite.FontFamily, 12, FontStyle.Bold), Text = title, ForeColor = Color.White };
            lblBody = new Label { Left = 12, Top = 40, Width = Width - 24, Height = Height - 80, Font = new Font(fontElite.FontFamily, 10, FontStyle.Regular), Text = body, AutoEllipsis = true, ForeColor = Color.White };
            lblTick = new Label { Left = Width - 60, Top = 8, Width = 48, Height = 20, Font = new Font(fontElite.FontFamily, 9, FontStyle.Regular), Text = "00", TextAlign = ContentAlignment.MiddleCenter, ForeColor = Color.White };

            BackColor = Color.FromArgb(32, 32, 32);
            Controls.Add(lblTitle);
            Controls.Add(lblBody);
            Controls.Add(lblTick);

            Rectangle desktopArea = Screen.GetWorkingArea(this);
            int dx = desktopArea.Right - Width - 16;
            int dy = desktopArea.Bottom / 2 - Height / 2;
            Location = new Point(dx, dy);
        }

        public void ShowWithTimeout(int mls)
        {
            timer = new Timer();
            nTick = 0;
            maxTick = Math.Max(1, mls / 1000);
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
            Show();
            Application.Run(this);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            nTick++;
            if (nTick > maxTick)
            {
                timer.Stop();
                Close();
                Application.ExitThread();
            }
            else
            {
                lblTick.Text = nTick.ToString("00");
                lblTick.Refresh();
            }
        }
    }
}