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
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Enter Only Alphabet")]
        public string FName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public string LName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address  is required")]
        [StringLength(40, ErrorMessage = "Max length 40")]
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone No.  is required")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile number")]
        public Int64 PhoneNo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email  is required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password  is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm  Password  is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirm Passwoerd should be same")]
        public string ConfirmPassword { get; set; }
        public bool IsEmailVerfied { get; set; }
        public System.Guid ActivationCode { get; set; }

    }
}