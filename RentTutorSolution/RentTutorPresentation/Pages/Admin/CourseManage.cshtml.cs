﻿using BusinessAccess.Repository;
using BusinessAccess.Services;
using DataAccess.Models;
using DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace RentTutorPresentation.Pages.Admin
{
    public class Index1Model : PageModel
    {
        private readonly ICourseServices _courseServices;

        public Index1Model(ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }
        public Paginate<Course> Course { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public int Size { get; set; } = 10;

        [BindProperty]
        public Course Courses { get; set; } = default!;


        private async Task<Paginate<Course>> GetCourses()
        {
            var result = await _courseServices.GetAll(PageIndex, Size);
            if (result.Status > 0 && result.Data != null)
            {
                var course = result.Data;
                return (Paginate<Course>)course;
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
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Course = await Search();
            }
            else
            {
                Course = await GetCourses();
            }
            ViewData["SuccessMessage"] = TempData["SuccessMessage"];
        }


        public async Task<IActionResult> OnPostEditAsync()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<CourseUpdateModel>(json);

            if (data == null)
            {
                return new JsonResult(new { success = false, message = "Invalid data" });
            }

            var result = await _courseServices.UpdateAsync(data.CourseID, data.Status);

            return new JsonResult(result);
        }

        public class CourseUpdateModel
        {
            public int CourseID { get; set; }
            public string Status { get; set; }
        }
    }
}
