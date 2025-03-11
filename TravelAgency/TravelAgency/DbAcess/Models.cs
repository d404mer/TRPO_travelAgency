using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.DbAcess
{
    public class Country
    {
        public int Country_ID { get; set; }
        public string Country_Name { get; set; }
    }

    public class Tour
    {
        public int Tour_ID { get; set; }
        public string Tour_Name { get; set; }
        public int? Country_ID { get; set; }
        public int Stay_Time { get; set; }
        public decimal Price { get; set; }
    }

    public class Guide
    {
        public int Guid_ID { get; set; }
        public string Guid_Name { get; set; }
        public string Guid_Lastname { get; set; }
    }

    public class TourGuide
    {
        public int Tour_Guid_ID { get; set; }
        public int Tour_ID { get; set; }
        public int Guid_ID { get; set; }
    }

   public class Agent
{
    public string Agent_Name { get; set; }
    public string Name { get; set; }
    public string Last_Name { get; set; }
    public string Role { get; set; }
    public string Phone_Number { get; set; }
    public string Email { get; set; }
    public string Password { get; set; } // Добавлено свойство для пароля
}

    public class Hotel
    {
        public int Hotel_ID { get; set; }
        public string Hotel_Name { get; set; }
        public int Stars { get; set; }
        public decimal Price_Per_Night { get; set; }
    }

    public class Book
    {
        public int Book_ID { get; set; }
        public string Agent_ID { get; set; }
        public int Tour_ID { get; set; }
        public DateTime Date_Of_Book { get; set; }
        public int? Hotel_ID { get; set; }
        public decimal Price { get; set; }
    }
}
