using BusinessAccess.Services;
using DataAccess.Models;
using DataAccess.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RentTutorPresentation.Pages
{
	public class RegisterPageModel : PageModel
	{
		/*private readonly IHubContext<TutorHub> _hubContext;
        private readonly IUserBusiness _userBusiness;

        [BindProperty]
        public User User { get; set; } = default!;
        public RegisterPageModel(IUserBusiness userBusiness, IHubContext<TutorHub> hubContext)
		{
            _userBusiness = userBusiness;
            _hubContext = hubContext;
		}

		public void OnGet()
		{
		}

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _userBusiness.RegisterTutor(User);
            if (result.Status > 0)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveNewTutorRegistration");
                ViewData["SuccessMessage"] = result.Message;
                return RedirectToPage("./Index");
            }
            else
            {
                ViewData["ErrorMessage"] = $"Error: {result.Message}";
                return Page();
            }

        }*/

        private readonly IStudentServices _userServices;
        private readonly ITutorServices _tutorServices;

        public RegisterPageModel(IStudentServices studentServices, ITutorServices tutorServices)
        {
            _userServices = studentServices;
            _tutorServices = tutorServices;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DataAccess.Models.Student Student { get; set; } = default!;
        public async Task<IActionResult> OnPostRegisterStudentAsync()
        {
            try
            {
                // Kiểm tra địa chỉ email
                var email = Student.StudentNavigation.Email; // Giả sử email được lưu trong thuộc tính Tutor.Email
                if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^[^@\s]+@((gmail\.com)|(fpt\.edu\.vn))$"))
                {
                    throw new ArgumentOutOfRangeException("Email", "Email must be a valid @gmail.com or @fpt.edu.vn address.");
                }
                // Kiểm tra mật khẩu
                var password = Student.StudentNavigation.PasswordHash; // Giả sử mật khẩu được lưu trong thuộc tính Tutor.Password
                if (string.IsNullOrEmpty(password) || password.Length < 6)
                {
                    throw new ArgumentOutOfRangeException("Password", "Password must be at least 6 characters long.");
                }
                var result = await _userServices.Save(Student);
                if (result.Status > 0)
                {
                    ViewData["SuccessMessage"] = result.Message;
                    return new JsonResult(new { isSuccess = true, message = "Congratulations, Successfully registered, please Log in" });

                }
                else
                {
                    ViewData["ErrorMessage"] = $"Error: {result.Message}";
                    return new JsonResult(new { isSuccess = false, message = "Email is duplicated, you can enter another email account." });
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Xử lý ngoại lệ và trả về thông báo lỗi
                return new JsonResult(new { isSuccess = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ khác
                return new JsonResult(new { isSuccess = false, message = "An unexpected error occurred: " + ex.Message });
            }
        }


        [BindProperty]
        public DataAccess.Models.Tutor Tutor { get; set; } = default!;


        public async Task<IActionResult> OnPostRegisterTutorAsync()
        {
            try
            {
                // Kiểm tra ngày sinh
                if (Tutor.TutorNavigation.Birthday.HasValue)
                {
                    var today = DateOnly.FromDateTime(DateTime.Now);
                    var birthDate = Tutor.TutorNavigation.Birthday.Value;

                    // Kiểm tra ngày sinh không được là ngày trong tương lai
                    if (birthDate >= today)
                    {
                        throw new ArgumentOutOfRangeException("Birthday", "Birthday cannot be in the future.");
                    }

                    // Kiểm tra người dùng phải ít nhất 18 tuổi
                    var age = today.Year - birthDate.Year;
                    if (birthDate > today.AddYears(-age)) age--;

                    if (age < 18 || age > 50)
                    {
                        throw new ArgumentOutOfRangeException("Birthday", "You must be at least 18 years old and bigest 50 years old.");
                    }
                }
                else
                {
                    throw new ArgumentException("Birthday must be provided.");
                }

                // Kiểm tra số điện thoại
                var phone = Tutor.TutorNavigation.Phone; // Giả sử số điện thoại được lưu trong thuộc tính Tutor.Phone
                if (string.IsNullOrEmpty(phone) || !Regex.IsMatch(phone, @"^0\d{9}$"))
                {
                    throw new ArgumentOutOfRangeException("Phone", "Phone number must start with 0, contain exactly 10 digits, and must not contain letters.");
                }

                // Kiểm tra địa chỉ email
                var email = Tutor.TutorNavigation.Email; // Giả sử email được lưu trong thuộc tính Tutor.Email
                if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^[^@\s]+@((gmail\.com)|(fpt\.edu\.vn))$"))
                {
                    throw new ArgumentOutOfRangeException("Email", "Email must be a valid @gmail.com or @fpt.edu.vn address.");
                }

                // Kiểm tra mật khẩu
                var password = Tutor.TutorNavigation.PasswordHash; // Giả sử mật khẩu được lưu trong thuộc tính Tutor.Password
                if (string.IsNullOrEmpty(password) || password.Length < 6)
                {
                    throw new ArgumentOutOfRangeException("Password", "Password must be at least 6 characters long.");
                }

                // Đăng ký Tutor
                var result = await _tutorServices.RegisterTutor(Tutor);
                if (result.Status > 0)
                {
                    ViewData["SuccessMessage"] = result.Message;
                    return new JsonResult(new { isSuccess = true, message = "Congratulations, Successfully registered Tutor, please Log in" });
                }
                else
                {
                    ViewData["ErrorMessage"] = $"Error: {result.Message}";
                    return new JsonResult(new { isSuccess = false, message = "There was a problem during the registration process, please check your email or other information again and register again" });
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Xử lý ngoại lệ và trả về thông báo lỗi
                return new JsonResult(new { isSuccess = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ khác
                return new JsonResult(new { isSuccess = false, message = "An unexpected error occurred: " + ex.Message });
            }
        }

    }
}