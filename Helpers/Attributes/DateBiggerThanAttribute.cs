using System.ComponentModel.DataAnnotations;

namespace TestApplication.Helpers.Attributes
{
    public class DateBiggerThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateBiggerThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var currentValue = (DateTime?)value;

            if (currentValue is null)
                return new ValidationResult("Дата должна быть указана.");

            var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (comparisonProperty is null)
                throw new ArgumentException($"Property with this name '{_comparisonProperty}' not found.");

            var comparisonValue = (DateTime?)comparisonProperty.GetValue(validationContext.ObjectInstance);
            if (currentValue <= DateTime.Now)
                return new ValidationResult("Дата должна быть больше текущей даты.");

            if (comparisonValue is not null && currentValue <= comparisonValue)
                return new ValidationResult($"Дата должна быть больше, чем ({comparisonValue.Value.ToShortDateString()}).");

            return ValidationResult.Success;
        }
    }
}
