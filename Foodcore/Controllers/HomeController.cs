using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using  System.Linq;
using Foodcore.Models;
using System.Data;

namespace Foodcore.Controllers
{
    public class HomeController : Controller
    {
        FoodcoreContext dc = new FoodcoreContext();

        [HttpGet]
        public ViewResult Login()
        {// logic for login page goes here

            return View();

        }

        [HttpPost]
        public ActionResult Login(IFormCollection frm)
        {

            string uname = frm["uname"];
            string pwd = frm["pwd"];



            //var res = (from t in dc.Registers
            //           where t.Username == uname && t.Pwd == pwd
            //           select t).Count();


            var c = dc.Registers.ToList().Where(c => c.Username == uname && c.Pwd == pwd).Count();

            if (c > 0)
            {
                HttpContext.Session.SetString("uid", uname);
                return RedirectToAction("Menu");

                // valid
            }
            else
            {

                ViewData["v"] = "Invalid username or password";
                // not valid
                return View();
            }

        }



        public ViewResult Logout()
        {// logic for login page goes here
            HttpContext.Session.Remove("uid");
            return View();

        }

        public ViewResult Home()
        {
            return View();
        }
        public ViewResult Menu()
        {
            var result = dc.Menus.ToList();
            return View(result);
        }





        [HttpGet]
        public ViewResult Myorders(string myitemid)
        {
            // is it 1 or many

            var result = dc.Menus.ToList().Find(c => c.Itemid == myitemid);

            if (result != null)
            {
                TempData["p"] = result.Price;
                TempData["i"] = result.Itemid;

                TempData.Keep();
            }

            return View(result);


        }


        [HttpPost]
        public ActionResult Myorders(IFormCollection c)
        {

            // insert new value to myorders table

            if (HttpContext.Session.GetString("uid") == null)
            {
                return RedirectToAction("Login");
            }

            else
            {
                Order o = new Order();
                o.Username = HttpContext.Session.GetString("uid");
                o.Itemid = TempData["i"].ToString();
                o.Price = Convert.ToInt32(TempData["p"]);
                o.Qty = Convert.ToInt32(c["t1"]);

                dc.Orders.Add(o);
                int i = dc.SaveChanges();

                if (i > 0)
                {
                    ViewData["a"] = "Your order placed successfully";
                }
                else
                {
                    ViewData["a"] = "Error occured try after some time";
                }

                return View();

            }


        }

        [HttpGet]
        public ActionResult Addtocart(string myitemid)
        {
            //  var result = dc.Menus.ToList().Find(c => c.Itemid == myitemid);

            //  li.Add(result);

            Mycart m = new Mycart();
            m.Username = HttpContext.Session.GetString("uid");
            m.Itemcode = myitemid;
            dc.Mycarts.Add(m);

            int i = dc.SaveChanges();
            if (i > 0)
            {
                ViewData["a"] = myitemid + "  Item Add successfully to cart";
            }
            else
            {
                ViewData["a"] = myitemid + "failed to add try again";
            }

            //TempData.Add(myitemid, myitemid);

            var res = dc.Menus.ToList().Join(dc.Mycarts.ToList(), c => c.Itemid, w => w.Itemcode, (c, w) => new { c.Itemid, c.Itemname, c.Price, c.Images, c.Itemdesc });

            int? sum = 0;

            foreach (var item in res)
            {
                sum = sum + item.Price;


            }

            ViewData["total"] = sum;


            return View(res);


        }






        [HttpGet]
        public ViewResult Registers()
        {

            return View();

        }

        [HttpPost]
        public ViewResult Registers(Register r)
        {
            /// using model state property u can find wheater page having error or not
            // is valid will be true if there is no error in the page
            // is valid will be false if it contain any error



            if (ModelState.IsValid)
            {
                dc.Registers.Add(r);
                int i = dc.SaveChanges();

                if (i > 0)
                {
                    ViewData["a"] = "New User created successfully";
                }
                else
                {
                    ViewData["a"] = "Error occured try after some time";
                }

                return View();
            }
            else
            {
                return View();
            }

        }


        public ViewResult Contact()
        {
            return View();
        }


    }
}
