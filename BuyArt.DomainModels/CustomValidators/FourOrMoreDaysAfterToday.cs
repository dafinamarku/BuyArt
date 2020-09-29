using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyArt.DomainModels.CustomValidators
{
  class FourOrMoreDaysAfterToday:ValidationAttribute
  {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      if (value == null)
      {
        return new ValidationResult(this.ErrorMessage);
      }
      DateTime date = (DateTime)value;
      if (date < DateTime.Now.AddDays(4))
        return new ValidationResult(this.ErrorMessage);
      else
        return ValidationResult.Success;
    }
  }
}
