using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuyArt.DomainModels.CustomValidators
{
  public class MinValue:ValidationAttribute
  {
    double compare;
    public MinValue(double x)
    {
      compare = x;
    }
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      double nr = Convert.ToDouble(value);
      if (nr <= compare)
        return new ValidationResult(this.ErrorMessage);
      return ValidationResult.Success;
    }
  }
}