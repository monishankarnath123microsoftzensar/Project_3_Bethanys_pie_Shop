using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalPieShopTake1.Models;

namespace FinalPieShopTake1.Controllers.Api
{
    public class CategoryDDApiController : ApiController
    {
        private ApplicationDbContext _context;
        public CategoryDDApiController()
        {
            _context = new ApplicationDbContext();
        }
        public IEnumerable<PieCategory> GetGenre()
        {
            var genre = _context.PieCategories.ToList();
            return genre;

        }
    }
}
