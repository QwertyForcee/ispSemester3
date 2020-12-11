using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace DataManager.Configuration
{
    public class DataManagerConfigurationManager
    {
        public DataManagerConfigModel LoadConfig(string path)
        {
            DataManagerConfigModel config;
            string file;
            try
            {              
                using (StreamReader reader = new StreamReader(path))
                {
                    file = reader.ReadToEnd();
                    config = JsonSerializer.Deserialize<DataManagerConfigModel>(file);
                    return config;
                }
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message} --- Source:{e.Source}");                
            }
        }
    }
}
