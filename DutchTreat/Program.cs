using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DutchTreat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            // l'execution de seeder qui se trouve dans la classe dutchSeeder ( cette classe permet de  manager les données )
            RunSeeding(host);
            host.Run();
        }

        private static void RunSeeding(IWebHost host)
        {
            // tout ce travail doit se faire au niveau d'un Scope : un scope c'est une un contenaire 
            // tant que le travail n'est pas fini le Scope reste ouvert 

            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                // on aura besoin de recuperer la configuration de dutchseeder et dire a mvc d'executer le sseder dans program.cs
                // mais il faudra ajouter l'indepence injection entre program.cs et StartUp

                //***************************************************
                //var seed = host.Services.GetService<DutchSeeder>();
                //***************************************************

                // la raison pour laquelle host.Services est changer parce que nous voulons recuperer
                // le service de Scope 
                var seed =  scope.ServiceProvider.GetService<DutchSeeder>();
                // il appel la methode qui permet d'executer mes action au niveau de dutchSeeder exemple 
                //ajouter un produit ou un Ordre
                seed.Seed();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(SetupConfiguration)
                .UseStartup<Startup>();

        private static void SetupConfiguration(WebHostBuilderContext arg1, IConfigurationBuilder builder)
        {
           
            builder.Sources.Clear();
            builder.AddJsonFile("Config.json", false, true)
                .AddEnvironmentVariables();
            
        }
    }
}
