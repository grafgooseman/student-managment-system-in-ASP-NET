using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AG_MT2.Models
{
    public class CustomDateRegexAttribute : ValidationAttribute
    {
        private readonly string _pattern;
        private readonly DateTime _minDate;
        private readonly DateTime _maxDate;

        public CustomDateRegexAttribute(string pattern, string minDate, string maxDate)
        {
            _pattern = pattern;
            _minDate = DateTime.ParseExact(minDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            _maxDate = DateTime.ParseExact(maxDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (!(value is DateTime dateValue))
            {
                return new ValidationResult("Invalid date format");
            }

            string dateString = dateValue.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            if (!Regex.IsMatch(dateString, _pattern))
            {
                return new ValidationResult("Invalid date format");
            }

            if (dateValue < _minDate || dateValue > _maxDate)
            {
                return new ValidationResult($"Date of Birth must be between {_minDate:MM/dd/yyyy} and {_maxDate:MM/dd/yyyy}");
            }

            return ValidationResult.Success;
        }
    }
}


