using BusinessAccess.Repository;
using BusinessAccess.Services;
using DataAccess.Models;
using DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace RentTutorPresentation.Pages.Admin
{
    public class Index1Model : PageModel
    {
        private readonly ICourseServices _courseServices;
        private readonly ICategoryServices _categoryServices;

        public Index1Model(ICourseServices courseServices, ICategoryServices categoryServices)
        {
            _courseServices = courseServices;
            _categoryServices = categoryServices;
        }
        public Paginate<Course> Course { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public int Size { get; set; } = 100;

        [BindProperty]
        public Course Courses { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SortStatus { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortCate { get; set; }




        private async Task<Paginate<Course>> GetCourses()
        {
            var result = await _courseServices.GetAllToAdmin(PageIndex, Size);
            if (result.Status > 0 && result.Data != null)
            {
                var course = result.Data;
                return (Paginate<Course>)course;
            }
            return null;
        }


        private async Task<Paginate<Course>> GetStatusCourses()
        {
            var result = await _courseServices.GetAllCourseByStatus(SortStatus, PageIndex, Size);

            if (result.Status > 0 && result.Data != null)
            {
                return (Paginate<Course>)result.Data;
            }
            return null;
        }

        private async Task<Paginate<Course>> GetCoursesByCate()
        {
            var result = await _courseServices.GetAllCourseByCate(SortCate, PageIndex, Size);

            if (result.Status > 0 && result.Data != null)
            {
                return (Paginate<Course>)result.Data;
            }
            return null;
        }
        private async Task<Paginate<Course>> Search()
        {
            var result = await _courseServices.Search(SearchTerm, PageIndex, Size);
            if (result.Status > 0 && result.Data != null)
            {
                var course = result.Data;
                return (Paginate<Course>)course;
            }
            return null;
        }
        public async Task OnGetAsync()
        {
            var courseCate = await _categoryServices.GetAllCategories();
            ViewData["CategoryId"] = new SelectList((System.Collections.IEnumerable)courseCate.Data, "CategoryId", "CategoryName");

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Course = await Search();
            }
            else if (!string.IsNullOrEmpty(SortStatus))
            {
                Course = await GetStatusCourses();
            }
            else if (!string.IsNullOrEmpty(SortCate))
            {
                Course = await GetCoursesByCate();
            }
            else
            {
                Course = await GetCourses();
            }
            ViewData["SuccessMessage"] = TempData["SuccessMessage"];
        }
    }
}
