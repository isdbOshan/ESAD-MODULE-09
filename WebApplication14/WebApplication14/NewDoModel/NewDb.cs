using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication14.Controllers;
using WebApplication14.Models;

namespace WebApplication14.NewDoModel
{
    public class NewDb
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
            public HttpPostedFileBase Picture { get; set; }
            public virtual ICollection<PartDetails> PartDetail { get; set; } = new List<PartDetails>();
        }

        public class PartDetails: CarDetail
        {
            
           
            [Required, StringLength(50)]
            public string PartName { get; set; }
            public string PartType { get; set; }
            //[Required, ForeignKey("CarDetail")]
            //public int CarId { get; set; }
            public virtual CarDetail CarDetail { get; set; }

        }
        public class CarDbContextInforamtions : DbContext
        {
            public DbSet<CarDetail> CarDetails { get; set; }
            public DbSet<PartDetails> PartDetails { get; set; }
        }
    }
}