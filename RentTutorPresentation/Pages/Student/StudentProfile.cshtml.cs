using BusinessAccess.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RentTutorPresentation.Pages.Student
{
    public class StudentProfileModel : PageModel
    {
        private readonly IUserServices _studentServices;

        public StudentProfileModel(IUserServices studentServices)
        {
            _studentServices = studentServices;
        }

        [BindProperty]
        public User Student { get; set; } = default!;

        // Method to load student data by ID, either from session or passed as a route parameter
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            int userId;

            if (id.HasValue)
            {
                // Use the ID passed via the URL route
                userId = id.Value;
            }
            else
            {
                // Fall back to the session-stored StudentId if no ID is provided in the route
                userId = HttpContext.Session.GetInt32("StudentId") ?? 0;
            }

            // If no valid ID is found, redirect to the login page
            if (userId == 0)
            {
                return RedirectToPage("/LoginPage");
            }

            // Fetch student details based on the determined ID
            var studentResponse = await _studentServices.GetByIdAsync(userId);
            if (studentResponse.Status > 0 && studentResponse.Data != null)
            {
                Student = studentResponse.Data as User;
            }
            else
            {
                return RedirectToPage("/LoginPage");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var result = await _studentServices.UpdateStudentAsync(Student);

                if (result.Status > 0)
                {
                    ViewData["SuccessMessage"] = result.Message;
                    return Page();
                }
                else
                {
                    ViewData["ErrorMessage"] = result.Message;
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }
        }
    }
}
