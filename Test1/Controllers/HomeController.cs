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



        //新增功能=====================================================================================


        public ActionResult CreateCard()  //新增功能實作
        {
            return View();
        }



        [HttpPost]
        public ActionResult CreateCard(Card card)
        {
            DBmanager dbmanager = new DBmanager();
            try
            {
                dbmanager.NewCard(card);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return RedirectToAction("Index");
        }




        //修改功能===================================================================================

        public ActionResult EditCard(String id)
        {
            DBmanager dBmanager = new DBmanager();
            Card card= dBmanager.GetCardById(id);
            return View(card);
        }

        [HttpPost]
        public ActionResult EditCard(Card card)
        {
            DBmanager dBmanager = new DBmanager();
            dBmanager.UpdateCard(card);
            return RedirectToAction("Index");
        }

        //刪除功能====================================================================================
        

        public ActionResult DeleteCard (String id)
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
                            Company = reader.GetString(reader.GetOrdinal("CompanyName")),
                            Contact = reader.GetString(reader.GetOrdinal("ContactName")),
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


            //修改功能:搜尋原本的資料==================================================

            public Card GetCardById(String id)
            {
                Card card = new Card();
                SqlConnection sqlConnection = new SqlConnection(ConnStr); //連接，ConnStr為上方宣告之連線字串
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Customers Where CustomerID=@id;");
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add(new SqlParameter("@id",id)); //將指定的SqlParameter(輸入本函式的String)物件加入至SqlParameterCollection中
                sqlConnection.Open(); //開啟連接

                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        card = new Card
                        {
                            ID = reader.GetString(reader.GetOrdinal("CustomerID")),
                            Company = reader.GetString(reader.GetOrdinal("CompanyName")),
                            Contact = reader.GetString(reader.GetOrdinal("ContactName")),
                        };
                    }
                }
                else
                {
                   card.ID = "未找到該筆資料,請返回首頁";
                }
                sqlConnection.Close();
                return card;
            }











            //新增功能==============================================================================================
            public void NewCard(Card card) //實作傳入資料庫方法
            {
                SqlConnection sqlConnection = new SqlConnection(ConnStr); //連接，ConnStr為上方宣告之連線字串
                SqlCommand sqlCommand = new SqlCommand("insert into  Customers(CustomerID, CompanyName, ContactName, ContactTitle, Address, City, Region, Country, Phone, Fax) VALUES (@id, @company, @contact, '1', '1', '1', '1', '1', '1', '1');");
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add(new SqlParameter("@id", card.ID)); //把值輸入上方SQL
                sqlCommand.Parameters.Add(new SqlParameter("@company", card.Company));
                sqlCommand.Parameters.Add(new SqlParameter("@contact",card.Contact));

               
                sqlConnection.Open(); //開啟連接
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close(); //關閉連接

               
            }



            //修改功能實作===========================================================================
            public void UpdateCard(Card card) //實作傳入資料庫方法
            {
                SqlConnection sqlConnection = new SqlConnection(ConnStr); //連接，ConnStr為上方宣告之連線字串
                SqlCommand sqlCommand = new SqlCommand("Update  Customers   SET   CustomerID=@id, CompanyName=@company, ContactName=@contact Where CustomerID=@id;");

                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add(new SqlParameter("@id", card.ID));
                sqlCommand.Parameters.Add(new SqlParameter("@company", card.Company));
                sqlCommand.Parameters.Add(new SqlParameter("@contact", card.Contact));


                sqlConnection.Open(); //開啟連接
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close(); //關閉連接


            }




            //刪除功能=====================================================================================================

            public void DeleteCardById(String id) {
                SqlConnection sqlConnection = new SqlConnection(ConnStr); //連接，ConnStr為上方宣告之連線字串
                SqlCommand sqlCommand = new SqlCommand("DELETE FROM Customers where CustomerID=@id;");
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add(new SqlParameter("@id", id)); //將指定的SqlParameter物件加入至SqlParameterCollection中
                sqlConnection.Open(); //開啟連接
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close(); //關閉連接

            }





        }

    









      


      



















    }
}