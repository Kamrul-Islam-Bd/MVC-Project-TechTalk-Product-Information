using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace TechTalk.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            this.CategoryList = new List<int>();
            this.SubCatList = new List<int>();
            this.ProgramList = new List<int>();
        }

        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        [StringLength(200)]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Product Details")]
        [StringLength(1000)]
        public string ProductDescription { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Stock Count")]
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        [Display(Name = "Picture URL")]
        public string PictureUrl { get; set; }

        [Display(Name = "Picture")]
        public HttpPostedFileBase PictureFile { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int ProductCategoryId { get; set; }


       
        [Display(Name = "Sub Category")]
        public int ProductSubCategoryId { get; set; }

       
        [Display(Name = "Program")]
        public int ProgramId { get; set; }

        public List<int>CategoryList { get; set; }
        public List<int>SubCatList { get; set; }
        public List<int>ProgramList { get; set; }
    }
}
