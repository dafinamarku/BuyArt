using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuyArt.DomainModels.CustomValidators
{
  public class MaxWords:ValidationAttribute
  {
    int max { get; set; }
    public MaxWords(int nr)
    {
      max = nr;
    }
    //nuk lejon qe nr i fjaleve ne nje tekst te jete me i madh se nje nr i caktuar
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      string text = Convert.ToString(value);
      string[] words = text.Split(' ');

      if (words.Length > max)
        return new ValidationResult(this.ErrorMessage);

      return ValidationResult.Success;
    }
  }
}