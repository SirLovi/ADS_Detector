using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;

namespace ADS_Detector_Notifications
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
            this.Close();
        }
    }
}
