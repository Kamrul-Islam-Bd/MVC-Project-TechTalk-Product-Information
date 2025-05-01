using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechTalk.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}