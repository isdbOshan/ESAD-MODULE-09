using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace R52_M12_Class_05_Work_01.ViewModels
{
    public class SpecInputModel
    {
        [Required, StringLength(30)]
        public string SpecName { get; set; }=default!;
        [Required, StringLength(50)]
        public string Value { get; set; } = default!;
    }
}