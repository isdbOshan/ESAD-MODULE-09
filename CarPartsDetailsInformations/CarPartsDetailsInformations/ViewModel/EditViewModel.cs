using CarPartsDetailsInformations.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarPartsDetailsInformations.ViewModel
{
    public class EditViewModel
    {
        [Key]
        public int CategoryId { get; set; }
        [Required, StringLength(50)]
        public string CategoryName { get; set; }
        [Required, StringLength(100)]
        public string Description { get; set; }
        public virtual List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}