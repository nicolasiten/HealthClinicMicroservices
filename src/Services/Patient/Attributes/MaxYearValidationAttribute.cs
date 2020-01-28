using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Patient.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxDateTodayValidationAttribute : ValidationAttribute
    {
        private readonly DateTime _maxDate;

        public MaxDateTodayValidationAttribute()
        {
            _maxDate = DateTime.Today;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validation)
        {
            DateTime dateValue = (DateTime)value;

            if (dateValue <= _maxDate)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Date Of Birth can't be in the future");
        }
    }
}
