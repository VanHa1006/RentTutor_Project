using BusinessAccess.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RentTutorPresentation.Pages.Tutor
{
    public class Tutor_ProfileModel : PageModel
    {
        private readonly IUserServices _studentServices;
        
        public Tutor_ProfileModel(IUserServices studentServices)
        {
            _studentServices = studentServices;
        }
        [BindProperty]
        public User Student { get; set; } = default!;


        public async Task OnGetStudentAsync()
        {
            int? userId = HttpContext.Session.GetInt32("TutorId");
            if (userId.HasValue)
            {
                var studentResponse = await _studentServices.GetByIdAsync(userId.Value);
                if (studentResponse.Status > 0 && studentResponse.Data != null)
                {
                    Student = studentResponse.Data as User;
                }
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await OnGetStudentAsync();
            if (Student == null)
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
