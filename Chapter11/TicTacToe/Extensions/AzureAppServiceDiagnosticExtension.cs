using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Extensions
{
    public class AzureAppServiceDiagnosticExtension
    {
        public static void AddAzureWebAppDiagnostics(IConfiguration configuration, ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddAzureWebAppDiagnostics();
        }
    }
}
