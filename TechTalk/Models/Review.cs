using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TechTalk.ViewModels;

namespace TechTalk.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [Required, Display(Name = "Reviewer Name"), StringLength(100)]
        public string ReviewerName { get; set; }

        [Required, Display(Name = "Review Content"), StringLength(1000)]
        public string Content { get; set; }

        [Required, Range(0, 5), Display(Name = "Rating")]
        public int Rating { get; set; }

        [Required, ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null;
    }
}