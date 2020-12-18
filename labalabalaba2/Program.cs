using System;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using TxtManager;
using System.Data;
using System.Data.SqlClient;
using TxtManager.Parsers;
using System.Configuration;
using DataManager;
using Models;
using DataManager.Configuration;
using XmlGen;
using System.Threading.Tasks;

namespace labalabalaba2
{  
        class Program
        {
            static Warden warden = new Warden(new ConfigurationManager("D:\\VS Maxim\\labalabalaba2\\labalabalaba2\\config.xml", "D:\\VS Maxim\\labalabalaba2\\labalabalaba2\\config.xsd"));
            static DataManager.DataManager dataManager1 = new DataManager.DataManager(new DataManagerConfigurationManager(), @"D:\VS Maxim\labalabalaba2\labalabalaba2\dbconfig.json");
            const string breakString = "break";
            static UserInterface userInterface;


            static void Main(string[] args) 
            {
                   userInterface = new UserInterface(dataManager1);
                   Thread thread = new Thread(new ThreadStart(WardenStart));
                   thread.Start();
                   userInterface.MainMenu();      
            }
            public static void WardenStart()
            {
                 warden.Start();
            }    
        }
}
