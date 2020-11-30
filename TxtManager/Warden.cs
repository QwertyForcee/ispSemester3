using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace TxtManager
{
    public class Warden
    {
        FileSystemWatcher watcher;
        TxtManager manager;
        ConfigurationManager configuration;
        Settings settings;
        object obj = new object();
        bool enabled = true;
        string targetDir;
        string sourceDir;
        public Warden(string targetDir,string sourceDir, ConfigurationManager configuration)
        {
            this.configuration = configuration;
            settings = configuration.ParseSettings();
            this.targetDir = settings.TargetPath;
            this.sourceDir = settings.SourcePath;
            watcher = new FileSystemWatcher(this.sourceDir);
            manager = new TxtManager("qwertyui",settings);// "qwertyui" - ключ

            watcher.Deleted += Watcher_Deleted;
            watcher.Created += Watcher_Created;
            watcher.Changed += Watcher_Changed;
            watcher.Renamed += Watcher_Renamed;
        }

        public void Start()
        {
            watcher.EnableRaisingEvents = true;
            while (enabled)
            {
                Thread.Sleep(1000);
            }
        }
        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
            enabled = false;
        }
        // переименование файлов
        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            string fileEvent = "переименован в " + e.FullPath;
            string filePath = e.OldFullPath;
            RecordEntry(fileEvent, filePath);
        }
        // изменение файлов
        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "изменен";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }
        // создание файлов
        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath.EndsWith(".txt"))
            {
                string fileEvent = "создан";
                string filePath = e.FullPath;
                manager.Send(e.FullPath);
                RecordEntry(fileEvent, filePath);
            }
            else
            {
                string fileEvent = "не имеет расширение .txt";
                string filePath = e.FullPath;
                RecordEntry(fileEvent, filePath);
            }
        }
        // удаление файлов
        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "удален";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }

        private void RecordEntry(string fileEvent, string filePath)
        {
            lock (obj)
            {
                using (StreamWriter writer = new StreamWriter(settings.LogsPath, true))
                {
                    writer.WriteLine(String.Format("{0} файл {1} был {2}",
                        DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), filePath, fileEvent));
                    writer.Flush();
                }
            }
        }
    }
}
