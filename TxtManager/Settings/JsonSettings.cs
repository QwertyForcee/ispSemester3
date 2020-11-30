using System;
using System.Collections.Generic;
using System.Text;

namespace TxtManager
{
        public class JsonSettings
        {
            public string SourcePath { get; set; }
            public string TargetPath { get; set; }
            public string LogsPath { get; set; }
            public string ExeptionsLogPath { get; set; }

            public bool NeedToEncrypt { get; set; }

            public bool NeedToArchive { get; set; }

            public string ArchivePath { get; set; }
            public JsonSettings() { }
            public JsonSettings(string SourcePath, string TargetPath, string LogsPath, string ExepcionsLogPath, bool NeedToEncrypt, bool needToArchive, string archivePath)
            {
                this.SourcePath = SourcePath;
                this.TargetPath = TargetPath;
                this.LogsPath = LogsPath;
                this.ExeptionsLogPath = ExepcionsLogPath;
                this.NeedToEncrypt = NeedToEncrypt;
                this.NeedToArchive = needToArchive;                
                this.ArchivePath = archivePath;
            }
        }
    }
