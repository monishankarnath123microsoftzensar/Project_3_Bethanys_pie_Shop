using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
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
            RegisterUser register = new RegisterUser();

            return View(register);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Exclude = "IsEmailVerfied,ActivationCode")] RegisterUser register)
        {
            if (ModelState.IsValid)
            {
                var isExist = IsEmailExist(register.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "EMAIL ALREADY EXIST");
                    return View(register);
                }
                else
                {
                    register.ActivationCode = Guid.NewGuid();
                    register.IsEmailVerfied = false;
                    HttpResponseMessage response = GlobalVariables.webApiClient.PostAsJsonAsync("RegisterApi", register).Result;
                    SendVerificationLinkEmail(register.Email, register.ActivationCode.ToString());
                    //_context.RegisterUsers.Add(register);
                    //_context.SaveChanges();
                    return RedirectToAction("Login", "RegisterUser");
                }
                
                //if (register.Id == 0)
                //{
                //    _context.RegisterUsers.Add(register);
                //    _context.SaveChanges();
                //    return RedirectToAction("Login", "RegisterUser");
                //}
                //else
                //{
                //    var registerInDb = _context.RegisterUsers.Single(c => c.Id == register.Id);
                //    registerInDb.FName = register.FName;
                //    registerInDb.LName = register.LName;
                //    registerInDb.Address = register.Address;
                //    registerInDb.ZipCode = register.ZipCode;
                //    registerInDb.City = register.City;
                //    registerInDb.State = register.State;
                //    registerInDb.PhoneNo = register.PhoneNo;
                //    registerInDb.Email = register.Email;
                //    registerInDb.Password = register.Password;
                //    _context.SaveChanges();
                //    return RedirectToAction("Index", "Home");
                //}
            }
            return View();
        }
        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            var v = _context.RegisterUsers.Where(a => a.Email == emailID).FirstOrDefault();
            return v != null;
        }
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            //_context.Configuration.ValidateOnSaveEnabled = false;
            var v = _context.RegisterUsers.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
            if (v != null)
            {
                v.IsEmailVerfied = true;
                _context.SaveChanges();

            }
            return View();
        }
        public ActionResult Login()
        {
            LoginModel objLoginModel = new LoginModel();
            return View(objLoginModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                var credential = _context.RegisterUsers.Where(m => m.Email == login.Email && m.Password == login.Password).FirstOrDefault();
                if (credential == null)
                {
                    ModelState.AddModelError("Error", "Email or Password is Incorrect");
                    return View();
                }
                else
                {
                    Session["Email"] = credential.Email;
                    Session["UserId"] = credential.Id;
                    Session["Name"] = credential.FName;
                    Session["Address"] = credential.Address+" "+credential.City+" "+credential.State+" "+credential.ZipCode;
                    Session["Phone"] = credential.PhoneNo;
                    Session["IsVerify"] = credential.IsEmailVerfied;
                    return RedirectToAction("Index", "Home");
                }

            }
            return View();
        }

        public ActionResult ViewUser(int? id)
        {
            var updateUser1 = _context.RegisterUsers.SingleOrDefault(c => c.Id == id);
            if (updateUser1 == null)
            {
                return HttpNotFound();
            }
            return View(updateUser1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewUser(RegisterUser register)
        {
            if (ModelState.IsValid)
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
                registerInDb.ConfirmPassword = register.ConfirmPassword;
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string EmailId)
        {
            var account = _context.RegisterUsers.Where(a => a.Email == EmailId).FirstOrDefault();
            if (account != null)
            {
                var resetCode = Guid.NewGuid();
                SendVerificationLinkEmail(account.Email, resetCode.ToString(), "ResetPassword");
                account.ActivationCode = resetCode;
                //account.ForgotPasswordCode = resetCode;
                _context.Configuration.ValidateOnSaveEnabled = false;
                _context.SaveChanges();
            }
            else
            {
                return HttpNotFound();
            }



            return View();

        }
        public ActionResult ResetPassword(string id)
        {
            var user = _context.RegisterUsers.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
            if (user != null)
            {
                ResetPasswordModel model = new ResetPasswordModel();
                model.ResetCode = id;
                return View(model);
                // return RedirectToAction("Login", "Account");
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.RegisterUsers.Where(a => a.ActivationCode == new Guid(model.ResetCode)).FirstOrDefault();
                if (user != null)
                {
                    user.Password = model.NewPassword;
                    user.ConfirmPassword = model.ConfirmPassword;
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Login", "RegisterUser");
        }
        [NonAction]
        public void SendVerificationLinkEmail(string emailId, string activationCode, string emailFor = "VerifyAccount")
        {


            var verifyUrl = "/RegisterUser/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var fromEmail = new MailAddress("NeedDoc247@gmail.com", "Bethany's pie shop"); //enterr email
            var toEmail = new MailAddress(emailId);
            var fromEmailPassword = "project123.net"; //Enter  password  of your email
            string subject = "";
            string body = "";


            if (emailFor == "VerifyAccount")
            {

                subject = "Your Account is successfully created";
                body = "Your account is successfully created click on the link to verify\n" + "<a href='" + link + "'>" + link + "</a>";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Your Password";
                body = "Hi" + " \nYour reset password link is here" + "\n <a href=" + link + ">Reset Password link</a>";
            }

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);

        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}