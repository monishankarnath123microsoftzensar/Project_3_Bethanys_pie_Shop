using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalPieShopTake1.Models
{
    public class Pies
    {
        public int Id { get; set; }
        [Display(Name = "Pie Name")]
        public string Name { get; set; }
        [Display(Name = "Short Description")]
        public string SDescription { get; set; }
        [Display(Name = "Long Description")]
        public string LDescription { get; set; }
        public float Price { get; set; }
        public bool IsPieOfTheWeek { get; set; }
        public bool InStock { get; set; }
        public string Image { get; set; }
        [Display(Name = "Thumbnail Image")]
        public string ImageThumb { get; set; }
        public PieCategory PieCategory { get; set; }
        public int PieCategoryId { get; set; }

    }
}