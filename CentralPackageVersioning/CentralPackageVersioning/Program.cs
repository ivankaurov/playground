using System;
using Newtonsoft.Json;

namespace CentralPackageVersioning
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Hello World! Newtonsoft is {typeof(JsonSerializer).Assembly.GetName().Version}");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey(true);
        }
    }
}
