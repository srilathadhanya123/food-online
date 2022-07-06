using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Foodcore.Models;
using System.Data;
namespace Foodcore.Controllers
{
    public class TestingController1 : Controller
    {
        FoodcoreContext dc = new FoodcoreContext();



        public ViewResult Printallnames()
        {
            string[] Names = { "Rohit", "sachin", "Dhoni", "Virat", "Raina" };
            ViewBag.i = Names;


            return View("Testing");
        }

        [HttpGet]
        public ViewResult Acceptnprint()
        {



            return View("Testing");
        }
        [HttpPost]
        public ViewResult Acceptnprint(IFormCollection c)
        {
            ViewBag.num = c["t1"];
            ViewBag.name = c["t2"];

            return View("Testing");
        }


        [HttpGet]
        public ViewResult Findgreatest()
        {


            return View("Testing");
        }
        [HttpPost]
        public ViewResult Findgreatest(IFormCollection c)
        {
            ViewBag.num1 = c["t1"];
            ViewBag.num2 = c["t2"];

            return View("Testing");
        }


        [HttpGet]
        public ViewResult Login()
        {// logic for login page goes here

            return View();

        }
              [HttpPost]
        public ViewResult Login(IFormCollection frm)
        {// logic for login page goes here

            if (frm["uname"] == "ajay" && frm["pwd"] == "india")
            {
                ViewData["v"] = "Valid user";
            }
            else
            {
                ViewData["v"] = "InValid user";
            }

            return View();

        }







        [HttpGet]
        public ViewResult reg()
        {


            return View("Testing");
        }
        [HttpPost]
        public ViewResult reg(IFormCollection c)
        {
            ViewBag.num1 = c["empid"];
            ViewBag.num2 = c["empname"];
            ViewBag.num3 = c["DOB"];
            ViewBag.num4 = c["gender"];
            ViewBag.num5 = c["Nationality"];
            return View("Testing");
        }
    }
}
    
