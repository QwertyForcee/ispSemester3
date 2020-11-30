using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using System.Xml.Serialization;

namespace TxtManager
{
    [Serializable]
    [XmlRoot(ElementName = "Settings", Namespace = "")]
    public class XmlSettings
    {
        [XmlElement("SourcePath")]
        public string SourcePath { get; set; } 

        [XmlElement("TargetPath")]
        public string TargetPath { get; set; }

        [XmlElement("LogsPath")]
        public string LogsPath { get; set; }      
        [XmlElement("ExeptionsLogPath")]
        public string ExeptionsLogPath { get; set; }
        [XmlElement("NeedToEncrypt")]
        public bool NeedToEncrypt { get; set; }
        [XmlElement("NeedToArchive")]
        public bool NeedToArchive { get; set; }
        [XmlElement("ArchivePath")]
        public string ArchivePath { get; set; }
        public XmlSettings() { }
        public XmlSettings(string SourcePath, string TargetPath, string LogsPath, string ExepcionsLogPath, bool NeedToEncrypt, bool needToArchive, string archivePath)
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
