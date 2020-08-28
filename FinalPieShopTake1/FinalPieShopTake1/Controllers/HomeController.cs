using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalPieShopTake1.Models;
using System.Data.Entity;
using FinalPieShopTake1.ViewModel;

namespace FinalPieShopTake1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var pie = _context.Pies.Include(c => c.PieCategory).ToList();
            //var pie = _context.PieCategories.ToList();
            //var viewModel = new NewPieViewModel
            //{
            //    PieCategories = pie
            //};
            return View(pie);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}