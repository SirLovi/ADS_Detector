using System;
using System.Windows.Forms;
using System.Configuration;

namespace ADS_Detector
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
            txtWatchedDirectories.Text = System.Configuration.ConfigurationManager.AppSettings["WatchedDirectories"];
            chkIncludeSubdirectories.Checked = Boolean.Parse(System.Configuration.ConfigurationManager.AppSettings["IncludeSubdirectories"]);
            txtWatchedFileTypes.Text = System.Configuration.ConfigurationManager.AppSettings["WatchedFileTypes"];
            txtExcludedDirectories.Text = System.Configuration.ConfigurationManager.AppSettings["ExcludedDirectories"];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["WatchedDirectories"].Value = txtWatchedDirectories.Text;
            config.AppSettings.Settings["IncludeSubdirectories"].Value = chkIncludeSubdirectories.Checked.ToString();
            config.AppSettings.Settings["WatchedFileTypes"].Value = txtWatchedFileTypes.Text;
            config.AppSettings.Settings["ExcludedDirectories"].Value = txtExcludedDirectories.Text;
            config.Save(ConfigurationSaveMode.Modified);
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
            MainForm.Instance.RestartDetector();
            this.Close();
        }

    }
}