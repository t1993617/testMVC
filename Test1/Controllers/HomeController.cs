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

        //客戶資料
        public class Card
        {
            public string ID { get; set; }
            public string Char_name { get; set; }
            public string Card_name { get; set; }
            public string Card_level { get; set; }
        }



        public class Student
        {
            public string id { get; set; }
            public string name { get; set; }
            public String score { get; set; }
            public Student()
            {
                id = string.Empty;
                name = string.Empty;
                score = string.Empty;
            }



            public Student(string _id, string _name, string _score)
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



            //test範圍=================================================
            /*欄位
CustomerID
CompanyName
ContactName
ContactTitle
Address
City
Region
Country
Phone
Fax
             */








            //   Data Source = LAPTOP - 2SE654DN\SQLEXPRESS; Initial Catalog = Northwind; Integrated Security = True

            SqlConnection XXX = new SqlConnection();

            XXX = new SqlConnection(@"Data Source=LAPTOP-2SE654DN\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True");


            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Customers");
            sqlCommand.Connection = XXX;
            XXX.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();


            //=============================================================

            DateTime date = DateTime.Now;
            Student data = new Student();
            List<Student> list = new List<Student>();



            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Student data1 = new Student()
                    {
                        id = reader.GetString(reader.GetOrdinal("CustomerID")),
                        name = reader.GetString(reader.GetOrdinal("CompanyName")),
                        score = reader.GetString(reader.GetOrdinal("ContactName")),
                      
                       
                };
                    list.Add(data1);
                }
            }





        //====================================================













      
        list.Add(new Student("1", "小明", "80"));
            list.Add(new Student("2", "小華", "70"));
            list.Add(new Student("3", "小英", "60"));
            list.Add(new Student("4", "小李", "50"));
            list.Add(new Student("5", "小張", "90"));
            list.Add(new Student("!!!!!", "~~~~~~~", "8"));


            ViewBag.Date = date;
            ViewBag.Student = data;
            ViewBag.List = list;
            return View();
 
        
        
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
                        ID = reader.GetString(reader.GetOrdinal("id")),
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