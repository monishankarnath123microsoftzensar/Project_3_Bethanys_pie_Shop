using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalPieShopTake1.Models;

namespace FinalPieShopTake1.Controllers.Api
{
    public class CategoryApiController : ApiController
    {
        private ApplicationDbContext _context;
        public CategoryApiController()
        {
            _context = new ApplicationDbContext();
        }
        public IHttpActionResult GetPieCategory()
        {
            var piecat = _context.PieCategories.ToList();
            //return customer;
            //return _context.Customers.ToList();
            if (piecat == null)
            {
                return NotFound();
            }
            return Ok(piecat);
        }
        public IHttpActionResult GetPieCategory(int id)
        {
            var piecat = _context.PieCategories.SingleOrDefault(c => c.Id == id);
            if (piecat == null)
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }

            return Ok(piecat);
        }
        //Post /api/customerapi
        [HttpPost]
        public IHttpActionResult CreatePieCategory(PieCategory pieCategory)
        {
            if (!ModelState.IsValid)
            {
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest("Model data is invalid");
            }

            _context.PieCategories.Add(pieCategory);
            _context.SaveChanges();
            return Ok(pieCategory);
        }
        //Put /api/customerapi/1
        [HttpPut]
        public IHttpActionResult UpdatePieCategory(int id, PieCategory pieCategory)
        {
            if (!ModelState.IsValid)
            {
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest("Model data is invalid");
            }
            var piecatInDb = _context.PieCategories.SingleOrDefault(c => c.Id == id);
            if (piecatInDb == null)
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }

            piecatInDb.CName = pieCategory.CName;
            piecatInDb.Description = pieCategory.Description;
            _context.SaveChanges();
            return Ok();
        }
        //Delete /api/customerapi/1

        public IHttpActionResult DeletePieCategory(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Not a Valid Customer id");
            }
            var piecatInDb = _context.PieCategories.SingleOrDefault(c => c.Id == id);
            if (piecatInDb == null)
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }

            _context.PieCategories.Remove(piecatInDb);
            _context.SaveChanges();
            return Ok();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
