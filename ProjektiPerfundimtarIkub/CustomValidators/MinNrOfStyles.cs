using BuyArt.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektiPerfundimtarIkub.CustomValidators
{
  public class MinNrOfStyles:ValidationAttribute
  {
    int min;

    public MinNrOfStyles(int nr)
    {
      this.min = nr;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      List<Style> currentList = (List<Style>)value;
      if (currentList.Where(x => x.IsSelected == true).Count() < min)
        return new ValidationResult(this.ErrorMessage);
      else
        return ValidationResult.Success;
    }
  }
}