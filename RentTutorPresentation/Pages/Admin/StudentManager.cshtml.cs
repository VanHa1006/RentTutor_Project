using BusinessAccess.Services;
using DataAccess.Models;
using DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
        public int Size { get; set; } = 100;

        [BindProperty(SupportsGet = true)]
        public string SortStatus { get; set; }

        private async Task<Paginate<User>> GetStudents()
        {
            var result = await _studentServices.GetAllStudents(PageIndex, Size);

            if (result.Status > 0 && result.Data != null)
            {
                return (Paginate<User>)result.Data;
            }
            return null;
        }

        private async Task<Paginate<User>> GetStatusStudents()
        {
            var result = await _studentServices.GetAllStudentsByStatus(SortStatus, PageIndex, Size);

            if (result.Status > 0 && result.Data != null)
            {
                return (Paginate<User>)result.Data;
            }
            return null;
        }

        private async Task<Paginate<User>> Search()
        {
            var result = await _studentServices.Search(SearchTerm, PageIndex, Size);
            if (result.Status > 0 && result.Data != null)
            {
                return (Paginate<User>)result.Data;
            }
            return null;
        }

        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Student = await Search();
            }
            else if (!string.IsNullOrEmpty(SortStatus))
            {
                Student = await GetStatusStudents();
            }
            else
            {
                Student = await GetStudents();
            }
        }
    }
}

