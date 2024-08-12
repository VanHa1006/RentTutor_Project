using BusinessAccess.Business;
using BusinessAccess.Repository;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RentTutorPresentation.Pages.Admin
{
    public class StudentDetailModel : PageModel
    {
        private readonly UserRepositories _userRepositories;

        public StudentDetailModel(DataAccess.Models.RenTurtorToStudentContext context)
        {
            _userRepositories = new UserRepositories(context);
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepositories.GetByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            User = user;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var success = await _userRepositories.UpdateUserAsync(User);
            if (!success)
            {
                return NotFound();
            }

            return RedirectToPage("./StudentManager");
        }
    }
}
