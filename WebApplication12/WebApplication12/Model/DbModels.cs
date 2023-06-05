using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication12.Model
{
    public abstract class ProductBase
    {

        [Key]
        public int ProductId { get; set; }
        [Required, StringLength(50)]
        public string ProductName { get; set; } = default!;
        [Required, DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }
    }
    public class Wearable: ProductBase
    {
        public int Size { get; set; } = default!;
        [Required, StringLength(20)]
        public string Color { get; set; } = default!;
    }
    public class Edible: ProductBase
    {
        public int Quantity { get; set; }
        [Required, StringLength(50)]
        public string Form { get; set; }=default!;
    }
    public class ProductDContext : DbContext
    {
        public ProductDContext()
        {
        }

        public ProductDContext(DbContextOptions<ProductDContext> options) : base(options) { }
        public DbSet<Wearable> Wearables { get; set; } = default!;
        public DbSet<Edible> Edibles { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wearable>().HasData(
                new Wearable {ProductId=1,ProductName="Sunglass",UnitPrice=250.00M, Size=34, Color="Black"},
                new Wearable { ProductId = 2, ProductName = "Cap", UnitPrice = 120.00M, Size = 25, Color = "White" }
                );
            modelBuilder.Entity<Edible>().HasData(new Edible { ProductId = 3, ProductName = "General Water", Quantity = 12, Form = "Drinking" });
        }
    }
    


}
    //public class EmployeeInfo
    //{
    //    [Key,ForeignKey("Employee")]
    //    public int EmployeeId { get; set; }
    //    [Required, StringLength(50)]
    //    public string Gender { get; set; } = default!;
    //    public string MaritalStatus { get; set; } = default!;
    //    [Required, StringLength(50)]
    //    public string Father { get; set; } = default!;
    //    [Required, StringLength(50)]
    //    public string Mother { get; set; } = default!;
    //    [Required, StringLength(50)]
    //    public string Address { get; set; } = default!;
    //    public string HomePhone { get; set; } = default!;
    //    [Required, StringLength(100)]
    //    public string ParmanentAddress { get; set; } = default!;
    //    public virtual Employee Employee { get; set; }= default!;   


    //}