using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalPieShopTake1.Models
{
    public class PieReview
    {
        public int Id { get; set; }
        public string Review { get; set; }
        public string UserName { get; set; }
        public float Rating { get; set; }
        public Pies Pies { get; set; }
        public int PiesId { get; set; }
    }
}