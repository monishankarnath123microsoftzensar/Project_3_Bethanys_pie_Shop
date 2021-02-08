using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalPieShopTake1.Models;
using System.Data.Entity;
using FinalPieShopTake1.ViewModel;
using System.Net;
using System.Net.Http;

namespace FinalPieShopTake1.Controllers
{
    public class AdminController : Controller
    {
        //private ApplicationDbContext _context;
        // GET: Admin
        //public AdminController()
        //{
        //    _context = new ApplicationDbContext();
        //}
        public ActionResult Index()
        {
            //Pass: Moadmin@123/Suadmin@123
            //LoginModel objLoginModel = new LoginModel();
            //return View(objLoginModel);
            return RedirectToAction("Login", "Account");
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Index(LoginModel login)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (_context.Admins.Where(m => m.Email == login.Email && m.Password == login.Password).FirstOrDefault() == null)
        //        {
        //            ModelState.AddModelError("Error", "Email or Password is Incorrect");
        //            return View();
        //        }
        //        else
        //        {
        //            var found = login.Email.IndexOf("@");

        //            Session["Email"] = login.Email.Substring(0, found).ToUpper();
        //            return RedirectToAction("Home", "Admin");
        //        }

        //    }
        //    return View();
        //}
        [Authorize]

        public ActionResult Home()
        {
            //var user = _context.RegisterUsers.ToList();
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("RegisterApi").Result;
            var user = response.Content.ReadAsAsync<IEnumerable<RegisterUser>>().Result;
            return View(user);
        }

        public ActionResult ViewCust(int id)
        {
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("RegisterApi?id=" + id.ToString()).Result;
            var singleCust = response.Content.ReadAsAsync<RegisterUser>().Result;
            //var singleCust = _context.RegisterUsers.SingleOrDefault(c => c.Id == id);
            //if (singleCust == null)
            //{
            //    return HttpNotFound();
            //}
            return View(singleCust);
        }
        [Authorize]
        public ActionResult AllPie()
        {
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("PieApi").Result;
            var pie = response.Content.ReadAsAsync<IEnumerable<Pies>>().Result;
            return View(pie);
        }
        public ActionResult Details(int id)
        {
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("PieApi?id=" + id.ToString()).Result;
            var pie = response.Content.ReadAsAsync<Pies>().Result;
            return View(pie);
        }

        [Authorize]
        public ActionResult AllCategory()
        {
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("CategoryApi").Result;
            var pie = response.Content.ReadAsAsync<IEnumerable<PieCategory>>().Result;
            return View(pie);
        }
        [Authorize(Roles = "CanManagePie,CanManageAll")]
        public ActionResult AddPie()
        {
            HttpResponseMessage response1 = GlobalVariables.webApiClient.GetAsync("CategoryDDApi").Result;

            var viewModel = new NewPieViewModel
            {
                PieCategories = response1.Content.ReadAsAsync<IEnumerable<PieCategory>>().Result
            };
            //var addPie = _context.PieCategories.ToList();
            //var viewModel = new NewPieViewModel
            //{
            //    PieCategories = addPie
            //};
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavePie(Pies pies)
        {
            if (pies.Id == 0)
            {
                HttpResponseMessage response = GlobalVariables.webApiClient.PostAsJsonAsync("PieApi", pies).Result;
            }
            else
            {
                //var pieInDb = _context.Pies.Single(c => c.Id == pies.Id);
                //pieInDb.Name = pies.Name;
                //pieInDb.SDescription = pies.SDescription;
                //pieInDb.LDescription = pies.LDescription;
                //pieInDb.Price = pies.Price;
                //pieInDb.IsPieOfTheWeek = pies.IsPieOfTheWeek;
                //pieInDb.InStock = pies.InStock;
                //pieInDb.PieCategoryId = pies.PieCategoryId;
                //pieInDb.Image = pies.Image;
                //pieInDb.ImageThumb = pies.ImageThumb;
                HttpResponseMessage response = GlobalVariables.webApiClient.PutAsJsonAsync($"PieApi/{pies.Id}", pies).Result;

            }
            //_context.SaveChanges();
            return RedirectToAction("AllPie", "Admin");
        }
        [Authorize(Roles = "CanManageCategory,CanManageAll")]
        public ActionResult AddPieCategory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCategory(PieCategory pieCategory)
        {
            if (pieCategory.Id == 0)
            {
                HttpResponseMessage response = GlobalVariables.webApiClient.PostAsJsonAsync("CategoryApi", pieCategory).Result;
                //_context.PieCategories.Add(pieCategory);
            }
            else
            {
                //var catInDb = _context.PieCategories.Single(c => c.Id == pieCategory.Id);
                //catInDb.CName = pieCategory.CName;
                //catInDb.Description = pieCategory.Description;
                HttpResponseMessage response = GlobalVariables.webApiClient.PutAsJsonAsync($"CategoryApi/{pieCategory.Id}", pieCategory).Result;
            }
            //_context.SaveChanges();
            return RedirectToAction("AllCategory", "Admin");
        }
        [Authorize(Roles = "CanManagePie,CanManageAll")]
        public ActionResult EditPie(int id)
        {
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync($"PieApi/{id}").Result;
            var updatePie = response.Content.ReadAsAsync<Pies>().Result;
            HttpResponseMessage response1 = GlobalVariables.webApiClient.GetAsync("CategoryDDApi").Result;
            //var updatePie = _context.Pies.SingleOrDefault(c => c.Id == id);
            //if (updatePie == null)
            //{
            //    return HttpNotFound();
            //}
            var vm = new NewPieViewModel
            {
                Pies = updatePie,
                PieCategories = response1.Content.ReadAsAsync<IEnumerable<PieCategory>>().Result
            };
            return View("AddPie", vm);
        }
        [Authorize(Roles = "CanManageCategory,CanManageAll")]
        public ActionResult EditCategory(int id)
        {
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync($"CategoryApi/{id}").Result;
            var updateCat = response.Content.ReadAsAsync<PieCategory>().Result;
            //var updateCat = _context.PieCategories.Find(id);
            //if (updateCat == null)
            //{
            //    return HttpNotFound();
            //}
            return View("AddPieCategory", updateCat);
        }
        //[Authorize(Roles = "CanManageAll")]
        //public ActionResult DeletePie(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var pieTbl = _context.Pies.Find(id);
        //    _context.Pies.Remove(pieTbl);
        //    _context.SaveChanges();
        //    return RedirectToAction("AllPie", "Admin");
        //}
        [Authorize(Roles = "CanManageAll")]
        public ActionResult DeleteCategory(int? id)
        {
            HttpResponseMessage response = GlobalVariables.webApiClient.DeleteAsync($"CategoryApi/{id}").Result;
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //var categoryTbl = _context.PieCategories.Find(id);
            //_context.PieCategories.Remove(categoryTbl);
            //_context.SaveChanges();
            return RedirectToAction("AllCategory", "Admin");
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Admin");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    _context.Dispose();
        //}

    }
}