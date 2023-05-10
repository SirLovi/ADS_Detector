using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS_Detector_Notifications
{
    partial class ConfigForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtWatchedDirectories;
        private System.Windows.Forms.CheckBox chkIncludeSubdirectories;
        private System.Windows.Forms.TextBox txtWatchedFileTypes;
        private System.Windows.Forms.TextBox txtExcludedDirectories;
        private System.Windows.Forms.Button btnSave;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtWatchedDirectories = new System.Windows.Forms.TextBox();
            this.chkIncludeSubdirectories = new System.Windows.Forms.CheckBox();
            this.txtWatchedFileTypes = new System.Windows.Forms.TextBox();
            this.txtExcludedDirectories = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
        }
    }
}

