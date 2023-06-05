using CarDetailsWebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarDetailsWebApi.ViewModels
{
    public class CarDataInputViewModel
    {
        public int CarDetailId { get; set; }
        [Required, StringLength(50)]
        public string CarName { get; set; }
        public DateTime LaunchDate { get; set; } = DateTime.Today;
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
        public bool IsStock { get; set; }
        [Required, StringLength(50)]
        public string Picture { get; set; }
        public  List<PartsDataInputViewModel> PartsDetails { get; set; } = new List<PartsDataInputViewModel>();
    }
}