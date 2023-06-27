using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.IO;

namespace ADS_Detector
{
    public partial class ADSDetector : Component
    {
        private List<FileSystemWatcher> watchers = new List<FileSystemWatcher>();
        private string[] watchedDirectories;
        private string[] watchedFileTypes;
        private string[] excludedDirectories;
        private bool IncludeSubdirectories { get; set; }

        public ADSDetector(string[] directories, string[] fileTypes, string[] excludedDirs, bool includeSubdirs)
        {
            watchedDirectories = directories;
            watchedFileTypes = fileTypes;
            excludedDirectories = excludedDirs;
            IncludeSubdirectories = includeSubdirs;
            Start();
        }

        public void Start()
        {
            Stop();
            StartADSWatcher();
        }

        public void Stop()
        {
            foreach (var watcher in watchers)
            {
                watcher.Dispose();
            }
            watchers.Clear();
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

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            if (watchedFileTypes.Any(e.FullPath.ToLower().EndsWith) && !excludedDirectories.Any(e.FullPath.ToLower().StartsWith))
            {
                if (DetectADS(e.FullPath))
                {
                    MainForm.Instance.Invoke(new Action(() => MainForm.Instance.ShowNotification($"ADS detected in {e.FullPath}")));
                }
            }
        }

        private static bool DetectADS(string path)
        {
            try
            {
                var streams = ADSHelper.GetAlternateStreams(path);
                
                if (streams.Count() > 1)
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