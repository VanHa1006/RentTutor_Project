using BusinessAccess;
using BusinessAccess.Services;
using DataAccess.Models;
using DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace RentTutorPresentation.Pages.Admin
{
    public class RegisTutorManageModel : PageModel
    {
        private readonly ITutorServices _tutorServices;

        private readonly IUserApprovalLogService _userApprovalLogService;
        public RegisTutorManageModel(ITutorServices tutorServices, IUserApprovalLogService userApprovalLogService)
        {
            _tutorServices = tutorServices;
            _userApprovalLogService = userApprovalLogService;
        }

        public string Message { get; set; } = default!;
        public Paginate<DataAccess.Models.Tutor> Tutor { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public int Size { get; set; } = 10;
        public DataAccess.Models.Tutor Tutors { get; set; } = default!;

        public UserApprovalLog Log { get; set; } = default!;

        private async Task<Paginate<DataAccess.Models.Tutor>> GetTutorsPeding()
        {
            var result = await _tutorServices.GetAllTutorsPending(PageIndex, Size);
            if (result.Status > 0 && result.Data != null)
            {

                var tutors = result.Data;
                return (Paginate<DataAccess.Models.Tutor>)tutors;
            }
            return null;
        }

        private async Task<Paginate<DataAccess.Models.Tutor>> Search()
        {
            var result = await _tutorServices.SearchPending(SearchTerm, PageIndex, Size);
            if (result.Status > 0 && result.Data != null)
            {
                var tutor = result.Data;
                return (Paginate<DataAccess.Models.Tutor>)tutor;
            }
            return null;
        }
        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Tutor = await Search();
            }
            else
            {
                Tutor = await GetTutorsPeding();
            }
        }

        // Handle Accept action
        public async Task<IActionResult> OnPostAcceptAsync(int TutorId)
        {
            try
            {
                var result = await _tutorServices.AcceptTutor(TutorId);
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
                if (!await TutorExists(Tutors.TutorNavigation.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<IActionResult> OnPostDeclineAsync(int TutorId, string Decision, string Reason)
        {
            try
            {
                // Log the decision and update the user's status
                var logResult = await _userApprovalLogService.RejectRequestRegisterTutor(TutorId, Decision, Reason);
                if (logResult.Status > 0)
                {
                    ViewData["SuccessMessage"] = "The tutor has been declined, and the decision has been logged.";
                    return RedirectToPage("./TutorManage");
                }
                else
                {
                    ViewData["ErrorMessage"] = logResult.Message;
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return Page();
            }
        }

        private async Task<bool> TutorExists(int id)
        {
            var customer = await _tutorServices.AcceptTutor(id);
            return customer != null && customer.Data != null;
        }
    }
}
