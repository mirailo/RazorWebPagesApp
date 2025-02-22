using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using static System.Net.Mime.MediaTypeNames;
using RazorWebPagesApp.Pages.BrowsePicture;
using RazorWebPagesApp.Pages.DatabaseConnection;
using RazorWebPagesApp.Models;
using RazorWebPagesApp.Data;


namespace RazorWebPagesApp.Pages.BrowsePicture
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;
        private readonly IWebHostEnvironment _env;


        public IndexModel(RazorPagesMovieContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;


        }

        [BindProperty]
        public IFormFile UploadFile { get; set; }

        [BindProperty]
        public Picture PicData { get; set; }

        [BindProperty]
        public User UserRec { get; set; }



        public async Task<IActionResult> OnGetAsync(int? id)
        {
            UserRec = await _context.User.FirstOrDefaultAsync(m => m.ID == id);

            //Console.WriteLine(EmailAddress);

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            DatabaseConnect DBCon = new DatabaseConnect(); // your own class and method in DatabaseConnection folder
            string dbStringConnection = DBCon.DBStringConnection();

            connectionStringBuilder.DataSource = dbStringConnection;
            var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

            connection.Open();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = @"SELECT FirstName FROM User WHERE EmailAdd=$email";
            selectCmd.Parameters.AddWithValue("$email", UserRec.EmailAdd);

            var reader = selectCmd.ExecuteReader();


            while (reader.Read())
            {
                UserRec.FirstName = reader.GetString(0);
            }

            if (UserRec.FirstName == null)
            {
                return NotFound();
            }

            return Page();
        }



        public async Task<IActionResult> OnPostAsync(int? id)
        {
            UserRec = await _context.User.FirstOrDefaultAsync(m => m.ID == id);

            Boolean check = CheckPic(UserRec.FirstName, UserRec.EmailAdd);

            if (!check)
            {

                //Saving the file to the server
                var Fileupload = Path.Combine(_env.WebRootPath, "Images", UploadFile.FileName);
                Console.WriteLine(Fileupload);
                using (var Fstream = new FileStream(Fileupload, FileMode.Create))
                {
                    await UploadFile.CopyToAsync(Fstream);
                    ViewData["Message"] = "File Uploaded to Image Data folder";

                }

                Console.WriteLine("Email is -->" + UserRec.EmailAdd);
                Console.WriteLine("File Name is -->" + UploadFile.FileName);
                Console.WriteLine("First Name is -->" + UserRec.FirstName);

                var connectionStringBuilder = new SqliteConnectionStringBuilder();
                DatabaseConnect DBCon = new DatabaseConnect(); // your own class and method in DatabaseConnection folder
                string dbStringConnection = DBCon.DBStringConnection();

                connectionStringBuilder.DataSource = dbStringConnection;
                var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

                connection.Open();

                var selectCmd2 = connection.CreateCommand();
                selectCmd2.CommandText = @"INSERT INTO Picture (Email, PicName, FirstName) VALUES ($email, $PicName, $firstName)";
                selectCmd2.Parameters.AddWithValue("$email", UserRec.EmailAdd);
                selectCmd2.Parameters.AddWithValue("$PicName", UploadFile.FileName);
                selectCmd2.Parameters.AddWithValue("$firstName", UserRec.FirstName);

                selectCmd2.Prepare();
                selectCmd2.ExecuteNonQuery();

                return RedirectToPage("/AdminPage/UserDetails");

            }
            else
            {
                ViewData["Message"] = "The user already has a picture. Go to update profile.";
                return Page();
            }



        }

        public Boolean CheckPic(string FName, string Email)
        {
            Boolean status = false;

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            DatabaseConnect DBCon = new DatabaseConnect(); // your own class and method in DatabaseConnection folder
            string dbStringConnection = DBCon.DBStringConnection();

            connectionStringBuilder.DataSource = dbStringConnection;
            var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

            connection.Open();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = @"SELECT FirstName FROM Picture WHERE Email=$email";
            selectCmd.Parameters.AddWithValue("$email", Email);

            var reader = selectCmd.ExecuteReader();
            var Name = "";

            while (reader.Read())
            {
                Name = reader.GetString(0);
            }

            if (FName == Name)
            {
                status = true;
            }

            return status;
        }



    }
}
