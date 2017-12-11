using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.ViewEngines;

namespace TicTacToe.Helpers
{
    public class EmailViewRenderHelper
    {
        IHostingEnvironment _hostingEnvironment;
        IConfiguration _configurationRoot;
        IHttpContextAccessor _httpContextAccessor;

        public async Task<string> RenderTemplate<T>(string template, IHostingEnvironment hostingEnvironment, IConfiguration configurationRoot, IHttpContextAccessor httpContextAccessor, T model) where T : class
        {
            _hostingEnvironment = hostingEnvironment;
            _configurationRoot = configurationRoot;
            _httpContextAccessor = httpContextAccessor;
            var renderer = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<EmailViewEngine>();
            return await renderer.RenderEmailToString<T>(template, model);
        }
    }
}
