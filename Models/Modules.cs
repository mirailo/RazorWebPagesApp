using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RazorWebPagesApp.Models
{
    public class Modules
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Module Code")]
        public string ModCode { get; set; }

        [Required]
        [Display(Name = "Module Name")]
        public string ModName { get; set; }

        [Required]
        [Display(Name = "Module Level")]
        public int ModLevel { get; set; }

        [Required]
        [Display(Name = "Module Semester")]
        public int ModSemester { get; set; }
    }
}
