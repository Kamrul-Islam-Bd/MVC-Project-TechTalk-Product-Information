using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechTalk.ViewModels
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required, Display(Name = "Customer Name"), StringLength(100)]
        public string CustomerName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Mobile No"), Phone]
        public string MobileNo { get; set; }
        public ICollection<Order> Orders { get; set; }=new List<Order>();
    }

}