using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LD_EC_PiaBackEnd.Services
{
    public class WriteToFile : IHostedService
    {
        private readonly IWebHostEnvironment env;
        private readonly string file = "log.txt";

        public WriteToFile(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Write("Ejecución iniciada: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Write("Ejecución terminada: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            return Task.CompletedTask;
        }

        public void Write(string content) 
        {
            var route = $@"{env.ContentRootPath}\wwwroot\file";
            using (StreamWriter writer = new StreamWriter(route, append: true))
            {
                writer.WriteLine(content);
            };
        }
    }
}
