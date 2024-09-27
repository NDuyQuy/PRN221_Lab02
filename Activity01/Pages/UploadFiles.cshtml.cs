using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;
namespace RazorPageLab02.Pages
{
    public class UploadFilesModel : PageModel
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public UploadFilesModel(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _environment = environment;
        }

        [Required(ErrorMessage ="Please choose at least one file")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions ="png,jpg,jpeg,gif")]
        [Display(Name ="Choose file(s) to upload")]
        [BindProperty]
        public IFormFile[] FileUploads { get; set; }

        public void OnGet()
        {
        }
        public async Task OnPostAsync()
        {
            if (FileUploads != null)
            {
                foreach (var FileUpload in FileUploads)
                {
                    var file = Path.Combine(_environment.ContentRootPath,"Images", FileUpload.FileName);
                    using(var fileStream = new FileStream(file,FileMode.Create))
                    {
                        await FileUpload.CopyToAsync(fileStream);
                    }
                }
            }
        }
    }
}
