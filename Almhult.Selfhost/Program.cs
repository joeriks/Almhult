using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almhult.Selfhost
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:" + ((args.Length>0)?args[0]:"8080");

            using (WebApp.Start<Startup>(url))
            {
                Process.Start(url + "/index.html");
                Console.WriteLine("Server running at http://localhost:8080/ open url and enter javascript console to try");
                Console.ReadLine();
            }

        }
    }
}
