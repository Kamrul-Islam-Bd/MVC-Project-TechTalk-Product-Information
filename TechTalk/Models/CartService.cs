using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechTalk.Models
{
    public class CartService
    {
        private readonly HttpContextBase _httpContext;

        public CartService(HttpContextBase httpContext)
        {
            _httpContext = httpContext;
        }

        private const string CartSessionKey = "Cart";

        public Cart GetCart()
        {
            var cart = (Cart)_httpContext.Session[CartSessionKey];
            if (cart == null)
            {
                cart = new Cart();
                _httpContext.Session[CartSessionKey] = cart;
            }
            return cart;
        }

        public void AddToCart(int productId, int quantity)
        {
            var db = new TechDbContext(); // Assuming ApplicationDbContext is your EF context

            var product = db.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                var cart = GetCart();

                var cartItem = cart.CartItems.FirstOrDefault(item => item.Product.ProductId == productId);

                if (cartItem != null)
                {
                    cartItem.Quantity += quantity;
                }
                else
                {
                    cart.CartItems.Add(new CartItem
                    {
                        Product = product,
                        Quantity = quantity
                    });
                }
            }
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCart();

            var cartItem = cart.CartItems.FirstOrDefault(item => item.Product.ProductId == productId);

            if (cartItem != null)
            {
                cart.CartItems.Remove(cartItem);
            }
        }

        public void EmptyCart()
        {
            var cart = GetCart();
            cart.CartItems.Clear();
        }

        public decimal GetCartTotal()
        {
            var cart = GetCart();
            return cart.CartItems.Sum(item => item.Product.Price * item.Quantity);
        }
    }
}