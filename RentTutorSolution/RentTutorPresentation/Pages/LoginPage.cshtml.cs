using BusinessAccess.Repository;
using BusinessAccess.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;

namespace RentTutorPresentation.Pages
{
    public class LoginPageModel : PageModel
    {
        private readonly UserService _userService;

        public LoginPageModel(UserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string PasswordHash { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            // Get request
        }

        public IActionResult OnPost()
        {
            var user = _userService.CheckLogin(Email, PasswordHash);

            if (user != null)
            {
                // Đăng nhập thành công và trạng thái là Active
                return RedirectToPage("/HomePage"); // Điều hướng đến HomePage
            }
            else
            {
                // Đăng nhập thất bại hoặc trạng thái không phải Active
                ErrorMessage = "Invalid login attempt or account is not active.";
                return Page(); // Quay lại trang đăng nhập và hiển thị lỗi
            }
        }
    }

}
