using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace R52_M12_Class_05_Work_01.Models
{
    public class Device
    {
        public int DeviceId { get; set; }
        [Required, StringLength(50)]
        public string DeviceName { get; set; } = default!;
        [Required, Column(TypeName = "date")]
        public DateTime ReleaseDate { get; set; } = DateTime.Today;
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
        public bool OnSale { get; set; }
        [Required, StringLength(30)]
        public string Picture { get; set; } = default!;
        public virtual ICollection<Spec> Specs { get; set; } = new List<Spec>();
    }
    public class Spec
    {
        public int SpecId { get; set; }
        [Required, StringLength(30)]
        public string SpecName { get; set; } = default!;
        [Required, StringLength(50)]
        public string Value { get; set; } = default!;
        [Required, ForeignKey("Device")]
        public int DeviceId { get; set; }
        public virtual Device Device { get; set; } = default!;
    }
    public class DeviceDbContext : DbContext
    {
        public DeviceDbContext(DbContextOptions<DeviceDbContext> options) : base(options) { }
        public DbSet<Device> Devices { get; set; } = default!;
        public DbSet<Spec> Specs { get; set; } = default!;
    }
}
