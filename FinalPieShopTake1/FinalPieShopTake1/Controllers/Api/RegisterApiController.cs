using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalPieShopTake1.Models;

namespace FinalPieShopTake1.Controllers.Api
{
    public class RegisterApiController : ApiController
    {
        private ApplicationDbContext _context;
        public RegisterApiController()
        {
            _context = new ApplicationDbContext();
        }
        public IHttpActionResult GetCustomers()
        {
            var register = _context.RegisterUsers.ToList();
            //return customer;
            //return _context.Customers.ToList();
            if (register == null)
            {
                return NotFound();
            }
            return Ok(register);
        }
        public IHttpActionResult GetCustomer(int id)
        {
            var register = _context.RegisterUsers.SingleOrDefault(c => c.Id == id);
            if (register == null)
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }

            return Ok(register);
        }
        //Post /api/customerapi
        [HttpPost]
        public IHttpActionResult CreateCustomer(RegisterUser registerUser)
        {
            if (!ModelState.IsValid)
            {
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest("Model data is invalid");
            }

            _context.RegisterUsers.Add(registerUser);
            _context.SaveChanges();
            return Ok(registerUser);
        }
        //Put /api/customerapi/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, RegisterUser register)
        {
            if (!ModelState.IsValid)
            {
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest("Model data is invalid");
            }
            var registerInDb = _context.RegisterUsers.SingleOrDefault(c => c.Id == id);
            if (registerInDb == null)
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }

            registerInDb.FName = register.FName;
            registerInDb.LName = register.LName;
            registerInDb.Address = register.Address;
            registerInDb.ZipCode = register.ZipCode;
            registerInDb.City = register.City;
            registerInDb.State = register.State;
            registerInDb.PhoneNo = register.PhoneNo;
            registerInDb.Email = register.Email;
            registerInDb.Password = register.Password;
            registerInDb.ConfirmPassword = register.Password;
            _context.SaveChanges();
            return Ok();
        }
        //Delete /api/customerapi/1

        public IHttpActionResult DeleteCustomer(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Not a Valid Customer id");
            }
            var registerInDb = _context.RegisterUsers.SingleOrDefault(c => c.Id == id);
            if (registerInDb == null)
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }

            _context.RegisterUsers.Remove(registerInDb);
            _context.SaveChanges();
            return Ok();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
