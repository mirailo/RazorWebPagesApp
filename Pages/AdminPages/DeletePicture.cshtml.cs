using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorWebPagesApp.Models;
using RazorWebPagesApp.Data;
using RazorWebPagesApp.Pages.DatabaseConnection;
using Microsoft.Data.Sqlite;

namespace RazorWebPagesApp.Pages.AdminPages
{
    public class DeletePictureModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;
        private readonly IWebHostEnvironment _env;

        public DeletePictureModel(RazorPagesMovieContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public Picture Picture { get; set; }

        public string PicName { get; set; }
        public int PictureID { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.User.FirstOrDefaultAsync(m => m.ID == id); //getting data from table User
            var UserEmail = User.EmailAdd;

            if (User == null)
            {
                return NotFound();
            }

            //retrieve the file name for the user using email address (email address is a primary key for table Picture)
            var connectionStringBuilder = new SqliteConnectionStringBuilder();

            DatabaseConnect DBCon = new DatabaseConnect(); // your own class and method in DatabaseConnection folder
            string dbStringConnection = DBCon.DBStringConnection();

            connectionStringBuilder.DataSource = dbStringConnection;
            var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

            connection.Open();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = @"SELECT PicName, Id FROM Picture WHERE Email=$email";
            selectCmd.Parameters.AddWithValue("$email", User.EmailAdd);

            var reader = selectCmd.ExecuteReader();


            while (reader.Read())
            {
                PicName = reader.GetString(0);
                PictureID = reader.GetInt32(1);

            }
            Console.WriteLine("Pic name delete pic page : " + PicName);
            return Page();
        }



        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Console.WriteLine("OnPost is performed");

            User = await _context.User.FindAsync(id);

            var connectionStringBuilder = new SqliteConnectionStringBuilder();

            DatabaseConnect DBCon = new DatabaseConnect(); // your own class and method in DatabaseConnection folder
            string dbStringConnection = DBCon.DBStringConnection();

            connectionStringBuilder.DataSource = dbStringConnection;
            var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

            connection.Open();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = @"SELECT PicName, Id FROM Picture WHERE Email=$email";
            selectCmd.Parameters.AddWithValue("$email", User.EmailAdd);

            var reader = selectCmd.ExecuteReader();

            while (reader.Read())
            {
                PicName = reader.GetString(0);
                PictureID = reader.GetInt32(1);

            }

            Console.WriteLine("Pic name onpost : " + PicName);
            if (!string.IsNullOrEmpty(PicName))
            {
                deletePicture(PicName, User.EmailAdd);
            }

            return RedirectToPage("/AdminPage/UserDetails");
        }



        public void deletePicture(string PicName, string email)
        {

            string RetrieveImage = Path.Combine(_env.ContentRootPath, "ImageData", PicName);
            System.IO.File.Delete(RetrieveImage);
            Console.WriteLine("File has been deleted");

            var connectionStringBuilder = new SqliteConnectionStringBuilder();

            DatabaseConnect DBCon = new DatabaseConnect(); // your own class and method in DatabaseConnection folder
            string dbStringConnection = DBCon.DBStringConnection();

            connectionStringBuilder.DataSource = dbStringConnection;
            var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

            connection.Open();

            var selectCmd2 = connection.CreateCommand();
            selectCmd2.CommandText = @"DELETE FROM Picture WHERE Email=$email";
            selectCmd2.Parameters.AddWithValue("$email", email);


            selectCmd2.Prepare();
            selectCmd2.ExecuteNonQuery();
        }



    }

}
