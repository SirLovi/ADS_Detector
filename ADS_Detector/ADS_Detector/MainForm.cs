using System;
using System.Drawing;
using System.Windows.Forms;

namespace ADS_Detector
{
    public partial class MainForm : Form
    {
        public static MainForm Instance { get; private set; }
        private ADSDetector adsDetector;
        private NotifyIcon notifyIcon1;

        public MainForm()
        {
            InitializeComponent();
            Instance = this;
            this.notifyIcon1 = new NotifyIcon();

            notifyIcon1.Icon = SystemIcons.Information;
            notifyIcon1.ContextMenu = new ContextMenu(new MenuItem[]
            {
                new MenuItem("Open", (s, e) => { this.Show(); this.WindowState = FormWindowState.Normal; }),
                new MenuItem("Configuration", (s, e) => { new ConfigForm().ShowDialog(); }),
                new MenuItem("Exit", (s, e) => { this.Close(); })
            });
            var directories = System.Configuration.ConfigurationManager.AppSettings["WatchedDirectories"].Split(';');
            var fileTypes = System.Configuration.ConfigurationManager.AppSettings["WatchedFileTypes"].Split(';');
            var excludedDirs = System.Configuration.ConfigurationManager.AppSettings["ExcludedDirectories"].Split(';');
            var includeSubdirs = Boolean.Parse(System.Configuration.ConfigurationManager.AppSettings["IncludeSubdirectories"]);
            adsDetector = new ADSDetector(directories, fileTypes, excludedDirs, includeSubdirs);
            notifyIcon1.Visible = true;
        }
        public void RestartDetector()
        {
            var directories = System.Configuration.ConfigurationManager.AppSettings["WatchedDirectories"].Split(';');
            var fileTypes = System.Configuration.ConfigurationManager.AppSettings["WatchedFileTypes"].Split(';');
            var excludedDirs = System.Configuration.ConfigurationManager.AppSettings["ExcludedDirectories"].Split(';');
            var includeSubdirs = Boolean.Parse(System.Configuration.ConfigurationManager.AppSettings["IncludeSubdirectories"]);
            adsDetector.Stop();
            adsDetector = new ADSDetector(directories, fileTypes, excludedDirs, includeSubdirs);
        }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }
        public void ShowNotification(string message)
        {
            notifyIcon1.BalloonTipTitle = "ADS Detector";
            notifyIcon1.BalloonTipText = message;
            notifyIcon1.ShowBalloonTip(5000);
            //RestartDetector();
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            adsDetector.Stop();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            new ConfigForm().ShowDialog();
        }
    }
}