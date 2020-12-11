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
/*
TODO:
Написать 


*/
namespace labalabalaba2
{  
        class Program
        {
            //static Warden warden = new Warden(new ConfigurationManager("D:\\VS Maxim\\labalabalaba2\\labalabalaba2\\config.xml", "D:\\VS Maxim\\labalabalaba2\\labalabalaba2\\config.xsd"));
            const string breakString = "break";
            static void Main(string[] args)
            {
            /*
               string userTry = "";
               Thread thread = new Thread(new ThreadStart(WardenStart));
               thread.Start();
               while (userTry != breakString)
               {
                   Console.Clear();
                   Console.WriteLine($"Enter '{breakString}' to quit the session");
                   userTry = Console.ReadLine();
                   if (userTry == breakString)
                   {
                       warden.Stop();
                       break;
                   }
               }*/
            AAA();
            }
            public static void WardenStart()
            {
                //warden.Start();
            }

            //test
            static void AAA()
            {
                 DataManager.DataManager dataManager = new DataManager.DataManager(new DataManagerConfigurationManager(), @"D:\VS Maxim\labalabalaba2\labalabalaba2\dbconfig.json");
            //dataManager.GetSomeShit(@"Data Source=BY-NODE\SQLS;Initial Catalog=AdventureWorks2019;Integrated Security=True", "Production.SendReviews");
                 //Console.WriteLine( dataManager.config.ConnectionString);
                  //Console.WriteLine(dataManager.config.StoreProcedure);

                 dataManager.Extract("Next-Door Bike Store");
            }
        }
}
