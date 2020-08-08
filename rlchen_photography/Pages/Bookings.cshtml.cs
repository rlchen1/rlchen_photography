using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Photography_Project.Pages
{
    public class BookingsModel : PageModel
    {
        public string Message { get; set; }
        public string Reply { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string date { get; set; }
        public string comment { get; set; }

        public void OnGet()
        {
            ViewData["Message"] = "Book your photoshoot day in advance!";
            ViewData["Reply"] = "";
        }

        public bool ValidFirstName(string firstName)
        {
            var firstNameRegEx = @"^([a-z\s]){1,}$";
            if (firstName == null || (!Regex.IsMatch(firstName, firstNameRegEx, RegexOptions.IgnoreCase)))
            {
                return false;
            }

            return true;
        }

        public bool ValidLastName(string lastName)
        {
            var lastNameRegEx = @"^([a-z\s]){1,}$";
            if (lastName == null || (!Regex.IsMatch(lastName, lastNameRegEx, RegexOptions.IgnoreCase)))
            {
                return false;
            }

            return true;
        }

        public void OnPost()
        {
            // Handle multiple request submission by resetting reply to null 
            ViewData["Reply"] = "";

            var firstName = Request.Form["firstName"];
            var lastName = Request.Form["lastName"];
            var email = Request.Form["email"];
            var date = Request.Form["date"];
            var comment = Request.Form["comment"];

            // Validation checks before submitting data to DB
            this.firstName = firstName;
            this.lastName = lastName;
            //first_name.Text = firstName;
            if ((!ValidFirstName(firstName)) || (!ValidLastName(lastName)))
            {
                ViewData["Message"] = "Book your photoshoot day in advance!";
               
                return;
            }

            // Initialize a new class instance to store the local variable values
            BookingsModel newBooking = new BookingsModel();

            newBooking.firstName = firstName;
            newBooking.lastName = lastName;
            newBooking.email = email;
            newBooking.date = date;
            newBooking.comment = comment;

            // SQL Database connection: write the data from class instance newBooking to rlchen_photography DB
            //string mainconn = @"Server=tcp:rlchenphotography.database.windows.net,1433;Initial Catalog=rlchen_photography;Persist Security Info=False;User ID=rlchen;Password=Wooper679;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
            string mainconn = @"Data Source=rlchenphotography.database.windows.net;Initial Catalog=rlchen_photography;Persist Security Info=True;User ID=rlchen;Password=Wooper679";
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "Insert Into [dbo].[rlchen_bookings] (firstName, lastName, email, date, comment)" +
                "Values (@firstName, @lastName, @email, @date, @comment)";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlconn.Open();
            sqlcomm.Parameters.AddWithValue("@firstName", newBooking.firstName);
            sqlcomm.Parameters.AddWithValue("@lastName", newBooking.lastName);
            sqlcomm.Parameters.AddWithValue("@email", newBooking.email);
            sqlcomm.Parameters.AddWithValue("@date", newBooking.date);
            sqlcomm.Parameters.AddWithValue("@comment", newBooking.comment);
            sqlcomm.ExecuteNonQuery();
            sqlconn.Close();

            // Response updates on successful DB entry
            ViewData["Message"] = "Book your photoshoot day in advance!";
            ViewData["Reply"] = $"Thank you {firstName}, we've received your request for {date} and will contact you soon at {email}.";
        }  
    }
}
