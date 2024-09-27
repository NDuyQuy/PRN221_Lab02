using Microsoft.AspNetCore.Mvc;
using RazorPageLab02.Biding;
using System.ComponentModel.DataAnnotations;

namespace RazorPageLab02.Model
{
    public class Customer
    {
        [Required(ErrorMessage = "Customer name is required!")]
        [StringLength(20,MinimumLength =3,ErrorMessage ="The length of name is from 3 to 20 characters ")]
        [Display(Name = "Customer name")]
        [ModelBinder(typeof(CheckNameBiding))]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Customer Password is required!")]
        [EmailAddress]
        [Display(Name = "Customer email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage ="Year of birth is required!")]
        [Display(Name ="Year of birth")]
        [Range(1960,2000,ErrorMessage ="1960-2000")]
        public int? YearOfBirth { get; set; }
    }
}
