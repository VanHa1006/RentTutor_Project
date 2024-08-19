using BusinessAccess.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RentTutorPresentation.Pages.Admin
{
    public class Create_CateModel : PageModel
    {
        private readonly ICategoryServices _categoryServices;

        public Create_CateModel(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _categoryServices.Save(Category);
            if (result != null)
            {
                ViewData["SuccessMessage"] = result.Message;
                return RedirectToPage("./CategoriesCourses");
            }
            else
            {
                ViewData["ErrorMessage"] = $"Error: {result.Message}";
                return Page();
            }
        }
    }
}
