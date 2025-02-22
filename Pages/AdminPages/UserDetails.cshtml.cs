using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorWebPagesApp.Models;
using RazorWebPagesApp.Data;

namespace RazorWebPagesApp.Pages.AdminPages
{
    public class UserDetailsModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public UserDetailsModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public string UserName;
        public const string SessionKeyName = "username";

        public IList<User> User { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            User = await _context.User.ToListAsync();

            UserName = HttpContext.Session.GetString(SessionKeyName);
            UserName = HttpContext.Session.Id;
            Console.WriteLine("Current session: " + UserName);

            if (string.IsNullOrEmpty(UserName))
            {
                Console.WriteLine("Session ended");
                return RedirectToPage("/AdminPage/Index2");
            }
            else
            {
                return Page();
            }

        }
    }
}
