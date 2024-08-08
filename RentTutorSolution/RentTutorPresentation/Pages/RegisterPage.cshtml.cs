using BusinessAccess.Services;
using DataAccess.Models;
using DataAccess.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace RentTutorPresentation.Pages
{
	public class RegisterPageModel : PageModel
	{
		/*private readonly IHubContext<TutorHub> _hubContext;
        private readonly IUserBusiness _userBusiness;

        [BindProperty]
        public User User { get; set; } = default!;
        public RegisterPageModel(IUserBusiness userBusiness, IHubContext<TutorHub> hubContext)
		{
            _userBusiness = userBusiness;
            _hubContext = hubContext;
		}

		public void OnGet()
		{
		}

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _userBusiness.RegisterTutor(User);
            if (result.Status > 0)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveNewTutorRegistration");
                ViewData["SuccessMessage"] = result.Message;
                return RedirectToPage("./Index");
            }
            else
            {
                ViewData["ErrorMessage"] = $"Error: {result.Message}";
                return Page();
            }

        }*/

        private readonly IStudentServices _userServices;
        private readonly ITutorServices _tutorServices;

        public RegisterPageModel(IStudentServices studentServices, ITutorServices tutorServices)
        {
            _userServices = studentServices;
            _tutorServices = tutorServices;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DataAccess.Models.Student Student { get; set; } = default!;
        public async Task<IActionResult> OnPostRegisterStudentAsync()
        {
            var result = await _userServices.Save(Student);
            if (result.Status > 0)
            {
                ViewData["SuccessMessage"] = result.Message;
                return RedirectToPage("./LoginPage");
            }
            else
            {
                ViewData["ErrorMessage"] = $"Error: {result.Message}";
                return Page();
            }

        }

        [BindProperty]
        public DataAccess.Models.Tutor Tutor { get; set; } = default!;
        public async Task<IActionResult> OnPostRegisterTutorAsync()
        {
            var result = await _tutorServices.RegisterTutor(Tutor);
            if (result.Status > 0)
            {
                ViewData["SuccessMessage"] = result.Message;
                return RedirectToPage("./LoginPage");
            }
            else
            {
                ViewData["ErrorMessage"] = $"Error: {result.Message}";
                return Page();
            }

        }
    }
}