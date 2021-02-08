using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalPieShopTake1.Models;
using System.Data.Entity;
using FinalPieShopTake1.ViewModel;
using System.Net.Http;

namespace FinalPieShopTake1.Controllers
{
    public class HomeController : Controller
    {
        //private ApplicationDbContext _context;
        //public HomeController()
        //{
        //    _context = new ApplicationDbContext();
        //}
        public ActionResult Index()
        {
            if (TempData["Cart"] != null)
            {
                float x = 0;
                float y=0;
                List<Cart> li2 = TempData["Cart"] as List<Cart>;
                foreach (var item in li2)
                {
                    x += item.bill;
                    y++;
                }
                TempData["total"] = x;
                TempData["Cartcounter"] = y;
            }
            TempData.Keep();
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("PieApi").Result;
            var pie = response.Content.ReadAsAsync<IEnumerable<Pies>>().Result;
            //var pie = _context.Pies.Include(c => c.PieCategory).ToList();

            return View(pie);
        }
        public ActionResult Details(int id)
        {
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("PieApi?id=" + id.ToString()).Result;
            var singlePie = response.Content.ReadAsAsync<Pies>().Result;

            //var singlePie = _context.Pies.Include(c => c.PieCategory).SingleOrDefault(c => c.Id == id);
            //if (singlePie == null)
            //{
            //    return HttpNotFound();
            //}
            return View(singlePie);
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
        public ActionResult ViewByCategory(int? id, string name)
        {
            IEnumerable<Pies> byCategory;
            //_context.Admins.Where(m => m.Email == login.Email && m.Password == login.Password).FirstOrDefault();
            //var byCategory = _context.Pies.Include(c => c.PieCategory).Where(m => m.PieCategoryId == id).ToList();
            var response = GlobalVariables.webApiClient.GetAsync($"PieApi/{id}?name={name}").Result;
            byCategory = response.Content.ReadAsAsync<IEnumerable<Pies>>().Result;

            return View(byCategory);
        }
        public ActionResult ViewAllPie()
        {
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("PieApi").Result;
            var pie = response.Content.ReadAsAsync<IEnumerable<Pies>>().Result;
            //var pie = _context.Pies.Include(c => c.PieCategory).ToList();
            return View(pie);
        }
        //public ActionResult Review(int? id)
        //{
        //    var reviewPie = _context.Pies.SingleOrDefault(c => c.Id == id);
        //    var vm = new PieReviewViewModel
        //    {
        //        Pies = reviewPie

        //    };
        //    return View(vm);
        //}
        //protected override void Dispose(bool disposing)
        //{
        //    _context.Dispose();
        //}
    }
}