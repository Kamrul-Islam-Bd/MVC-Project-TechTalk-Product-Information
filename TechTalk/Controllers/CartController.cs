using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechTalk.Models;

namespace TechTalk.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController()
        {
            _cartService = new CartService(HttpContext);
        }

        public ActionResult Index()
        {
            var cart = _cartService.GetCart();
            return View(cart);
        }

        public ActionResult AddToCart(int productId, int quantity)
        {
            _cartService.AddToCart(productId, quantity);
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult RemoveFromCart(int productId)
        {
            _cartService.RemoveFromCart(productId);
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult EmptyCart()
        {
            _cartService.EmptyCart();
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult CartSummary()
        {
            var cart = _cartService.GetCart();
            return PartialView("_CartSummary", cart);
        }
    }
}