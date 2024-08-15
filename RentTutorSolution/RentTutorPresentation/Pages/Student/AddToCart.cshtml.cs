using BusinessAccess.Business;
using DataAccess.CartModel;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RentTutorPresentation.Helpers;

namespace RentTutorPresentation.Pages.Student
{
    public class AddToCartModel : PageModel
    {
        private readonly RenTurtorToStudentContext _context;
        public AddToCartModel(RenTurtorToStudentContext context)
        {
            _context = context;
        }
        public IActionResult OnGet(int id, int pageIndex, string searchTerm)
        {
            var course = _context.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            if (pageIndex == 0) pageIndex = 1;

            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();

            cart.AddToCart(new CartItem
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                Image = course.Image,
                Price = course.Price,
                Quantity = 1 // Default quantity to add to cart
            });
            TempData["SuccessMessage"] = $"{course.CourseName} is added to cart";
            HttpContext.Session.SetInt32("cartQuantity", cart.Items.Count);
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToPage("/Courses", new { PageIndex = pageIndex, SearchTerm = searchTerm });
        }
    }
}
