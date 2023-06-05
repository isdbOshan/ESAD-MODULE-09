
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using R52_Mid_09_Evidence.Models;
using System.Collections.Generic;

namespace R52_Mid_09_Evidence.ViewModels
{
    public class DeviceInputModel
    {
        public int DeviceId { get; set; }
        [Required, StringLength(50)]
        public string DeviceName { get; set; } 
        [Required, Column(TypeName = "date")]
        public DateTime ReleaseDate { get; set; } = DateTime.Today;
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
        public bool OnSale { get; set; }
        [Required, StringLength(30)]
        public string Picture { get; set; }
        public List<SpecInputModel> Specs { get; set; } = new List<SpecInputModel>();
    }
}
