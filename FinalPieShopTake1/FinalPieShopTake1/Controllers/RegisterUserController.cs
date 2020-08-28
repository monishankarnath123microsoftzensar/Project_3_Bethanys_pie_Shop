using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalPieShopTake1.Models;

namespace FinalPieShopTake1.Controllers
{
    public class RegisterUserController : Controller
    {
        ApplicationDbContext _context;
        // GET: RegisterUser
        public RegisterUserController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Save(RegisterUser register)
        {
            if (register.Id == 0)
            {
                _context.RegisterUsers.Add(register);
            }
            else
            {
                var registerInDb = _context.RegisterUsers.Single(c => c.Id == register.Id);
                registerInDb.FName = register.FName;
                registerInDb.LName = register.LName;
                registerInDb.Address = register.Address;
                registerInDb.ZipCode = register.ZipCode;
                registerInDb.City = register.City;
                registerInDb.State = register.State;
                registerInDb.PhoneNo = register.PhoneNo;
                registerInDb.Email = register.Email;
                registerInDb.Password = register.Password;


            }
            _context.SaveChanges();
            return RedirectToAction("Login", "RegisterUser");
        }
        public ActionResult ViewUser(int id)
        {
            var updateUser1 = _context.RegisterUsers.SingleOrDefault(c => c.Id == id);
            if (updateUser1 == null)
            {
                return HttpNotFound();
            }
            return View(updateUser1);
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}