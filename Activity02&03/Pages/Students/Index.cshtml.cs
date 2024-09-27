using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesLab.Models;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesLab.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesLab.Models.SchoolContext _context;
        private readonly IConfiguration Configuration;

        public string NameSort { get; set; } = string.Empty;
        public string DateSort { get; set; } = string.Empty;
        public string CurrentFilter { get; set; } = string.Empty;
        public string CurrentSort { get; set; } = string.Empty;
        public PaginatedList<Student> Students { get; set; }

        public bool IsSearching = false;

        public IndexModel(SchoolContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public IList<Student> Student { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder,string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = (sortOrder == "Date") ? "date_desc" : "Date";

            if (searchString != null) pageIndex = 1;
            else searchString = currentFilter;
            CurrentFilter = searchString;
            IQueryable<Student> studentsIQ = from s in _context.Students select s;
            if (!String.IsNullOrEmpty(searchString))
                studentsIQ = from student in studentsIQ
                             where student.FirstMidName.Contains(searchString) || student.LastName.Contains(searchString)
                             select student;
            switch(sortOrder)
            {
                case "name_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s=> s.LastName);
                    break;
                case "Date":
                    studentsIQ = studentsIQ.OrderBy(s=>s.EnrollmentDate);
                    break;
                case "date_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default: 
                    studentsIQ = studentsIQ.OrderBy(s=>s.LastName);
                    break;
            }
            var pageSize = Configuration.GetValue("PageSize", 4);
            Students = await PaginatedList<Student>.CreateAsync(studentsIQ.AsNoTracking(),pageIndex??1,pageSize);
        }
    }
}
