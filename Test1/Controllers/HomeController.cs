using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;



namespace Test1.Controllers
{
    public class HomeController : Controller
    {
   

        public class Student
        {
            public string id { get; set; }
            public string name { get; set; }
            public int score { get; set; }
            public Student()
            {
                id = string.Empty;
                name = string.Empty;
                score = 0;
            }
            public Student(string _id, string _name, int _score)
            {
                id = _id;
                name = _name;
                score = _score;
            }
            public override string ToString()
            {
                return $"學號:{id}, 姓名:{name}, 分數:{score}.";
            }
        }

        public ActionResult Index()
        {
            DateTime date = DateTime.Now;
            Student data = new Student();
            List<Student> list = new List<Student>();
            list.Add(new Student("1", "小明", 80));
            list.Add(new Student("2", "小華", 70));
            list.Add(new Student("3", "小英", 60));
            list.Add(new Student("4", "小李", 50));
            list.Add(new Student("5", "小張", 90));
            list.Add(new Student("!!!!!", "~~~~~~~", 8));

            //test範圍=================================================

            /*
             //   Data Source = LAPTOP - 2SE654DN\SQLEXPRESS; Initial Catalog = Northwind; Integrated Security = True

            SqlConnection sqlConnection = new SqlConnection(@"Server =LAPTOP - 2SE654DN\SQLEXPRESS; Database =  Northwind; Trusted_Connection = SSPI");
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Customers");
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Card card = new Card
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("id")),
                        Char_name = reader.GetString(reader.GetOrdinal("char_name")),
                        Card_name = reader.GetString(reader.GetOrdinal("card_name")),
                        Card_level = reader.GetString(reader.GetOrdinal("card_level")),
                    
                    };
                   
                }
            }
    */
            //====================================================

            ViewBag.Date = date;
            ViewBag.Student = data;
            ViewBag.List = list;
            return View();
 
        
        
        }









        public class Card
        {
            public int ID { get; set; }
            public string Char_name { get; set; }
            public string Card_name { get; set; }
            public string Card_level { get; set; }
        }



        public List<Card> GetCards()
        {
            ViewBag.Date = "cccccccccccc";
            List<Card> cards = new List<Card>();
            SqlConnection sqlConnection = new SqlConnection(@"Data Source = LAPTOP - 2SE654DN\SQLEXPRESS; Initial Catalog = Northwind; Integrated Security = True");
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Customers");
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Card card = new Card
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("id")),
                        Char_name = reader.GetString(reader.GetOrdinal("char_name")),
                        Card_name = reader.GetString(reader.GetOrdinal("card_name")),
                        Card_level = reader.GetString(reader.GetOrdinal("card_level")),
                    };
                    cards.Add(card);
                }
            }
            else
            {
                Console.WriteLine("資料庫為空！");
            }
            sqlConnection.Close();
            return cards;
        }































        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}