using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace ADS_Detector
{
    public partial class Service1 : ServiceBase
    {
        private static List<FileSystemWatcher> watchers = new List<FileSystemWatcher>();
        private static string[] watchedDirectories;
        private static string[] watchedFileTypes;
        private static string[] excludedDirectories;
        private static bool IncludeSubdirectories { get; set; }

        public Service1()
        {
            InitializeComponent();
            watchedDirectories = System.Configuration.ConfigurationManager.AppSettings["WatchedDirectories"].Split(';');
            IncludeSubdirectories = Boolean.Parse(System.Configuration.ConfigurationManager.AppSettings["IncludeSubdirectories"]);
            watchedFileTypes = System.Configuration.ConfigurationManager.AppSettings["WatchedFileTypes"].Split(';');
            excludedDirectories = System.Configuration.ConfigurationManager.AppSettings["ExcludedDirectories"].Split(';');
        }

        protected override void OnStart(string[] args)
        {
            StartADSWatcher();
        }

        protected override void OnStop()
        {
            foreach (var watcher in watchers)
            {
                watcher.Dispose();
            }
        }

        private void StartADSWatcher()
        {
            foreach (string path in watchedDirectories)
            {
                var watcher = new FileSystemWatcher();
                watcher.Path = path;
                watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                watcher.Changed += new FileSystemEventHandler(OnChanged);
                watcher.Renamed += new RenamedEventHandler(OnChanged);
                watcher.EnableRaisingEvents = true;
                watcher.IncludeSubdirectories = IncludeSubdirectories;
                watchers.Add(watcher);
            }
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            if (watchedFileTypes.Any(e.FullPath.ToLower().EndsWith) && !excludedDirectories.Any(e.FullPath.ToLower().StartsWith))
            {
                if (DetectADS(e.FullPath))
                {
                    NotificationApp.SendNotification($"ADS detected in {e.FullPath}");
                }
            }
        }

        private static bool DetectADS(string path)
        {
            try
            {
                var streams = ADSHelper.GetAlternateStreams(path);
                if (streams.Any(s => !s.Equals(":$DATA", StringComparison.OrdinalIgnoreCase)))
                {
                    foreach (var stream in streams)
                    {
                        Debug.WriteLine($"ADS detected: {stream}");
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error detecting ADS: {ex.Message}");
            }

            return false;
        }
    }
}
