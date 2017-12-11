using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TicTacToe.Extensions;

namespace TicTacToe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .CaptureStartupErrors(true)
                .UseStartup("TicTacToe")
                //.UseStartup<Startup>()
                .PreferHostingUrls(true)
                //.UseUrls("http://localhost:5000")
                //.UseApplicationInsights()
                //.ConfigureLogging((hostingcontext, logging) =>
                //{
                //    logging.AddLoggingConfiguration(hostingcontext.Configuration);
                //})
                .Build();
    }
}
