using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace R52_Mid_09_Evidence.ViewModels
{
    public class SpecInputModel
    {
        [Required, StringLength(30)]
        public string SpecName { get; set; }
        [Required, StringLength(50)]
        public string Value { get; set; }
    }
}