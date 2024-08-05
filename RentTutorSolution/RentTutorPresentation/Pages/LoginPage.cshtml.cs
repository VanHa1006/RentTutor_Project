using BusinessAccess.Repository;
using BusinessAccess.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RentTutorPresentation.Pages
{
    public class LoginPageModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        public LoginPageModel(IUserRepository userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        [BindProperty]
        public User user { get; set; } = default!;
        public string ErrorMessage { get; private set; }

        public IActionResult OnPost()
        {

            if (!string.IsNullOrWhiteSpace(user.Email) && !string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                try
                {
                    var check = _userService.checkLogin(user.Email, user.PasswordHash);
                    if (check != null)
                    {
                        if (check.IsActive != true)
                        {
                            ErrorMessage = "You are not allowed access into system";
                            return Page();
                        }
                        if (check.Role.Equals("US"))
                        {
                            return RedirectToPage("HomePage");
                            /*try
                            {
                                var cart = HttpContext.Session.GetString("cart");
                                if (cart != null)
                                {
                                    HttpContext.Session.SetInt32("UserID", check.UserId);
                                    return RedirectToPage("Cart");
                                }
                            }
                            catch
                            {
                                HttpContext.Session.SetInt32("UserID", check.UserId);
                                return RedirectToPage("HomePage");
                            }
                            HttpContext.Session.SetInt32("UserID", check.UserId);
                            return RedirectToPage("HomePage");
                        }
                        if (check.RoleId.Equals("AD"))
                        {
                            HttpContext.Session.SetInt32("UserID", check.UserId);
                            HttpContext.Session.SetString("isAdmin", check.RoleId);
                            return RedirectToPage("Admin/UserManagement/ShowUserList");
                        }
                        if (check.RoleId.Equals("MN"))
                        {
                            HttpContext.Session.SetInt32("UserID", check.UserId);
                            HttpContext.Session.SetString("isManager", check.RoleId);
                            return RedirectToPage("Manager/StaffManagement/Index");
                        }
                        if (check.RoleId.Equals("ST"))
                        {
                            HttpContext.Session.SetInt32("UserID", check.UserId);
                            HttpContext.Session.SetString("isStaff", check.RoleId);
                            return RedirectToPage("Staff/BirdManagement/Index");*/
                        }
                    }
                    ErrorMessage = "Incorect User Name or Password Please Try Again";
                    return Page();
                }
                catch
                {
                    ErrorMessage = "Incorect User Name or Password Please Try Again";
                    return Page();
                }
            }
            return Page();
        }
    }
}
