using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalPieShopTake1.Models;

namespace FinalPieShopTake1.ViewModel
{
    public class NewPieViewModel
    {
        public IEnumerable<PieCategory> PieCategories { get; set; }
        public Pies Pies { get; set; }
    }
}