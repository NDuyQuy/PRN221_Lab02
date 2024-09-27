using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageLab02.Model;

namespace RazorPageLab02.Pages
{
    public class CustomerFormModel : PageModel
    {
        public string Message {  get; set; } = string.Empty;
        [BindProperty]
        public Customer customerInfo { get; set; }
        public void OnGet()
        {
            if (ModelState.IsValid)
            {
                Message = "Information is OK";
                ModelState.Clear();
            }
            else
                Message = "Error on input data";
        }
    }
}
