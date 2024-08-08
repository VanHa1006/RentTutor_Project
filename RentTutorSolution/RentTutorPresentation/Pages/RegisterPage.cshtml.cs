using BusinessAccess.Services;
using DataAccess.Models;
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
    }
}