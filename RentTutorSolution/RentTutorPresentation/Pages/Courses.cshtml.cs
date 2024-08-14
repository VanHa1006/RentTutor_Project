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
        private readonly ITutorServices _tutorServicesService;
        public CoursesModel(ICourseServices courseServices, IUserServices tutorServices, ITutorServices tutorServicesService)
        {
            _courseServices = courseServices;
            _tutorServices = tutorServices;
            _tutorServicesService = tutorServicesService;
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
                return result.Data as Paginate<Course> ?? new Paginate<Course>();
            }
            return new Paginate<Course>();
        }

        private async Task<Paginate<User>> GetTutors()
        {
            var result = await _tutorServices.GetAllTutor(PageIndex, Size);
            if (result.Status > 0 && result.Data != null)
            {
                return result.Data as Paginate<User> ?? new Paginate<User>();
            }
            return new Paginate<User>();
        }
        private async Task<Paginate<Course>> Search()
        {
            if (string.IsNullOrEmpty(SearchTerm))
            {
                return await GetCourses(); // Nếu không có từ khóa tìm kiếm, lấy tất cả khóa học
            }
            else
            {
                var result = await _courseServices.Search(SearchTerm, PageIndex, Size);
                if (result.Status > 0 && result.Data != null)
                {
                    return result.Data as Paginate<Course> ?? new Paginate<Course>();
                }
                return new Paginate<Course>();
            }
        }
    

        public async Task OnGetAsync()
        {
            // Log or debug to check the value of SearchTerm
            Console.WriteLine($"SearchTerm: {SearchTerm}");

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Course = await Search();
            }
            else
            {
                Course = await GetCourses();
            }

            Tutor = await GetTutors(); 
            ViewData["SuccessMessage"] = TempData["SuccessMessage"];
        }
    }
}
