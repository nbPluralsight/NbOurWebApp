using DutchTreat.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using DutchTreat.Service;
using DutchTreat.Data.Entities;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DutchTreat.Data;

namespace DutchTreat.Controllers
{
    public class AppController : Controller 
    {
        private readonly IMailService _mailservice;
        //private readonly DutchContext _context;
        private readonly IDutchRepository _repository;
        //public AppController(IMailService mailService, DutchContext context)
        //{
        //    this._mailservice = mailService;
        //    _context = context;
        //}
        public AppController(IMailService mailService, IDutchRepository repository)
        {
            this._mailservice = mailService;
            _repository = repository;
        }

        public IActionResult Index()
        {
            //throw new InvalidOperationException("Something bad");

            // ici c'est pour montrer que le chargement de Scope a fonctionné 
           // var result = _context.Products.ToList();

             return View();
        }
        [HttpGet("Contact")]
        public IActionResult Contact()
        {
          //throw new InvalidOperationException("Something bad");
            ViewBag.Title = "Contact Us";
            return View();
        }
        [HttpPost("Contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if(ModelState.IsValid)
            {
                // Send a email
                _mailservice.SendMessage("nbvoice@gmail.com", model.Subject,model.Message);
                ViewBag.UserMessage = "Send email";
                ModelState.Clear();
            }
            else
            {
                //Error
            }           
            return View();
        }
        [HttpGet("About")]
        public IActionResult About()
        {
            ViewBag.Title = "About Us";
            return View();
        }
        public IActionResult Shop()
        {

            // Cette requete sera remplacé par le repository 
            //var result = _context.Products
            //    .OrderBy(p => p.Category)
            //    .Select(p => p)
            //    .ToList();

            //var result2 = from p in _context.Products
            //              orderby p.Category
            //              select p;

            var result = _repository.GetAllProducts();
           
            return View(result.ToList());
        }

    }
}
