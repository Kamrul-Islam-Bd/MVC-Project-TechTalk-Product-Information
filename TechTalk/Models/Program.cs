using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechTalk.ViewModels
{
    public class Program
    {
        [Key]
        public int ProgramId { get; set; }

        [Required, Display(Name = "Program Name"), StringLength(100)]
        public string ProgramName { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }


}