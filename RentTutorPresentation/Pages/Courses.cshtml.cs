﻿using BusinessAccess.Services;
using DataAccess.Models;
using DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace RentTutorPresentation.Pages
{
    public class CoursesModel : PageModel
    {
        private readonly ICourseServices _courseServices;
        private readonly IUserServices _tutorServices;
        private readonly ITutorServices _tutorServicesService;

        public CoursesModel(ICourseServices courseServices, IUserServices tutorServices, ITutorServices tutorServicesService)
        {
            _courseServices = courseServices;
            _tutorServices = tutorServices;
            _tutorServicesService = tutorServicesService;
        }

        public Paginate<Course> Course { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int Size { get; set; } = 12;

        [BindProperty(SupportsGet = true)]
        public Paginate<User> Tutor { get; set; } = default!;
        public int? UserId { get; set; }

        [BindProperty]
        public Course Courses { get; set; } = default!;

        private async Task<Paginate<Course>> GetCourses()
        {
            UserId = HttpContext.Session.GetInt32("StudentId");

            var result = await _courseServices.GetAll(PageIndex, Size);

            if (result.Status > 0 && result.Data != null)
            {
                var courses = result.Data as Paginate<Course> ?? new Paginate<Course>();

                if (UserId.HasValue)
                {
                    // Lọc các khóa học dựa trên OrderDetails của UserId hiện tại
                    courses.Items = courses.Items
                        .Where(course => course.OrderDetails != null &&
                                         !course.OrderDetails
                                             .Where(od => od.Order != null && od.Order.StudentId == UserId.Value)
                                             .Any(od => od.Status == "Studying" || od.Status == "Pending"))
                        .ToList();
                }

                return courses;
            }

            return new Paginate<Course>();
        }

        private async Task<Paginate<User>> GetTutors()
        {
            var result = await _tutorServices.GetAllTutorActive(PageIndex, Size);
            if (result.Status > 0 && result.Data != null)
            {
                return result.Data as Paginate<User> ?? new Paginate<User>();
            }
            return new Paginate<User>();
        }

        private async Task<Paginate<Course>> Search()
        {
            if (string.IsNullOrEmpty(SearchTerm))
            {
                return await GetCourses();
            }
            else
            {
                var result = await _courseServices.Search(SearchTerm, PageIndex, Size);
                if (result.Status > 0 && result.Data != null)
                {
                    return result.Data as Paginate<Course> ?? new Paginate<Course>();
                }
                return new Paginate<Course>();
            }
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

            Tutor = await GetTutors();
            ViewData["SuccessMessage"] = TempData["SuccessMessage"];
        }

        public async Task<IActionResult> OnPostSearchAsync()
        {
            Course = await Search();
            Tutor = await GetTutors();
            return Page();
        }
    }
}