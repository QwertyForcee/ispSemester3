using System;
using TxtManager;

namespace labalabalaba2
{
    class Program
    {
        Warden warden=new Warden("D:\\SourceDir", "D:\\TargetDir");
        const string breakString = "break";
        string userTry = "";
        void Main(string[] args)
        {
          
            warden.Start();
            
            while (userTry!=breakString)
            {
                Console.Clear();
                Console.WriteLine($"Enter '{breakString}' to quit the session");
                userTry = Console.ReadLine();

            }
        }
    }
}
