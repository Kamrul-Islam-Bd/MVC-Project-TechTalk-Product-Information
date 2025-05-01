using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechTalk.ViewModels;

namespace TechTalk.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductsController _productsController;

        public HomeController()
        {
            _productsController = new ProductsController();
        }

        public ActionResult Index()
        {
            var productsActionResult = _productsController.Index() as ViewResult;

            if (productsActionResult != null && productsActionResult.Model is IEnumerable<Product>)
            {
                var products = (IEnumerable<Product>)productsActionResult.Model;
                return View(products);
            }
            else
            {
                return View("Error"); 
            }
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