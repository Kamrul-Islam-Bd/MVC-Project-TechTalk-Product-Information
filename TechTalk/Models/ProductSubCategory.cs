using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TechTalk.ViewModels
{
    public class ProductSubCategory
    {
        [Key]
        public int ProductSubCategoryId { get; set; }



        [Required, Display(Name = "Sub Category"), StringLength(100)]
        public string ProSubCatName { get; set; }



        [ForeignKey("ProductCategory")]
        public int ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }



        public virtual ICollection<Product> Products { get; set; }=new List<Product>();
        public virtual ICollection<Program> Programs { get; set; }=new List<Program>();
    }


}