using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Seguro.Model.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class IdentificationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && value.ToString().Length > 0)
            { }
            else
            {
                return new ValidationResult("Error al ingresar identificacion");
            }
            Regex regex = new Regex(@"^[0-9]+$");
            if (!regex.IsMatch(value.ToString()))
            {
                return new ValidationResult("Error Valores ingresados no Validos");
            }
            return ValidationResult.Success;
        }
    }
}