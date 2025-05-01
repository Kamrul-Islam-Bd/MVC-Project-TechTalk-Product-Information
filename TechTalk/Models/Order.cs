using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TechTalk.ViewModels
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required, Display(Name = "Order Number"), StringLength(50)]
        public string OrderNo { get; set; }

        [Required, Display(Name = "Order Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Is Paid")]
        public bool IsPaid { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }=new List<OrderItem>();
    }

}