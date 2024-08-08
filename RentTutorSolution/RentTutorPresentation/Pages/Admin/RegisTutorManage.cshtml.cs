using BusinessAccess.Services;
using DataAccess.Models;
using DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RentTutorPresentation.Pages.Admin
{
    public class RegisTutorManageModel : PageModel
    {
        private readonly ITutorServices _tutorServices;

        public RegisTutorManageModel(ITutorServices tutorServices)
        {
            _tutorServices = tutorServices;
        }

        public string Message { get; set; } = default!;
        public Paginate<DataAccess.Models.Tutor> Tutor { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public int Size { get; set; } = 10;

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
    }
}
