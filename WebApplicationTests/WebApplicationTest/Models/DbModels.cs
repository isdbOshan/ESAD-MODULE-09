using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplicationTest.Models
{
    public class CarDetail
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
        public ICollection<PartsDetail> PartsDetails { get; set; } = new List<PartsDetail>();

    }
    public class PartsDetail
    {
        public int PartsDetailId { get; set; }
        [Required, StringLength(50)]
        public string PartName { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal PartsPrice { get; set; }
        [Required, ForeignKey("CarDetail")]
        public int CarDetailId { get; set; }
        public CarDetail CarDetail { get; set; }

    }
    public class CarInformationDbContext : DbContext
    {
        public CarInformationDbContext()
        {
            Database.SetInitializer(new DbInitiatializer());
        }
        public DbSet<CarDetail> CarDetails { get; set; }
        public DbSet<PartsDetail> PartsDetails { get; set; }

    }
    public class DbInitiatializer : DropCreateDatabaseIfModelChanges<CarInformationDbContext>
    {
        protected override void Seed(CarInformationDbContext context)
        {
            CarDetail car = new CarDetail
            {
                CarName = "BMW",
                LaunchDate = new DateTime(2019, 2, 2),
                Price = 1800000,
                IsStock = true,
                Picture = "1.jgp"
            };
            car.PartsDetails.Add(new PartsDetail { PartName = "Air Condition", PartsPrice = 1200 });
            car.PartsDetails.Add(new PartsDetail { PartName = "Hydrolic Brake", PartsPrice = 15000 });

            context.CarDetails.Add(car);
            context.SaveChanges();
        }
    }
}