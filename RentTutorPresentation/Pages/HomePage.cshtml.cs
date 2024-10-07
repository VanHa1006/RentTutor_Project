using BusinessAccess.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RentTutorPresentation.Pages
{
    public class HomePageModel : PageModel
    {

        public int? UserId { get; set; }

        [BindProperty]
        public User Student { get; set; } = default!;

        private readonly IUserServices _studentServices;

        public HomePageModel(IUserServices studentServices)
        {
            _studentServices = studentServices;
        }

        public void OnGet()
        {
            UserId = HttpContext.Session.GetInt32("StudentId");

            if (UserId.HasValue)
            {
                Student = _studentServices.GetUserById(UserId.Value);
            }
        }


    }
}
