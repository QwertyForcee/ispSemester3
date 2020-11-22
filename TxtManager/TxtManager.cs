using System;
using System.IO;
namespace TxtManager
{
    public class TxtManager
    {
        public void Send(string TargetDir, string FullPath)
        {
            string targetFolder = $"{TargetDir}\\{DateTime.Now.Year}\\{DateTime.Now.Month}\\{DateTime.Now.Day}";
            Directory.CreateDirectory(targetFolder);
            string fileNewName = $"{targetFolder}\\Sales_{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Hour}_{DateTime.Now.Minute}.txt";
             
            File.Move(FullPath, fileNewName);

        }
    }
}
