using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalPieShopTake1.Models
{
    public class PieCategory
    {
        public int Id { get; set; }
        [Display(Name = "Category_Name")]
        public string CName { get; set; }
        public string Description { get; set; }
    }
}