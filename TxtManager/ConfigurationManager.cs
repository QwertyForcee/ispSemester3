using System;
using System.Collections.Generic;
using System.Text;
using TxtManager.Parsers;

namespace TxtManager
{
    public class ConfigurationManager
    {
        IParser parser;
        public ConfigurationManager(string path, string config)
        {
            if (path.EndsWith(".xml"))
            {
                parser = new XmlParser(path, config);   
            }
            else if (path.EndsWith(".json"))
            {
                parser = new JsonParser(path, config);
            }       
            else
            {
                throw new ArgumentNullException();
            }
        }
        public Settings ParseSettings() => parser.ParseSettings();
    }   
}
