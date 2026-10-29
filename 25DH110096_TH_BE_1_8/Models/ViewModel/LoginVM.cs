using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _25DH110096_TH_BE_1_8.Models.ViewModel
{
    public class LoginVM
    {
        [Required]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}