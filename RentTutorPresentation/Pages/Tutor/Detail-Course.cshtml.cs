using BusinessAccess.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RentTutorPresentation.Pages.Tutor
{
    public class Detail_CourseModel : PageModel
    {
        private readonly ICourseServices _courseServices;
        private readonly ICategoryServices _categoryServices;

        public Detail_CourseModel(ICourseServices courseServices, ICategoryServices categoryServices)
        {
            _courseServices = courseServices;
            _categoryServices = categoryServices;
        }

        [BindProperty]
        public Course Course { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseServices.GetById((int)id);
            if (course == null)
            {
                return NotFound();
            }
            Course = (Course)course.Data;
            var courseCate = await _categoryServices.GetAllCategories();
            ViewData["CategoryId"] = new SelectList((System.Collections.IEnumerable)courseCate.Data, "CategoryId", "CategoryName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                int? tutorId = HttpContext.Session.GetInt32("TutorId");
                Course.TutorId = tutorId ?? 0;
                Course.UpdatedAt = DateTime.Now;
                var result = await _courseServices.UpdateAsync(Course);
                if (result.Status > 0)
                {
                    TempData["SuccessMessage"] = result.Message;
                    return RedirectToPage("./Courses-manage");
                }
                else
                {
                    TempData["ErrorMessage"] = result.Message;
                    return Page();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_courseServices.FindId(Course.CourseId) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
