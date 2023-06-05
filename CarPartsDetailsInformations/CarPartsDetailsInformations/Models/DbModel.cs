using Microsoft.Owin.BuilderProperties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace CarPartsDetailsInformations.Models
{
    public class Categorie
    {
        [Key] 
        public int CategoryId { get; set; }
        [Required, StringLength(50)]
        public string CategoryName { get; set; }
        [Required, StringLength(100)]
        public string Description { get; set; }
        public virtual List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
     
    }
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }
        [Required, StringLength(50)]
        public string VehicleName { get; set; }
        [Required, Column(TypeName = "money"), DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required]
        public int MakeYear { get; set; }
        public bool IsStock { get; set; }
        [Required, StringLength(50)]
        public string Picture { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Categorie Categories { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

    public class Customer
    {
      
        public int CustomerId { get; set; }
        [Required, StringLength(50)]
        public string CustomerName { get; set; }
        [Required, DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required,StringLength(100)]
        public string Address { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual List<Order> Orders { get; set; }= new List<Order>();
    }
    public class Order
    {
       
        public int OrderId { get; set; }    
        public string OrderName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customers { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
    public class OrderItem
    {
        
        public int OrderItemId { get; set; }
        public string OrderName { get; set; }
        public int Quantity { get; set; }
        public int VehicleId { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Orders { get; set; }
        [ForeignKey("VehicleId")]
        public virtual Vehicle Vehicles { get; set; }
    }
    public class VehicleInformationDbContext: DbContext
    {
       
        public DbSet<Categorie> Categories { get; set; }   
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Customer> Customers { get; set; } 
        public DbSet<Order> Orders { get; set; }   
        public DbSet<OrderItem> OrdersItems { get; set;}
    }


}

