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


        public IActionResult OnPost()

        {
            var user = _userService.CheckLogin(Email, PasswordHash);

            if (user != null)
            {
                if (user.Status != "Active")
                {
                    ErrorMessage = "You are not allowed access into system";
                    return Page();
                }
                if (user.Role.Equals("Student"))
                {
                    try
                    {
                        /*var cart = HttpContext.Session.GetString("cart");
                        if (cart != null)
                        {
                            HttpContext.Session.SetInt32("UserID", user.UserId);
                            return RedirectToPage("Cart");
                        }*/
                    }
                    catch
                    {
                        HttpContext.Session.SetInt32("StudentId", user.UserId);
                        return RedirectToPage("HomePage");
                    }
                    HttpContext.Session.SetInt32("StudentId", user.UserId);
                    return RedirectToPage("HomePage");

                }
                if (user.Role.Equals("Admin"))
                {
                    HttpContext.Session.SetInt32("AdminId", user.UserId);
                    HttpContext.Session.SetString("isAdmin", user.Role);
                    return RedirectToPage("Admin/Index");
                }
                if (user.Role.Equals("Tutor"))
                {
                    HttpContext.Session.SetInt32("TutorId", user.UserId);
                    HttpContext.Session.SetString("isTutor", user.Role);
                    return RedirectToPage("Tutor/Index");
                }
            }
            ErrorMessage = "Incorect User Name or Password Please Try Again";
            return Page();
            
        }
    }

}
