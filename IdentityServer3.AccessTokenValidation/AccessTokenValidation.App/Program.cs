using System;
using Microsoft.Owin.Hosting;

namespace AccessTokenValidation.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var baseAddress = "http://localhost:9000/";

            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey(true);
            }
        }
    }
}
