using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace TxtManager
{
    public class ArchiveSettings
    {   
         public ArchiveSettings(bool needToArchive,string archivePath)
        {
            this.NeedToArchive = needToArchive;
            this.ArchivePath = archivePath;
        }
         public string ArchivePath { get; set; }
         public bool NeedToArchive {get; set; }
        
    }
}
