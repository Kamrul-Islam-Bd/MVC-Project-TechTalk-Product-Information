using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechTalk.ViewModels
{
    public class ProductCategory
    {
        [Key]
        public int ProductCategoryId { get; set; }

        [Required, Display(Name = "Category"), StringLength(100)]
        public string ProCatName { get; set; }

        public virtual ICollection<ProductSubCategory> ProductSubCategories { get; set; }=new List<ProductSubCategory>();
        public virtual ICollection<Product> Products { get; set; }=new List<Product>();
        public virtual ICollection<Program> Programs { get; set; }=new List<Program>();

    }


}