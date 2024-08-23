﻿using BusinessAccess.Repository;
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
        private readonly LoginService _userService;

        public LoginPageModel(LoginService userService)
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
                if (user.Status == "Disactive")
                {
                    ErrorMessage = "Your account has been banned for violating the rules or is under review and will be reopened later.";
                    return Page();
                }
                if (user.Status == "Pending")
                {
                    ErrorMessage = "You must wait for Admin approval! Please wait within 15 minutes for a response.";
                    return Page();
                }
                if (user.Status == "RejectRequest")
                {
                    HttpContext.Session.SetInt32("TutorId", user.UserId);
                    return RedirectToPage("BecomeTutor");
                }
                if (user.Role.Equals("Student"))
                {
                    try
                    {
                        var cart = HttpContext.Session.GetString("cart");
                        if (cart != null)
                        {
                            HttpContext.Session.SetInt32("StudentId", user.UserId);
                            return RedirectToPage("Cart");
                        }
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
                    return RedirectToPage("Tutor/Index");
                }
            }
            ErrorMessage = "Incorect User Name or Password Please Try Again";
            return Page();
            
        }
    }

}
