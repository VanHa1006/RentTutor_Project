using BusinessAccess.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RentTutorPresentation.Pages
{
    public class TutorDetailModel : PageModel
    {
        private readonly ITutorServices _tutorServices;

        public TutorDetailModel(ITutorServices tutorServices)
        {
            _tutorServices = tutorServices;
        }
        [BindProperty]
        public DataAccess.Models.Tutor Tutor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutor = await _tutorServices.GetTutorById(id);
            if (tutor == null)
            {
                return NotFound();
            }
            Tutor = tutor.Data as DataAccess.Models.Tutor;
            return Page();
        }
    }
}
