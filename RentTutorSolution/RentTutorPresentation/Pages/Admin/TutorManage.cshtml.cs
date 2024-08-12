using BusinessAccess.Services;
using DataAccess.Models;
using DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RentTutorPresentation.Pages.Admin
{
    public class TutorManageModel : PageModel
    {
        private readonly IUserServices _tutorServices;
        private readonly ITutorServices _tutorServicesRepository;
        public TutorManageModel(IUserServices tutorServices, ITutorServices tutorServicesRepository)
        {
            _tutorServices = tutorServices;
            _tutorServicesRepository = tutorServicesRepository;
        }

        public string Message { get; set; } = default!;
        public Paginate<User> Tutor { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public int Size { get; set; } = 10;

        private async Task<Paginate<User>> GetTutors()
        {
            var result = await _tutorServices.GetAllTutor(PageIndex, Size);
            if (result.Status > 0 && result.Data != null)
            {

                var students = result.Data;
                return (Paginate<User>)students;
            }
            return null;
        }

        private async Task<Paginate<User>> Search()
        {
            var result = await _tutorServices.Search(SearchTerm, PageIndex, Size);
            if (result.Status > 0 && result.Data != null)
            {
                var customer = result.Data;
                return (Paginate<User>)customer;
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
                Tutor = await GetTutors();
            }
        }
    }
}
