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
namespace labalabalaba2
{  
        class Program
        {
            static Warden warden = new Warden(new ConfigurationManager("D:\\VS Maxim\\labalabalaba2\\labalabalaba2\\config.xml", "D:\\VS Maxim\\labalabalaba2\\labalabalaba2\\config.xsd"));
            const string breakString = "break";
            static void Main(string[] args) 
            {

            string userTry;
               Thread thread = new Thread(new ThreadStart(WardenStart));
               thread.Start();
            Console.WriteLine("Shop Name:");
            userTry = Console.ReadLine();
            Example(userTry);
            }
            public static void WardenStart()
            {
                 warden.Start();
            }

            //test
            static void Example(string Input)
            {
                 DataManager.DataManager dataManager = new DataManager.DataManager(new DataManagerConfigurationManager(), @"D:\VS Maxim\labalabalaba2\labalabalaba2\dbconfig.json");
                 dataManager.Extract(Input);
                 dataManager.GenerateXml();
                 dataManager.TransferFile("D:\\SourceDir");
            }
        }
}
