using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalPieShopTake1.Models;
using System.Data.Entity;

namespace FinalPieShopTake1.Controllers.Api
{
    public class PieApiController : ApiController
    {
        private ApplicationDbContext _context;
        public PieApiController()
        {
            _context = new ApplicationDbContext();
        }
        public IHttpActionResult GetPie()
        {
            var pie = _context.Pies.Include(m => m.PieCategory).ToList();
            //return customer;
            //return _context.Customers.ToList();
            if (pie == null)
            {
                return NotFound();
            }
            return Ok(pie);
        }
        public IHttpActionResult GetPie(int id)
        {
            var pie = _context.Pies.Include(m => m.PieCategory).SingleOrDefault(c => c.Id == id);
            if (pie == null)
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }

            return Ok(pie);
        }
        public IHttpActionResult GetPie(int id, string name)
        {
            //var pie = _context.Pies.Include(m => m.PieCategory).SingleOrDefault(c => c.Id == id);
            var byCategory = _context.Pies.Include(c => c.PieCategory).Where(m => m.PieCategoryId == id).ToList();
            if (byCategory == null)
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }

            return Ok(byCategory);
        }
        //Post /api/customerapi
        [HttpPost]
        public IHttpActionResult CreatePie(Pies pies)
        {
            if (!ModelState.IsValid)
            {
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest("Model data is invalid");
            }

            _context.Pies.Add(pies);
            _context.SaveChanges();
            return Ok(pies);
        }
        //Put /api/customerapi/1
        [HttpPut]
        public IHttpActionResult UpdatePie(int id, Pies pies)
        {
            if (!ModelState.IsValid)
            {
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest("Model data is invalid");
            }
            var pieInDb = _context.Pies.Include(m => m.PieCategory).SingleOrDefault(c => c.Id == id);
            if (pieInDb == null)
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }

            pieInDb.Name = pies.Name;
            pieInDb.SDescription = pies.SDescription;
            pieInDb.LDescription = pies.LDescription;
            pieInDb.Price = pies.Price;
            pieInDb.IsPieOfTheWeek = pies.IsPieOfTheWeek;
            pieInDb.InStock = pies.InStock;
            pieInDb.PieCategoryId = pies.PieCategoryId;
            pieInDb.Image = pies.Image;
            pieInDb.ImageThumb = pies.ImageThumb;
            _context.SaveChanges();
            return Ok();
        }
        //Delete /api/customerapi/1

        public IHttpActionResult DeletePie(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Not a Valid Customer id");
            }
            var pieInDb = _context.Pies.SingleOrDefault(c => c.Id == id);
            if (pieInDb == null)
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }

            _context.Pies.Remove(pieInDb);
            _context.SaveChanges();
            return Ok();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
