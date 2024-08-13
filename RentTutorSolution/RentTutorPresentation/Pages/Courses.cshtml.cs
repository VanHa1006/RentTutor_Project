using BusinessAccess.Services;
using DataAccess.Models;
using DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RentTutorPresentation.Pages
{
    public class CoursesModel : PageModel
    {
        private readonly ICourseServices _courseServices;
        private readonly IUserServices _tutorServices;
        private readonly ITutorServices _tutorServicesRepository;
        public CoursesModel(ICourseServices courseServices, IUserServices tutorServices, ITutorServices tutorServicesRepository)
        {
            _courseServices = courseServices;
            _tutorServices = tutorServices;
            _tutorServicesRepository = tutorServicesRepository;
        }
        public Paginate<Course> Course { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public int Size { get; set; } = 10;

        [BindProperty(SupportsGet = true)]
        public Paginate<User> Tutor { get; set; } = default!;
        
        [BindProperty]
        public Course Courses { get; set; } = default!;


        private async Task<Paginate<Course>> GetCourses()
        {
            var result = await _courseServices.GetAll(PageIndex, Size);
            if (result.Status > 0 && result.Data != null)
            {
                var course = result.Data;
                return (Paginate<Course>)course;
            }
            return null;
        }

        private async Task<Paginate<User>> GetTutors()
        {
            var result = await _tutorServices.GetAllTutor(PageIndex, Size);
            if (result.Status > 0 && result.Data != null)
            {

                var students = result.Data;
                return (Paginate<User>)students;
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
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Course = await Search();
            }
            else
            {
                Course = await GetCourses();
                Tutor = await GetTutors();
            }
            ViewData["SuccessMessage"] = TempData["SuccessMessage"];
        }
    }
}
