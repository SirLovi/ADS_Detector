using System;
using System.IO.Pipes;
using System.Text;

namespace ADS_Detector
{
    public static class NotificationApp
    {
        public static void SendNotification(string message)
        {
            try
            {
                using (var client = new NamedPipeClientStream(".", "ADSPipe", PipeDirection.Out))
                {
                    client.Connect();
                    var bytes = Encoding.UTF8.GetBytes(message);
                    client.Write(bytes, 0, bytes.Length);
                    client.WaitForPipeDrain();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }
    }
}