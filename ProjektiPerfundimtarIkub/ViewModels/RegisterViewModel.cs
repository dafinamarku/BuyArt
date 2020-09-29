using BuyArt.DomainModels.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektiPerfundimtarIkub.ViewModels
{
  public class RegisterViewModel
  {
    [Required]
    [RegularExpression("^[A-Za-z0-9]*$", ErrorMessage ="{0} must contain only letters and digits.")]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
    public string ConfirmPassword { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [MaxWords(100, ErrorMessage ="{0} can not have more than 100 words.")]
    public string Bio { get; set; }
    
    [Required]
    [Display(Name ="Profie Type")]
    public string ProfileType { get; set; }
  }
}