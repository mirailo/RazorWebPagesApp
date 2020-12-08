using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RazorWebPagesApp.Models
{
    public class Picture
    {
        public int Id { get; set; }

        [Required]
        public string PicName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }
    }
}
