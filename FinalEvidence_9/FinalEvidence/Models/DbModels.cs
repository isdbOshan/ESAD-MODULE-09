using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace FinalEvidence.Models
{
    public class CarDetail
    {

        public int CarDetailId { get; set; }
        [Required, StringLength(50)]
        public string CarName { get; set; } = default!;
        public DateTime LaunchDate { get; set; } = DateTime.Today;
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
        public bool IsStock { get; set; }
        [Required, StringLength(50)]
        public string Picture { get; set; } = default!;
        public ICollection<PartsDetail> PartsDetails { get; set; } = new List<PartsDetail>();

    }
    public class PartsDetail
    {
        public int PartsDetailId { get; set; }
        [Required, StringLength(50)]
        public string PartName { get; set; } = default!;
        [Required, Column(TypeName = "money")]
        public decimal PartsPrice { get; set; }
        [Required, ForeignKey("CarDetail")]
        public int CarDetailId { get; set; }
        public CarDetail CarDetail { get; set; } = default!;

    }

    public class CarInformationDbContext : DbContext
    {
        public CarInformationDbContext(DbContextOptions<CarInformationDbContext> options) : base(options) { }
        public DbSet<CarDetail> CarDetails { get; set; } = default!;
        public DbSet<PartsDetail> PartsDetails { get; set; } = default!;
    }

}