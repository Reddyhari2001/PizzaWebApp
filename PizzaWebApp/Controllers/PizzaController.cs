using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PizzaWebApp.Models;

namespace PizzaWebApp.Controllers
{
    public class PizzaController : Controller
    {
        static public List<onlinepizza> pizzadetails = new List<onlinepizza>() {

                new onlinepizza { PizzaId = 100,Type = "Corn", Price =250},
                new onlinepizza { PizzaId = 101,Type = "Cheese",Price=300},
                new onlinepizza { PizzaId = 102,Type = "Mexican",Price=450}
            };
        static public List<OrderPage> orderdetails = new List<OrderPage>();


        public IActionResult Index()
        {
            return View(pizzadetails);

        }


        public IActionResult Cart(int id)
        {
            var found = (pizzadetails.Find(p => p.PizzaId == id));

            TempData["id"] = id;

            return View(found);

        }
        [HttpPost]
        public IActionResult Cart(IFormCollection f)
        {
            Random r = new Random();
            int id = Convert.ToInt32(TempData["id"]);
            OrderPage o = new OrderPage();
            var found = (pizzadetails.Find(p => p.PizzaId == id));
            o.OrderId = r.Next(100, 999);
            o.PizzaId = id;
            o.Price = found.Price;
            o.Type = found.Type;
            o.Quantity = Convert.ToInt32(Request.Form["qty"]);
            o.TotalPrice = o.Price * o.Quantity;

            orderdetails.Add(o);

            return RedirectToAction("Checkout");

        }


        public IActionResult Checkout()
        {    
            return View(orderdetails);

        }
    }
}
