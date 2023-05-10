using System;
using System.Drawing;
using System.IO.Pipes;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace ADS_Detector_Notifications
{
    public partial class MainForm : Form
    {
        private NamedPipeServerStream pipeServer;

        public MainForm()
        {
            InitializeComponent();

            notifyIcon1.Icon = SystemIcons.Information;
            notifyIcon1.ContextMenu = new ContextMenu(new MenuItem[]
            {
                new MenuItem("Open", (s, e) => { this.Show(); this.WindowState = FormWindowState.Normal; }),
                new MenuItem("Configuration", (s, e) => { new ConfigForm().ShowDialog(); }),
                new MenuItem("Restart Service", (s, e) => { RestartService(); }),
                new MenuItem("Exit", (s, e) => { this.Close(); })
            });
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            pipeServer = new NamedPipeServerStream("ADSPipe", PipeDirection.InOut, 1, PipeTransmissionMode.Message);
            pipeServer.WaitForConnection();

            BeginRead();
        }
        private void RestartService()
        {
            ServiceController service = new ServiceController("ADS_Detector");
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(5000);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void BeginRead()
        {
            byte[] buffer = new byte[4096];
            pipeServer.BeginRead(buffer, 0, buffer.Length, EndReadCallBack, buffer);
        }

        private void EndReadCallBack(IAsyncResult result)
        {
            try
            {
                int readBytes = pipeServer.EndRead(result);
                if (readBytes > 0)
                {
                    byte[] buffer = result.AsyncState as byte[];
                    string message = Encoding.UTF8.GetString(buffer, 0, readBytes);
                    ShowNotification(message);
                    BeginRead();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }

        private void ShowNotification(string message)
        {
            notifyIcon1.BalloonTipTitle = "ADS Detector";
            notifyIcon1.BalloonTipText = message;
            notifyIcon1.ShowBalloonTip(5000);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            pipeServer?.Close();
        }
    }
}
