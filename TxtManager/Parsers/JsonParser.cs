using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using TxtManager;

namespace TxtManager.Parsers
{
    class JsonParser:IParser
    {
        private string Path { get; }
        private string ConfigPath { get; }
        public JsonParser(string path, string configPath)
        {
            Path = path;
            ConfigPath = configPath;
        }
        public Settings ParseSettings()
        {
            try
            {
                JsonSettings settings;
                string file;
                using (StreamReader reader = new StreamReader(Path))
                {
                    file = reader.ReadToEnd();
                    settings = JsonSerializer.Deserialize<JsonSettings>(file);
                }
                return new Settings(settings.SourcePath, settings.TargetPath, settings.LogsPath, settings.ExeptionsLogPath, settings.NeedToEncrypt, new ArchiveSettings(settings.NeedToArchive, settings.ArchivePath));
            }
            catch(Exception e)
            {
                throw new Exception($"{e.Message} --- Source:{e.Source}");
            }
        }
    }
}
