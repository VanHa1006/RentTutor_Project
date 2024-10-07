using BusinessAccess.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RentTutorPresentation.Pages.Admin
{
    public class Delete_CateModel : PageModel
    {
        private readonly ICategoryServices _categoryServices;

        public Delete_CateModel(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productcategory = await _categoryServices.DeleteAsync(id);

            if (productcategory.Status > 0)
            {
                ViewData["SuccessMessage"] = productcategory.Message;
                return RedirectToPage("./CategoriesCourses");
            }
            else
            {
                TempData["ErrorMessage"] = productcategory.Message;
                return RedirectToPage("./CategoriesCourses");
            }
        }
    }
}
