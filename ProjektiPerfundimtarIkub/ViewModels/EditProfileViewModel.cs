using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektiPerfundimtarIkub.ViewModels
{
  public class EditProfileViewModel
  {
    [Required]
    [RegularExpression("^[A-Za-z0-9]*$", ErrorMessage = "{0} must contain only letters and digits.")]
    public string Username { get; set; }

    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
    public string ConfirmPassword { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string Bio { get; set; }
  }
}