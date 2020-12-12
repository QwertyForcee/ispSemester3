using Models;
using System;
using System.IO;
using System.Xml.Serialization;

namespace XmlGen
{
    public static class XmlGenerator
    {
        public static string ConvertToXml(string path, ToFileModel toFile)
        {
                XmlSerializer serializer = new XmlSerializer(typeof(ToFileModel));
                using (Stream sr = new FileStream(path, FileMode.Create))
                {
                    serializer.Serialize(sr, toFile);
                }
                return path;
        }       

        
    }
}
