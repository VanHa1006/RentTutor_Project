using BusinessAccess.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RentTutorPresentation.Pages.Admin
{
    public class Update_CateModel : PageModel
    {
        private readonly ICategoryServices _categoryServices;

        public Update_CateModel(ICategoryServices categoryServices)
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

            var category = await _categoryServices.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            Category = category.Data as Category;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _categoryServices.UpdateAsync(Category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductCategoryExists(Category.CategoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./CategoriesCourses");
        }

        private async Task<bool> ProductCategoryExists(int id)
        {
            var productCategory = await _categoryServices.GetById(id);
            return productCategory != null && productCategory.Data != null;
        }
    }
}

