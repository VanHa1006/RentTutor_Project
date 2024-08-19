using BusinessAccess.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RentTutorPresentation.Pages.Admin
{
    public class Courses_DetailModel : PageModel
    {
        private readonly ICourseServices _courseServices;
        private readonly ICategoryServices _categoryServices;
        private readonly ITutorServices _tutorServices;
        private readonly IUserServices _userServices;

        public Courses_DetailModel(ICourseServices courseServices, ICategoryServices categoryServices, ITutorServices tutorServices, IUserServices userServices)
        {
            _courseServices = courseServices;
            _categoryServices = categoryServices;
            _tutorServices = tutorServices;
            _userServices = userServices;
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
            var courseTutor = await _tutorServices.GetAllTutors();
            var userCourse = await _userServices.GetAllUsers();
            ViewData["CoursesCategoryId"] = new SelectList((System.Collections.IEnumerable)courseCate.Data, "CoursesCategoryId");
            ViewData["CoursesTutorId"] = new SelectList((System.Collections.IEnumerable)courseTutor.Data, "CoursesTutorId");
            ViewData["CoursesUserId"] = new SelectList((System.Collections.IEnumerable)userCourse.Data, "CoursesUserId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var result = await _courseServices.UpdateAsync(Course);
                if (result.Status > 0)
                {
                    TempData["SuccessMessage"] = result.Message;
                    return RedirectToPage("./CourseManage");
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
