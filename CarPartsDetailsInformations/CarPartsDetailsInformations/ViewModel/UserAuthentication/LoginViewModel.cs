using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarPartsDetailsInformations.ViewModel.UserAuthentication
{
    public class LoginViewModel
    {
        [Required, StringLength(15)]
        public  string UserName { get; set; }
        [Required, StringLength(20, MinimumLength = 6), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}