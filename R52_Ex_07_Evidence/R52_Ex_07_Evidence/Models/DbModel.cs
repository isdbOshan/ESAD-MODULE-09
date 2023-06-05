using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace R52_Ex_07_Evidence.Models
{
    public class Candidate
    {
        public int CandidateId { get; set; }
        [Required, StringLength(50)]
        public string CandidateName { get; set; }
        [Required, DataType(DataType.Date)]
        public System.DateTime BirthDate { get; set; }
        [Required, StringLength(30)]
        public string AppliedFor { get; set; }
        [Required, Column(TypeName ="money"), DataType(DataType.Currency)]
        public decimal ExpectedSalary { get; set; }
        public bool WorkFromHome { get; set; }
        [Required, StringLength(30)]
        public string Picture { get; set; }
        public virtual ICollection<Qualification> Qualifications { get; set; } = new List<Qualification>();
    }
    public class Qualification
    {
        public int QualificationId { get; set; }
        [Required, StringLength(50)]
        public string Degree { get; set; }
        [Required]
        public int PassingYear { get; set; }
        [Required, StringLength(50)]
        public string Institute { get; set; }
        [Required, StringLength(20)]
        public string Result { get; set; }
        [Required, ForeignKey("Candidate")]
        public int CandidateId { get; set; }
        public virtual Candidate Candidate { get; set; }
    }
    public class CandidateDbContext : DbContext
    {
        public CandidateDbContext()
        {
            Database.SetInitializer(new DbInitializer());
        }
        public DbSet<Candidate> Candidates { get; set;}
        public DbSet<Qualification> Qualifications { get; set; }
    }
    public class DbInitializer:DropCreateDatabaseIfModelChanges<CandidateDbContext>
    {
        protected override void Seed(CandidateDbContext context)
        {
            Candidate c = new Candidate
            {
                CandidateName = "Candidate 1",
                AppliedFor = "Executive",
                BirthDate = new DateTime(1999, 4, 1),
                ExpectedSalary = 10000.00M,
                Picture = "1.jpg",
                WorkFromHome = true
            };
            c.Qualifications.Add(new Qualification { Degree = "HSC", PassingYear = 2017, Institute = "Some School", Result = "3.4/5" });
            context.Candidates.Add(c);
        }
    }
}