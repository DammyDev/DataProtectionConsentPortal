﻿using System.ComponentModel.DataAnnotations;

namespace ConsentPortal.Data
{
    public class LoginModel
    {
        [Required]
        
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Display(Name = "Remember me")]
        //public bool RememberMe { get; set; }
    }
}