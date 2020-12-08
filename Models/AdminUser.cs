using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace RazorWebPagesApp.Models
{
    public class AdminUser
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Staff No")]
        public string StaffNo { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Admin User Name")]
        public string AdminUserName { get; set; }

        [Required]
        [Display(Name = "Admin Password")]
        public string AdminPassword { get; set; }
    }
}
