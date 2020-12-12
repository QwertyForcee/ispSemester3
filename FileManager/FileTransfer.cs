using System;
using System.IO;

namespace FileManager
{
    public static  class FileTransfer
    {
        public static void Transfer(string source, string destination)
        {
            if (!File.Exists(source)) return;
            File.Move(source, destination);
            File.Delete(source);
        }
    }
}
