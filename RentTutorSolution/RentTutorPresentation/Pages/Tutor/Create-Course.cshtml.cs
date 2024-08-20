using BusinessAccess.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RentTutorPresentation.Pages.Tutor
{
    public class Create_CourseModel : PageModel
    {
        private readonly ICourseServices _courseServices;
        private readonly ICategoryServices _categoryServices;

        public Create_CourseModel(ICourseServices courseServices, ICategoryServices categoryServices)
        {
            _courseServices = courseServices;
            _categoryServices = categoryServices;
        }

        public async Task<IActionResult> OnGet()
        {
            var courseCate = await _categoryServices.GetAllCategories();
            ViewData["CategoryId"] = new SelectList((System.Collections.IEnumerable)courseCate.Data, "CategoryId", "CategoryName");
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            int? tutorId = HttpContext.Session.GetInt32("TutorId");
            Course.TutorId = tutorId ?? 0;
            Course.CreatedAt = DateTime.Now;
            Course.UpdatedAt = DateTime.Now;
            Course.Status = "Active";
            var result = await _courseServices.Save(Course);
            if (result != null)
            {
                ViewData["SuccessMessage"] = result.Message;
                return RedirectToPage("./Courses-manage");
            }
            else
            {
                ViewData["ErrorMessage"] = $"Error: {result.Message}";
                return Page();
            }
        }
    }
}
