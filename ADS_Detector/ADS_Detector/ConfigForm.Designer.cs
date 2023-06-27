namespace ADS_Detector
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtWatchedDirectories = new System.Windows.Forms.TextBox();
            this.chkIncludeSubdirectories = new System.Windows.Forms.CheckBox();
            this.txtWatchedFileTypes = new System.Windows.Forms.TextBox();
            this.txtExcludedDirectories = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtWatchedDirectories
            // 
            this.txtWatchedDirectories.Location = new System.Drawing.Point(91, 113);
            this.txtWatchedDirectories.Name = "txtWatchedDirectories";
            this.txtWatchedDirectories.Size = new System.Drawing.Size(100, 20);
            this.txtWatchedDirectories.TabIndex = 0;
            // 
            // chkIncludeSubdirectories
            // 
            this.chkIncludeSubdirectories.AutoSize = true;
            this.chkIncludeSubdirectories.Location = new System.Drawing.Point(362, 151);
            this.chkIncludeSubdirectories.Name = "chkIncludeSubdirectories";
            this.chkIncludeSubdirectories.Size = new System.Drawing.Size(80, 17);
            this.chkIncludeSubdirectories.TabIndex = 1;
            this.chkIncludeSubdirectories.Text = "checkBox1";
            this.chkIncludeSubdirectories.UseVisualStyleBackColor = true;
            // 
            // txtWatchedFileTypes
            // 
            this.txtWatchedFileTypes.Location = new System.Drawing.Point(91, 151);
            this.txtWatchedFileTypes.Name = "txtWatchedFileTypes";
            this.txtWatchedFileTypes.Size = new System.Drawing.Size(100, 20);
            this.txtWatchedFileTypes.TabIndex = 2;
            // 
            // txtExcludedDirectories
            // 
            this.txtExcludedDirectories.Location = new System.Drawing.Point(91, 204);
            this.txtExcludedDirectories.Name = "txtExcludedDirectories";
            this.txtExcludedDirectories.Size = new System.Drawing.Size(100, 20);
            this.txtExcludedDirectories.TabIndex = 3;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtExcludedDirectories);
            this.Controls.Add(this.txtWatchedFileTypes);
            this.Controls.Add(this.chkIncludeSubdirectories);
            this.Controls.Add(this.txtWatchedDirectories);
            this.Name = "ConfigForm";
            this.Text = "ConfigForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtWatchedDirectories;
        private System.Windows.Forms.CheckBox chkIncludeSubdirectories;
        private System.Windows.Forms.TextBox txtWatchedFileTypes;
        private System.Windows.Forms.TextBox txtExcludedDirectories;
    }
}