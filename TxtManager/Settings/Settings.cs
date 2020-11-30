using System;
using System.Collections.Generic;
using System.Text;

namespace TxtManager
{
    public class Settings
    {
        public Settings()
        {

        }
        public Settings(string SourcePath, string TargetPath, string LogsPath, string ExepcionsLogPath,bool NeedToEncrypt, ArchiveSettings archiveSettings)
        {
            this.SourcePath = SourcePath;
            this.TargetPath = TargetPath;   
            this.LogsPath = LogsPath;
            this.ExepcionsLogPath = ExepcionsLogPath;
            this.NeedToEncrypt = NeedToEncrypt;
            this.archiveSettings = archiveSettings;
        }
        public string SourcePath { get; set; }
        public string TargetPath { get; set; }
        public string LogsPath { get; set; }
        public string ExepcionsLogPath { get; set; }
        public bool NeedToEncrypt { get; set; }
        public ArchiveSettings archiveSettings { get; set; }
    }
}
