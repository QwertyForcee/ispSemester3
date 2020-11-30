using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace TxtManager.Parsers
{
    public class XmlParser:IParser
    {
        public XmlParser()
        {

        }
        private string Path { get; }
        private string ConfigPath { get; }
        public XmlParser(string path, string config)
        {
            Path = path;
            ConfigPath = config;
        }

        public Settings ParseSettings()
        {
            try
            {
                XmlSchemaSet schema = new XmlSchemaSet();
                schema.Add("", ConfigPath);
                XmlDocument document = new XmlDocument();
                document.Load(Path);
                //document.LoadXml(Path);
                document.Schemas = schema;
                ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);
                document.Validate(eventHandler);              
                var settings = DeserializeObj(Path);
                return new Settings(settings.SourcePath, settings.TargetPath, settings.LogsPath, settings.ExeptionsLogPath , settings.NeedToEncrypt, new ArchiveSettings(settings.NeedToArchive, settings.ArchivePath));
            }
            catch(Exception e)
            {              
                throw new Exception($"{e.Message} {e.Source}");
            }
        }
        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            XmlSeverityType type = XmlSeverityType.Warning;
            if (!Enum.TryParse<XmlSeverityType>("Error", out type))
            {
                if (type == XmlSeverityType.Error)
                {
                    throw new Exception(e.Message);
                }
            }
        }
        public XmlSettings DeserializeObj(string xPath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(XmlSettings));
                XmlSettings settings;
                using (Stream sr = new FileStream(xPath, FileMode.Open))
                {
                    settings = (XmlSettings)serializer.Deserialize(sr);
                }
                return settings;
            }
            catch(Exception e)
            {
                WriteExeptionLog("XML PARSER... " + e.Message);
                throw new Exception();
            }
        }
        private void WriteExeptionLog(string message)
        {
             System.IO.StreamWriter streamWriter = new System.IO.StreamWriter("D:\\exeptionslog.txt", true);
            streamWriter.WriteLine($"{DateTime.Now:dd/MM/yyyy hh:mm:ss} " + message);
            streamWriter.Flush();
            streamWriter.Close();
        }
    }
}
