using BusinessAccess.Services;
using DataAccess.Models;
using DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace RentTutorPresentation.Pages
{
    public class CoursesDetailModel : PageModel
    {
        private readonly ICourseServices _courseServices;

        public CoursesDetailModel(ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }
        [BindProperty]
        public Course Course { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseServices.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            Course = course.Data as Course;

            // Get Tutor information
            var courseResult = await _courseServices.GetById(id);
            if (courseResult == null)
            {
                return NotFound();
            }
            Course = courseResult.Data as DataAccess.Models.Course;

            return Page();
        }
    }
}
