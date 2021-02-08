using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalPieShopTake1.Models
{
    public class Cart
    {
        public int productid { get; set; }
        public string productname { get; set; }
        public float price { get; set; }
        [Range(1, 5, ErrorMessage = "Should be 1-5")]
        public int qty { get; set; }
        public float bill { get; set; }
    }
}