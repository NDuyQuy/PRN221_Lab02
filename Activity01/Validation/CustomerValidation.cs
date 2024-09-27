using System;
using System.ComponentModel.DataAnnotations;
namespace RazorPageLab02.Validation
{
    public class CustomerValidation : ValidationAttribute
    {
        public CustomerValidation()
        {
            ErrorMessage = "The year of birth cannot greater than current year(2024)";
        }

        public override bool IsValid(object? value)
        {
            if (value == null) return false;
            else
            {
                int number = int.Parse(s: value.ToString());
                return (number < 2024);
            }
        }
    }
}
