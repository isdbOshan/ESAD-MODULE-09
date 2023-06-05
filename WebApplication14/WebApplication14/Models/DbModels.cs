using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication14.Models
{
    public class CarDetail
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
        public virtual ICollection<PartDetails> PartDetail { get; set; } = new List<PartDetails>();


    }

    public class PartDetails
    {
        [Key]
        public int PartId { get; set; }
        public string PartName { get; set; }
        public string PartType { get; set; }
        [Required, ForeignKey("CarDetail")]
        public int CarId { get; set; }
        public virtual CarDetail CarDetail { get; set; }

    }
    public class CarDbContextInforamtions : DbContext
    {
        public DbSet<CarDetail> CarDetails { get; set; }
        public DbSet<PartDetails> PartDetails { get; set; }

        public System.Data.Entity.DbSet<WebApplication14.ViewModel.CarInputViewM> CarInputViewMs { get; set; }
    }
}