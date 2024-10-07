using BusinessAccess.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RentTutorPresentation.Helpers;

namespace RentTutorPresentation.Pages.Student
{
    public class RemoveFromCartModel : PageModel
    {
        public IActionResult OnGet(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();

            // Ki?m tra xem s?n ph?m có trong gi? hàng không
            var cartItem = cart.Items.Find(item => item.CourseId == id);
            if (cartItem != null)
            {
                cart.RemoveFromCart(id);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }

            return RedirectToPage("/Student/Shoppingcart");
        }
    }
}
