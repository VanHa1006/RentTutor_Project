using BusinessAccess.Services;
using DataAccess.Models;
using DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;

namespace RentTutorPresentation.Pages.Admin
{
    public class CategoriesCoursesModel : PageModel
    {
        private readonly ICategoryServices _categoryServices;

        public CategoriesCoursesModel(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        public string Message { get; set; } = default!;

        public Paginate<Category> Category { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public int Size { get; set; } = 10;

        [BindProperty]
        public Category Categories { get; set; } = default!;



        private async Task<Paginate<Category>> GetCategoryPage()
        {
            var result = await _categoryServices.GetAll(PageIndex, Size);
            if (result.Status > 0 && result.Data != null)
            {

                var categories = result.Data;
                return (Paginate<Category>)categories;
            }
            return null;
        }

        private async Task<Paginate<Category>> Search()
        {
            var result = await _categoryServices.Search(SearchTerm, PageIndex, Size);
            if (result.Status > 0 && result.Data != null)
            {
                var categories = result.Data;
                return (Paginate<Category>)categories;
            }
            return null;
        }

        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Category = await Search();
            }
            else
            {
                Category = await GetCategoryPage();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var deletecategory = await _categoryServices.DeleteAsync((int)id);
            if (deletecategory.Status > 0)
            {
                return new JsonResult(new { success = true, message = "Category deleted successfully" });
            }
            else
            {
                return new JsonResult(new { success = false, message = "!!!!!!!!!!!!" });
            }
        }


    }
}