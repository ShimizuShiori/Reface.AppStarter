using Reface.AppStarter;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            AppSetup.Start<ConsoleApp1AppModule>();
            Console.ReadLine();
        }
    }
}
