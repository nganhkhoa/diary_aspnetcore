using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace diary.Models.AccountViewModels
{

      public class LoginViewModel
      {
            [Required]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            public bool RememberMe { get; set; }
      }

      public class RegisterViewModel
      {
            [Required]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Password not match")]
            public string RetypePassword { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }
      }

      public class LoginRegisterModel
      {
            public LoginViewModel login { get; set; }
            public RegisterViewModel register { get; set; }
      }
}