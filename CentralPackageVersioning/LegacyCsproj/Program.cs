using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LegacyCsproj
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
