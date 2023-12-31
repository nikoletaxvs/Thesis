﻿using System.ComponentModel.DataAnnotations;

namespace ThesisOct2023.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //[Display(Name = "Remember me")]
        //public bool RememberMe { get; set; }
    }

}
