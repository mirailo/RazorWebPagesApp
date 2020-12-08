using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RazorWebPagesApp.Models
{
    public class WelcomePage
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Date Update")]
        public string DateUpdate { get; set; }


        [Required]
        [Display(Name = "Welcome Message")]
        public string Message { get; set; }
    }
}
