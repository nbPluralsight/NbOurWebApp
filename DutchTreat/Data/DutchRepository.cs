using DutchTreat.Data.Entities;
using DutchTreat.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        //Dans cette classe nous allons injecter Dutch Context
        // le job de Repository c'est de exposer les appelles a la base de de données que nous voulons 
        // mais en réalité en va l'exposer en tant que Inteface
        private readonly DutchContext _ctx;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext ctx,ILogger<DutchRepository> logger)
        {
            _ctx = ctx;
            this._logger = logger;
        }

        

        public IEnumerable<Product> GetAllProducts()
        {
            _logger.LogInformation("GetAllProducts was called");


            try
            {
                return _ctx.Products
              .OrderBy(p => p.Title)
              .ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed de get All Products {ex}");
                return null;
            }
          
                
        }

        public IEnumerable<Product> GetProductByCategorie(String categorie)
        {
            return _ctx.Products.Where(p => p.Category == categorie).ToList();
                
        }

        public bool SaveAll()
        {
           return  _ctx.SaveChanges() > 0;
        }
    }
}
