using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlTech.Web.Data;

namespace ControlTech.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
           
            var host = CreateHostBuilder(args).Build();

            CriarDb_Semente(host);

            host.Run();
        }

        private static void CriarDb_Semente(IHost host)
        {
            // cria o escopo para sua injeção de dependencia criar o contexto e seu Banco de dados
            using (var escopo = host.Services.CreateScope())
            {
                var servicos = escopo.ServiceProvider;

                try
                {
                    var contexto = servicos.GetRequiredService<ApplicationDbContext>();
                    // como a classe de inicialização é estatica, não precisamos Instancia-la
                    DbInicializacao.Initialize(contexto);
                }
                catch (Exception ex)
                {
                    var log = servicos.GetRequiredService<ILogger>();
                    log.LogError(ex, " Um erro foi encontrado na geração da semente do Database");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
