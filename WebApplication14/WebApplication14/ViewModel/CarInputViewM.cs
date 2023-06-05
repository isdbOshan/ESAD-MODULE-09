using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication14.Models;

namespace WebApplication14.ViewModel
{
    public class CarInputViewM
    {
        [Key]
        public int CarId { get; set; }
        [Required, StringLength(50)]
        public string CarName { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime MakeYear { get; set; }
        public bool IsStock { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required, StringLength(50)]
        public string Picture { get; set; }
        public virtual List<PartDetails> PartDetail { get; set; } = new List<PartDetails>();
    }
}