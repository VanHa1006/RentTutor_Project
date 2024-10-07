using BusinessAccess.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RentTutorPresentation.Pages
{
    public class BecomeTutorModel : PageModel
    {
        private readonly ITutorServices _services;

        public BecomeTutorModel(ITutorServices tutorServices)
        {
            _services = tutorServices;
        }
        [BindProperty]
        public DataAccess.Models.Tutor Tutor { get; set; } = default!;


        public async Task OnGetTutorAsync()
        {
            int? userId = HttpContext.Session.GetInt32("TutorId");
            if (userId.HasValue)
            {
                var tutorServices = await _services.GetById(userId.Value);
                if (tutorServices.Status > 0 && tutorServices.Data != null)
                {
                    Tutor = tutorServices.Data as DataAccess.Models.Tutor;
                }
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await OnGetTutorAsync();
            if (Tutor == null)
            {
                return RedirectToPage("/LoginPage");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                int? userId = HttpContext.Session.GetInt32("TutorId");
                if (userId.HasValue)
                {
                    var tutorServices = await _services.GetById(userId.Value);
                    if (tutorServices.Status > 0 && tutorServices.Data != null)
                    {
                        Tutor = tutorServices.Data as DataAccess.Models.Tutor;
                    }
                }

                var result = await _services.UpdateTutorAndUserAsync(Tutor);

                if (result.Status > 0)
                {
                    ViewData["SuccessMessage"] = result.Message;
                    return RedirectToPage("/Logout");
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
