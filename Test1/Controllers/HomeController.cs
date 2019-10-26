using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

            ViewBag.Date = date;
            ViewBag.Student = data;
            ViewBag.List = list;
            return View();
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