
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using R52_M12_Class_05_Work_01.Models;
using System.Collections.Generic;

namespace R52_M12_Class_05_Work_01.ViewModels
{
    public class DeviceEditModel
    {
        public int DeviceId { get; set; }
        [Required, StringLength(50)]
        public string DeviceName { get; set; } = default!;
        [Required, Column(TypeName = "date")]
        public DateTime ReleaseDate { get; set; } = DateTime.Today;
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
        public bool OnSale { get; set; }
        [StringLength(30)]
        public string Picture { get; set; } = default!;
        public List<SpecInputModel> Specs { get; set; } = new List<SpecInputModel>();
    }
}
