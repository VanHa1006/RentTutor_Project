using BusinessAccess.Services;
using DataAccess.Models;
using DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RentTutorPresentation.Pages.Admin
{
    public class StudentManagerModel : PageModel
    {
        private readonly IUserServices _studentServices;

        public StudentManagerModel(IUserServices studentServices)
        {
            _studentServices = studentServices;
        }

        public string Message { get; set; } = default!;
        public Paginate<User> Student { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public int Size { get; set; } = 10;

        private async Task<Paginate<User>> GetStudents()
        {
            var result = await _studentServices.GetAllStudents(PageIndex, Size);
            if (result.Status > 0 && result.Data != null)
            {
                var students = result.Data;
                return (Paginate<User>)students;
            }
            return null;
        }

        private async Task<Paginate<User>> Search()
        {
            var result = await _studentServices.Search(SearchTerm, PageIndex, Size);
            if (result.Status > 0 && result.Data != null)
            {
                var customer = result.Data;
                return (Paginate<User>)customer;
            }
            return null;
        }
        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Student = await Search();
            }
            else
            {
                Student = await GetStudents();
            }
        }
    }
}

