using BusinessAccess.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RentTutorPresentation.Helpers;

namespace RentTutorPresentation.Pages.Student
{
    public class ShoppingcartModel : PageModel
    {
        public ShoppingCart Cart { get; set; }

        public IActionResult OnGet()
        {

            var cartData = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");


            if (cartData == null)
            {

                Cart = new ShoppingCart();
            }
            else
            {

                Cart = cartData;
            }

            return Page();
        }

        public IActionResult OnPostCheckout()
        {
            // Lấy dữ liệu giỏ hàng từ Session
            var cartData = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");

            if (cartData == null || cartData.Items.Count == 0)
            {
                // Xử lý giỏ hàng rỗng
                return RedirectToPage("EmptyCart");
            }

            // Lưu giỏ hàng vào session (nếu cần thiết)
            HttpContext.Session.SetObjectAsJson("Cart", cartData);
            HttpContext.Session.SetObjectAsJson("TotalAmount", cartData.GetTotal());
            HttpContext.Session.SetObjectAsJson("TotalDiscount", cartData.GetDiscount());
            HttpContext.Session.SetObjectAsJson("TotalPrice", cartData.Price());

            return RedirectToPage("./Checkout");
        }
    }
}

