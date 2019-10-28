using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Test1.Models;

namespace Test1.Controllers
{

    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            DBmanager dbmanager = new DBmanager();
            List<Card> cards = dbmanager.GetCards();
            ViewBag.cards = cards;
            return View();
        }

        public ActionResult CreateCard()  //新增功能實作
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCard(Card card)
        {
            DBmanager dbmanager = new DBmanager();
            try {
                dbmanager.NewCard(card);
            }
            catch(Exception e){ 
            Console.WriteLine(e.ToString());
            }



            return View(); }



        public ActionResult DeleteCard (int id)
        {
            DBmanager dBmanager = new DBmanager();
            dBmanager.DeleteCardById(id);

            return RedirectToAction("Index");
        }





        public class DBmanager
        {
            private readonly string ConnStr = "Data Source=LAPTOP-2SE654DN\\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";
            //C#內\會有逸出問題，連線字串內以\\取代



            public List<Card> GetCards()
            {
                List<Card> cards = new List<Card>();
                SqlConnection sqlConnection = new SqlConnection(ConnStr); //連接，ConnStr為上方宣告之連線字串
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Customers;");
                sqlCommand.Connection = sqlConnection;

                sqlConnection.Open(); //開啟連接

                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Card card = new Card
                        {
                            ID = reader.GetString(reader.GetOrdinal("CustomerID")),
                            Char_name = reader.GetString(reader.GetOrdinal("CompanyName")),
                            Card_name = reader.GetString(reader.GetOrdinal("ContactName")),
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



            //新增功能
            public void NewCard(Card card) //實作傳入資料庫方法
            {
                SqlConnection sqlConnection = new SqlConnection(ConnStr); //連接，ConnStr為上方宣告之連線字串
                SqlCommand sqlCommand = new SqlCommand(@" insert into  Customers(CustomerID, CompanyName, ContactName, ContactTitle, Address, City, Region, Country, Phone, Fax)
VALUES(@char_name, @card_name, @card_level, '', '', '', '', '', '', '');");
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add(new SqlParameter("@char_name", card.ID));
                sqlCommand.Parameters.Add(new SqlParameter("@card_name", card.Char_name));
                sqlCommand.Parameters.Add(new SqlParameter("@card_level",card.Card_name));

               
                sqlConnection.Open(); //開啟連接
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close(); //關閉連接

               
            }







            //刪除功能
            public void DeleteCardById(int id) {
                SqlConnection sqlConnection = new SqlConnection(ConnStr); //連接，ConnStr為上方宣告之連線字串
                SqlCommand sqlCommand = new SqlCommand("Delete  FROM Customers Where CustomerID = @id;");
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add(new SqlParameter("@id", id));
                sqlConnection.Open(); //開啟連接
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close(); //關閉連接

            }





        }

    









      


      



















    }
}