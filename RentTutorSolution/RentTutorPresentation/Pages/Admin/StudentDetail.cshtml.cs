using BusinessAccess.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RentTutorPresentation.Pages.Admin
{
    public class StudentDetailModel : PageModel
    {

        private readonly IUserServices _studentServices;
        public StudentDetailModel(IUserServices studentServices)
        {
            _studentServices = studentServices;
        }
        [BindProperty]
        public User Student { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _studentServices.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            Student = user.Data as User;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var result = await _studentServices.UpdateAsync(Student);
                if (result.Status > 0)
                {
                    ViewData["SuccessMessage"] = result.Message;
                    return RedirectToPage("./StudentManager");
                }
                else
                {
                    ViewData["ErrorMessage"] = result.Message;
                    return Page();
                }
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!await StudentExists(Student.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task<bool> StudentExists(int id)
        {
            var customer = await _studentServices.GetByIdAsync(id);
            return customer != null && customer.Data != null;
        }
    }
}
