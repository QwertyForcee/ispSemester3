using System;
using System.Threading;
using TxtManager;
namespace labalabalaba2
{
    class Program
    {
        static Warden warden=new Warden("D:\\TargetDir", "D:\\SourceDir");
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
