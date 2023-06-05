using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CarDetailsWebApi.ViewModels;

namespace FinalEvidence.ViewModels
{
    public class CarDataEditViewModel
    {
        public int CarDetailId { get; set; }
        [Required, StringLength(50)]
        public string CarName { get; set; } = default!;
        public DateTime LaunchDate { get; set; } = DateTime.Today;
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
        public bool IsStock { get; set; }
        [Required, StringLength(50)]
        public string Picture { get; set; }= default!;
        public  List<PartsDataInputViewModel> PartsDetails { get; set; } = new List<PartsDataInputViewModel>();
    }
}