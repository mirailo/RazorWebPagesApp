using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RazorWebPagesApp.Models
{
    public class User
    {
        public int ID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string EmailAdd { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
