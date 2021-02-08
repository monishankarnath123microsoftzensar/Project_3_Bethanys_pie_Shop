using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalPieShopTake1.Models;
using System.Net.Mail;
using System.Net.Http;

namespace FinalPieShopTake1.Controllers
{
    public class AddToCartController : Controller
    {
        private ApplicationDbContext _context;
        // GET: AddToCart
        public AddToCartController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult AddtoCart(int? id)
        {
            var pie = _context.Pies.Where(x => x.Id == id).SingleOrDefault();
            return View(pie);
        }
        List<Cart> li = new List<Cart>();
        [HttpPost]
        public ActionResult AddtoCart(Pies pies, string qty, int id)
        {
            var pie = _context.Pies.Where(x => x.Id == id).SingleOrDefault();
            Cart c = new Cart();
            c.productid = pie.Id;
            c.price = (float)pie.Price;
            c.qty = Convert.ToInt32(qty);
            c.bill = c.price * c.qty;
            c.productname = pie.Name;
            if (TempData["Cart"] == null)
            {
                li.Add(c);
                TempData["Cartcounter"] = li.Count;
                TempData["Cart"] = li;
            }
            else
            {
                List<Cart> li2 = TempData["Cart"] as List<Cart>;
                int flag = 0;
                foreach (var item in li2)
                {
                    if (item.productid == c.productid)
                    {
                        item.qty += c.qty;
                        item.bill += c.bill;
                        flag = 1;
                    }
                }
                if (flag == 0)
                {
                    li2.Add(c);
                }
                TempData["Cartcounter"] = li2.Count + li.Count;
                TempData["Cart"] = li2;
            }
            TempData.Keep();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Remove(int? id)
        {
            List<Cart> li2 = TempData["Cart"] as List<Cart>;
            Cart c = li2.Where(x => x.productid == id).SingleOrDefault();
            li2.Remove(c);
            float h = 0;
            float j = 0;
            foreach (var item in li2)
            {
                h += item.bill;
                j++;
            }
            TempData["total"] = h;
            TempData["Cartcounter"] = j;
            return RedirectToAction("CheckOut");

        }
        public ActionResult CheckOut()
        {
            TempData.Keep();
            return View();
        }
        //[HttpPost]
        //public ActionResult CheckOut(tbl_order order)
        //{
        //    List<Cart> li = TempData["Cart"] as List<Cart>;
        //    tbl_invoice iv = new tbl_invoice();
        //    iv.in_fk_user = Convert.ToInt32(Session["u_id"].ToString());
        //    iv.in_date = System.DateTime.Now;
        //    iv.in_totalbill = (float)TempData["total"];
        //    db.tbl_invoice.Add(iv);
        //    db.SaveChanges();

        //    foreach (var item in li)
        //    {
        //        tbl_order od = new tbl_order();
        //        od.O_fk_pro = item.productid;
        //        od.o_fk_invoice = iv.in_id;
        //        od.o_date = System.DateTime.Now;
        //        od.o_qty = item.qty;
        //        od.o_unitprice = (int)item.price;
        //        od.o_bill = item.bill;
        //        db.tbl_order.Add(od);
        //        db.SaveChanges();
        //    }
        //    TempData.Remove("total");
        //    TempData.Remove("Cart");
        //    TempData["msg"] = "Transaction Completed successfully........";
        //    TempData.Keep();
        //    return RedirectToAction("Index");
        //}
        public ActionResult PlaceOdr()
        {
            if (Session["Name"] == null)
            {
                return RedirectToAction("Login", "RegisterUser");
            }
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("RegisterApi?id=" + Session["UserId"].ToString()).Result;
            var singleCust = response.Content.ReadAsAsync<RegisterUser>().Result;

            return View(singleCust);
        }
        public ActionResult EditAdd(int id)
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
        public ActionResult EditAdd(RegisterUser register)
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
                return RedirectToAction("PlaceOdr", "AddToCart");
            }

            return View();
        }
        public ActionResult SendMail()
        {
            if(Session["IsVerify"].ToString() == "True")
            {
                //Session["Email"] = "monishankarnath@gmail.com";
                //Session["Name"] = "Moni";
                //string name = Session["Name"].ToString();
                string toAddress = Session["Email"].ToString();
                //string fromAddress = "NeedDoc247@gmail.com";
                var fromEmail = new MailAddress("NeedDoc247@gmail.com", "Bethany's pie shop");
                var toEmail = new MailAddress(toAddress);
                char[] letters = "abcdefghijklmnopqrstuvwxyz0123456789@#0123456789?".ToCharArray();
                Random r = new Random();
                string randomString = "";
                for (int i = 0; i < 8; i++)
                {
                    randomString += letters[r.Next(0, 20)].ToString();
                }


                MailMessage mm = new MailMessage(fromEmail, toEmail);
                mm.Subject = "Your Order Have Been Placed!!!!";


                mm.Body = "Hello, Sir/Madam " + Session["Name"] + "\nYour order has been placed in BethnyPieShop on : " + DateTime.Now.ToShortDateString() + " at " + DateTime.Now.ToShortTimeString() + "\nYour Order Reff No is: " + randomString.ToString() + "\nYour EmailId is: " + Session["Email"] + "\nYour Phone No. is: " + Session["Phone"] + "\nPlease kindly pay this amount: Rs" + TempData["total"] + " when delivered to the below address" + "\nYour Address: " + Session["Address"];

                SmtpClient sc = new SmtpClient("smtp.gmail.com", 587);
                sc.Credentials = new System.Net.NetworkCredential()
                {
                    //project123.net
                    //123abc456def
                    UserName = fromEmail.Address, //"NeedDoc247@gmail.com",
                    Password = "project123.net"
                };

                sc.EnableSsl = true;
                sc.Send(mm);

                TempData["Cart"] = null;
                return View();
            }
            else
            {
                return View("NotVerify");
            }
            

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}