using BusinessAccess.Business;
using BusinessAccess.Services;
using DataAccess.Models;
using DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RentTutorPresentation.Pages.Admin
{
    public class StudentManagerModel : PageModel
    {
        private readonly IUserBusiness _userBusiness;
        private readonly UserService _userService;

        public StudentManagerModel(IUserBusiness userBusiness, UserService userService)
        {
            _userBusiness = userBusiness;
            _userService = userService;
        }
        public List<User> Students { get; set; }

        public async Task OnGetAsync()
        {
            Students = await _userService.GetAllStudents();
        }

        public async Task<IActionResult> OnGetStudentDetailsAsync(int id)
        {
            var student = await _userService.GetUserByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return new JsonResult(student);
        }
    }
}
