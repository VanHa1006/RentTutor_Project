using BusinessAccess.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RentTutorPresentation.Pages.Tutor
{
    public class Delete_CourseModel : PageModel
    {
        private readonly ICourseServices _courseServices;

        public Delete_CourseModel(ICourseServices courseServices)
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

            var course = await _courseServices.DeleteAsync(id);

            if (course.Status > 0)
            {
                ViewData["SuccessMessage"] = course.Message;
                return RedirectToPage("./Courses-manage");
            }
            else
            {
                TempData["ErrorMessage"] = course.Message;
                return RedirectToPage("./Courses-manage");
            }
        }
    }
}
