using System;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using TxtManager;
using TxtManager.Parsers;
namespace labalabalaba2
{
    class Program
    {
        static Warden warden = new Warden("D:\\TargetDir", "D:\\SourceDir", new ConfigurationManager("D:\\VS Maxim\\labalabalaba2\\labalabalaba2\\config.json", "D:\\VS Maxim\\labalabalaba2\\labalabalaba2\\config.xsd"));
        const string breakString = "break";
        static void Main(string[] args)
        {
            string userTry = "";
            Thread thread = new Thread(new ThreadStart(WardenStart));  
            thread.Start();          
            while (userTry!=breakString)
            {
                Console.Clear();
                Console.WriteLine($"Enter '{breakString}' to quit the session");
                userTry = Console.ReadLine();
                if (userTry == breakString)
                {
                    warden.Stop();
                    break;  
                }
            }
        }
        public static void WardenStart()
        {
            warden.Start();
        }
    }
}
