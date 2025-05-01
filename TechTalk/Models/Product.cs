using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using TechTalk.Models;

namespace TechTalk.ViewModels
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }



        [Required, Display(Name = "Product Name"), StringLength(200)]
        public string ProductName { get; set; }



        [Required, Display(Name = "Product Details"), StringLength(1000)]
        public string ProductDescription { get; set; }



        [Required, Range(0, double.MaxValue)]
        public decimal Price { get; set; }



        [Required, Display(Name = "Stock Count"), Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }



        [Display(Name = "Picture URL"), DataType(DataType.ImageUrl)]
        public string PictureUrl { get; set; }



        [ForeignKey("Program")]
        public int? ProgramId { get; set; }
        public virtual Program Program { get; set; }



        [ForeignKey("ProductCategory")]
        public int? ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }



        [ForeignKey("ProductSubCategory")]
        public int? ProductSubCategoryId { get; set; }
        public  ProductSubCategory ProductSubCategory { get; set; }

        [ForeignKey("Discount")]
        public int? DiscountId { get; set; }
        public virtual Discount Discount { get; set; }

        public int ViewCount { get; set; }

        public int CartAddCount { get; set; }

        [Range(0, 5)]
        public double AverageRating { get; set; }
        public int TotalRatings { get; set; }

        // Navigation property for reviews
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();


        public virtual ICollection<Inventory> Inventories { get; set; }=new List<Inventory>();
        public ICollection<OrderItem> OrderItems { get; internal set; }=new List<OrderItem>();
    }


}