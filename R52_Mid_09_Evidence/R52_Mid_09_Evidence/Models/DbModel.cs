using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace R52_Mid_09_Evidence.Models
{
    public class Device
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
        public  ICollection<Spec> Specs { get; set; } = new List<Spec>();
    }
    public class Spec
    {
        public int SpecId { get; set; }
        [Required, StringLength(30)]
        public string SpecName { get; set; } 
        [Required, StringLength(50)]
        public string Value { get; set; } 
        [Required, ForeignKey("Device")]
        public int DeviceId { get; set; }
        public   Device Device { get; set; }
    }
    public class DeviceDbContext : DbContext
    {
       public DeviceDbContext()
        {
            Database.SetInitializer(new DbInitiatializer());
        }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Spec> Specs { get; set; } 
    }
    public class DbInitiatializer : DropCreateDatabaseIfModelChanges<DeviceDbContext>
    {
        protected override void Seed(DeviceDbContext context)
        {
            Device d = new Device { DeviceName = "S23 Ultra", ReleaseDate = new DateTime(2923, 2, 1), OnSale = true, Picture = "1.jpg", Price = 120000 };
            d.Specs.Add(new Spec { SpecName = "RAM", Value = "12GB" });
            d.Specs.Add(new Spec { SpecName = "Storage", Value = "128GB" });
            context.Devices.Add(d);
            context.SaveChanges();

        }
    }
}