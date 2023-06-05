using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace R52_M12_Class_05_Work_01.ViewModels.Identity
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; } = default!;


        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; } = default!;
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }=default!;
    }
}
