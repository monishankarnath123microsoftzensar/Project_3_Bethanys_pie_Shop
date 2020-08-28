using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalPieShopTake1.Models
{
    public class RegisterUser
    {
        public int Id { get; set; }
        [Display(Name = "First_Name")]
        public string FName { get; set; }
        [Display(Name = "Last_Name")]
        public string LName { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PhoneNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}