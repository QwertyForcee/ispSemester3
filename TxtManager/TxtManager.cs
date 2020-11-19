using System;
using System.IO;
namespace TxtManager
{
    public class TxtManager
    {
        public void Send(string TargetDir, string FullPath)
        {        
            string fileNewName = $"{TargetDir}\\{DateTime.Now.Year}\\{DateTime.Now.Month}\\{DateTime.Now.Day}\\Sales_{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}.txt";
            File.Move(FullPath, fileNewName);

        }
    }
}
