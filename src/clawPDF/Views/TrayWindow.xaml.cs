using System.Windows;
using System.Windows.Forms;

namespace clawSoft.clawPDF.Views
{
    public partial class TrayWindow : Window
    {
        private NotifyIcon notifyIcon;

        public TrayWindow()
        {
            InitializeComponent();
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new System.Drawing.Icon("clawPDF.ico");
            notifyIcon.Text = "clawPDF";
            notifyIcon.MouseDoubleClick += NotifyIcon_MouseDoubleClick;
            notifyIcon.BalloonTipClicked += NotifyIcon_BalloonTipClicked;
            notifyIcon.Visible = true;
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        private void NotifyIcon_BalloonTipClicked(object sender, System.EventArgs e)
        {
        }
    }
}