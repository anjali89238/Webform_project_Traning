using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Register
    {
        [Required(ErrorMessage ="Name is required")]
        [StringLength(50,ErrorMessage ="Name must be less than 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Enter a vaild Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        [RegularExpression(@"^[0-9]{10}$",ErrorMessage ="Enter a valid 10 digit Mobile number")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Image is required")]
        public string   Image { get; set; }
        [Required(ErrorMessage = "AdharNo is required")]
        [RegularExpression(@"^[0-9]{12}$",ErrorMessage ="Enter a valid 12 digit Adhaar Number")]
        public string AdharNo { get; set; }

    }
}