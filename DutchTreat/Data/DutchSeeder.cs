using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace DutchTreat.Data.Entities
{
    
    public class DutchSeeder
    {
        //seeder permet de globalier l'ensemble des données a recuperer dans dutchcontext de la methode OnCreate()
        // ce qui nous permet de recuperer l'ensemble des produit et les réutiliser  
        private readonly DutchContext _ctx;
        private readonly IHostingEnvironment _hosting;
        //mentionner a DutchSeeder que nous voulions travailler avec les données de DutchContext
        public DutchSeeder(DutchContext ctx, IHostingEnvironment hosting)
        {
            _ctx = ctx;
            _hosting = hosting;
        }
        //premiere des choses à faire c'est de s'assurer si la base de données existe 
        public void Seed()
        {
            _ctx.Database.EnsureCreated();
            if(!_ctx.Products.Any())
            {
                // we need to create a simple data
                // comme nous utilisons un fichier Json pour obtenir des exemples de produit
                // nous allons recuperer un fichier a partir de la source de données pour le recuperer 
                // il fautdra mentionner le path ainsi le path lors de l'execution 
                var filenPath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filenPath);
                // il faut deserializer le fichier Json il faut qu'on soit claire que on utilisent 
                // Jason  parce que les produit vient d'un fichier Json 

                var product = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);

                var order = _ctx.Orders.Where(ord => ord.Id == 1).FirstOrDefault();

                if(order !=null)
                {
                    order.Items = new List<OrderItem>()
                   {
                     new OrderItem()
                     {
                        Product = product.FirstOrDefault(),
                        Quantity =5,
                        UnitPrice = product.First().Price
                     }
                   };
                }

                _ctx.AddRange(product);
                _ctx.SaveChanges();
            }
        }
    }
}
