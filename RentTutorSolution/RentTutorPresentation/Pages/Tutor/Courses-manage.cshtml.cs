using BusinessAccess.Services;
using DataAccess.Models;
using DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RentTutorPresentation.Pages.Tutor
{
    public class Courses_manageModel : PageModel
    {
        private readonly ICourseServices _courseServices;

        public Courses_manageModel(ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }
        public Paginate<Course> Course { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public int Size { get; set; } = 10;
        public int? TutorId { get; set; }

        private async Task<Paginate<Course>> GetCourses()
        {
            var result = await _courseServices.GetAllCourseToTutor(PageIndex, Size, TutorId);
            if (result.Status > 0 && result.Data != null)
            {
               return (Paginate<Course>)result.Data;
            }
            return null;
        }
        private async Task<Paginate<Course>> Search()
        {
            var result = await _courseServices.Search(SearchTerm, PageIndex, Size);
            if (result.Status > 0 && result.Data != null)
            {
                var course = result.Data;
                return (Paginate<Course>)course;
            }
            return null;
        }
        public async Task OnGetAsync()
        {
            TutorId = HttpContext.Session.GetInt32("TutorId");
            if (!TutorId.HasValue)
            {
                // Xử lý trường hợp tutorId không có trong session
            }

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Course = await Search();
            }
            else
            {
                Course = await GetCourses();
            }

            ViewData["SuccessMessage"] = TempData["SuccessMessage"];
        }
    }
}
