using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechTalk.ViewModels.ProductViewModels
{
    public class InventoryViewModel
    {
        public int InventoryId { get; set; }

        [Required, Display(Name = "Product")]
        public int ProductId { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int QuantityInStock { get; set; }
    }

}