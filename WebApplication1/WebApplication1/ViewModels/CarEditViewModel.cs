using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class CarEditViewModel
    {
        [Key]
        public int CarId { get; set; }
        [Required, StringLength(100)]
        public string CarName { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public bool IsStock { get; set; }
        public string Picture { get; set; }
        public List<PartDetail> PartDetail { get; set; } = new List<PartDetail>();
    }
}