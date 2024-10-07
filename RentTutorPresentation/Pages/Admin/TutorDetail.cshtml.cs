using BusinessAccess.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RentTutorPresentation.Pages.Admin
{
    public class TutorDetailModel : PageModel
    {
        private readonly IUserServices _userServices;
        private readonly ITutorServices _tutorServices;

        public TutorDetailModel(IUserServices userServices, ITutorServices tutorServices)
        {
            _userServices = userServices;
            _tutorServices = tutorServices;
        }
        [BindProperty]
        public User Tutor { get; set; } = default!;
        public DataAccess.Models.Tutor TutorDetails { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userServices.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            Tutor = user.Data as User;

            // Get Tutor information
            var tutorResult = await _tutorServices.GetTutorById(id);
            if (tutorResult == null)
            {
                return NotFound();
            }
            TutorDetails = tutorResult.Data as DataAccess.Models.Tutor;

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var result = await _userServices.UpdateAsync(Tutor);
                if (result.Status > 0)
                {
                    ViewData["SuccessMessage"] = result.Message;
                    return RedirectToPage("./TutorManage");
                }
                else
                {
                    ViewData["ErrorMessage"] = result.Message;
                    return Page();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TutorExists(Tutor.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
        private async Task<bool> TutorExists(int id)
        {
            var customer = await _userServices.GetByIdAsync(id);
            return customer != null && customer.Data != null;
        }
    }
}

