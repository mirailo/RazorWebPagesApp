using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorWebPagesApp.Models;

namespace RazorWebPagesApp.Data
{
    public class RazorPagesMovieContext : DbContext
    {
        internal object Picture;
        public RazorPagesMovieContext(DbContextOptions<RazorPagesMovieContext> options)
                : base(options)
        {
        }

        //public DbSet<RazorWebPagesApp.Models.Picture> Picture { get; set; }

        public DbSet<RazorWebPagesApp.Models.User> User { get; set; }

        public DbSet<RazorWebPagesApp.Models.AdminUser> AdminUser { get; set; }

        public DbSet<RazorWebPagesApp.Models.Modules> Modules { get; set; }


    }
}