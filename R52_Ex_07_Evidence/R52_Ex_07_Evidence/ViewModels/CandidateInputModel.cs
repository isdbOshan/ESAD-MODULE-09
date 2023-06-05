using R52_Ex_07_Evidence.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace R52_Ex_07_Evidence.ViewModels
{
    public class CandidateInputModel
    {
        [Key]
        public int CandidateId { get; set; }
        [Required, StringLength(50)]
        public string CandidateName { get; set; }
        [Required, DataType(DataType.Date)]
        public System.DateTime BirthDate { get; set; }
        [Required, StringLength(30)]
        public string AppliedFor { get; set; }
        [Required, Column(TypeName = "money"), DataType(DataType.Currency)]
        public decimal ExpectedSalary { get; set; }
        public bool WorkFromHome { get; set; }
        [Required, StringLength(30)]
        public string Picture { get; set; }
        public virtual List<Qualification> Qualifications { get; set; } = new List<Qualification>();
    }
}