using BusinessAccess.Business;
using BusinessAccess.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentTutorPresentation.Helpers;

namespace RentTutorPresentation.Pages.Student
{
    public class CheckoutModel : PageModel
    {
        private readonly RenTurtorToStudentContext _context;
        private readonly IOrderService _orderBusiness;
        private readonly IOrderDetailServices _orderDetailBusiness;
        public int UserID { get; set; }

        public CheckoutModel(RenTurtorToStudentContext context, IOrderService orderBusiness,
            IOrderDetailServices orderDetailBusiness)
        {
            _context = context;
            _orderBusiness = orderBusiness;
            _orderDetailBusiness = orderDetailBusiness;
        }
        public IActionResult OnGet()
        {
            var cartData = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            var totalAmount = HttpContext.Session.GetObjectFromJson<decimal>("TotalAmount");
            var totalDiscount = HttpContext.Session.GetObjectFromJson<decimal>("TotalDiscount");
            var totalPrice = HttpContext.Session.GetObjectFromJson<decimal>("TotalPrice");
            var userIdFromSession = HttpContext.Session.GetInt32("UserId");
            if (userIdFromSession.HasValue)
            {
                UserID = userIdFromSession.Value;
            }
            TempData["CartData"] = cartData;
            TempData["TotalAmount"] = totalAmount;
            TempData["TotalDiscount"] = totalDiscount;
            TempData["TotalPrice"] = totalPrice;


            //TempData["Cart"] = JsonConvert.SerializeObject(cartData);

            ViewData["UserId"] = new SelectList(_context.Students, "UserId", "UserId");
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;
        [BindProperty]
        public string Cart { get; set; } = string.Empty;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    }
}
