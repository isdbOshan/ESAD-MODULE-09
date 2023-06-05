using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CarDetail
    {
        [Key]
        public int CarId { get; set; }
        [Required, StringLength(100)]
        public string CarName { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public bool IsStock { get; set; }
        [Required, StringLength(100)]
        public string Picture { get; set; }
        public ICollection<PartDetail> PartDetail { get; set; } = new List<PartDetail>();
    }
    public class PartDetail
    {
        [Key]
        public int PartId { get; set; }
        public int Door { get; set; }
        [Required, StringLength(100)]
        public string EngineType { get; set; }
        [Required,ForeignKey("CarDetail")]
        public int CarId { get; set; }
        public virtual CarDetail CarDetail { get; set; }= new CarDetail();
    }

    public class VehicleDbContexts : DbContext
    {
        public DbSet<CarDetail> CarDetails { get; set; }
        public DbSet<PartDetail> PartDetail { get; set; }   
     }
}