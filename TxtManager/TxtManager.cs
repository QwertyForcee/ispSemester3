using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TxtManager
{
    public class TxtManager
    {
        public TxtManager(string sKey,Settings settings)
        {          
            this.sKey = sKey;
            this.settings = settings;
        }
        private Settings settings;
        private readonly string sKey;
        //основной метод
        public void Send(string FullPath, string extension) 
        {
            string currentString = FullPath;    
            if (settings.NeedToEncrypt)
            {
                string encFilePath = currentString.Replace(extension, ".des");
                EncryptFile(currentString, encFilePath, sKey);
                currentString = encFilePath;
            }
            if (settings.archiveSettings.NeedToArchive)
            {
                string zipFileName = Path.ChangeExtension(currentString, ".zip");
                Compress(currentString, zipFileName);
                string archiveFolder = $"{settings.archiveSettings.ArchivePath}";
                if (!Directory.Exists(archiveFolder))
                {
                    Directory.CreateDirectory(archiveFolder);
                }
                string archiveZipFile = $"{archiveFolder}\\Sales_{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Hour}_{DateTime.Now.Minute}.zip";
                File.Move(zipFileName, archiveZipFile);
                File.Delete(currentString);
                if (settings.NeedToEncrypt) { 
                    currentString = Path.ChangeExtension(archiveZipFile, ".des");
                }
                else currentString = Path.ChangeExtension(archiveZipFile, extension);
                Decompress(archiveZipFile, currentString);
            }
            string targetFolder = $"{settings.TargetPath}\\{DateTime.Now.Year}\\{DateTime.Now.Month}\\{DateTime.Now.Day}";
            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }
            if (settings.NeedToEncrypt)
            {
                DencryptFile(currentString, Path.ChangeExtension(currentString, extension), sKey);
                File.Delete(currentString);
                currentString = Path.ChangeExtension(currentString, extension);
            }
            string finalFileName = $"{targetFolder}\\{Path.GetFileName(currentString)}";
            File.Move(currentString, finalFileName);
        }
        private void Compress(string sourceFile,string targetFile)
        {
            try
            {
                using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
                {
                    using (FileStream targetStream = File.Create(targetFile))
                    {
                        using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                        {
                            sourceStream.CopyTo(compressionStream);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                using (StreamWriter writer = new StreamWriter(settings.ExepcionsLogPath, true))
                {
                    writer.WriteLine(String.Format("{0} Message:{1} Source:{2}",
                        DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), e.Message, e.Source));
                    writer.Flush();
                }
            }

        }
        private void Decompress(string compressedFile, string targetFile)
        {
            try
            {
                using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
                {
                    using (FileStream targetStream = File.Create(targetFile))
                    {
                        using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(targetStream);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                using (StreamWriter writer = new StreamWriter(settings.ExepcionsLogPath, true))
                {
                    writer.WriteLine(String.Format("{0} Message:{1} Source:{2}",
                        DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), e.Message, e.Source));
                    writer.Flush();
                }
            }
        }
        private void EncryptFile(string source, string target,string sKey)
        {
            if (!target.EndsWith(".des"))
            {
                target = target.Remove(target.LastIndexOf('.')) + ".des";
            }
            FileStream sourceFile = new FileStream(source, FileMode.Open, FileAccess.Read);
            FileStream encryptedFile = new FileStream(target, FileMode.Create, FileAccess.Write);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            try
            {
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                ICryptoTransform desencrypt = DES.CreateEncryptor();
                CryptoStream cryptoStream = new CryptoStream(encryptedFile, desencrypt, CryptoStreamMode.Write);
                byte[] bytearrayinput = new byte[sourceFile.Length - 0];
                sourceFile.Read(bytearrayinput, 0, bytearrayinput.Length);
                cryptoStream.Write(bytearrayinput, 0, bytearrayinput.Length);
                cryptoStream.Close();
            }
            catch(Exception e)
            {
                using (StreamWriter writer = new StreamWriter(settings.ExepcionsLogPath, true))
                {
                    writer.WriteLine(String.Format("{0} Message:{1} Source:{2}",
                        DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), e.Message, e.Source));
                    writer.Flush();
                }
            }
            sourceFile.Close();
            encryptedFile.Close();
        }
        private void DencryptFile(string source, string target, string sKey)
        {
            FileStream sourceFile = new FileStream(source, FileMode.Open, FileAccess.Read);
            FileStream encryptedFile = new FileStream(target, FileMode.Create, FileAccess.Write);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            try
            {
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                ICryptoTransform desencrypt = DES.CreateDecryptor();
                CryptoStream cryptoStream = new CryptoStream(encryptedFile, desencrypt, CryptoStreamMode.Write);
                byte[] bytearrayinput = new byte[sourceFile.Length - 0];    
                sourceFile.Read(bytearrayinput, 0, bytearrayinput.Length);
                cryptoStream.Write(bytearrayinput, 0, bytearrayinput.Length);
                cryptoStream.Close();
            }
            catch(Exception e)
            {
                using (StreamWriter writer = new StreamWriter(settings.ExepcionsLogPath, true))
                {
                    writer.WriteLine(String.Format("{0} Message:{1} Source:{2}",
                        DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), e.Message, e.Source));
                    writer.Flush();
                }              
            }
            sourceFile.Close();
            encryptedFile.Close();
        }

    }
}
