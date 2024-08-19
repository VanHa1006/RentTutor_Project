using BusinessAccess.Business;
using BusinessAccess.Services;
using DataAccess.Models;
using MailKit.Search;
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
        private readonly VNPayService _vnPayService;
        public int UserID { get; set; }

        public CheckoutModel(RenTurtorToStudentContext context, IOrderService orderBusiness,
            IOrderDetailServices orderDetailBusiness, VNPayService vnPayService)
        {
            _context = context;
            _orderBusiness = orderBusiness;
            _orderDetailBusiness = orderDetailBusiness;
            _vnPayService = vnPayService;
        }
        public IActionResult OnGet()
        {
            var cartData = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            var totalAmount = HttpContext.Session.GetObjectFromJson<decimal>("TotalAmount");
            var totalDiscount = HttpContext.Session.GetObjectFromJson<decimal>("TotalDiscount");
            var totalPrice = HttpContext.Session.GetObjectFromJson<decimal>("TotalPrice");
            var userIdFromSession = HttpContext.Session.GetInt32("StudentId");
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

        public async Task<IActionResult> OnPostAsync()
        {
            //List<CartItem> cartItems = JsonConvert.DeserializeObject<List<CartItem>>(Cart);
            var cartData = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            var totalAmount = HttpContext.Session.GetObjectFromJson<decimal>("TotalAmount");
            var totalDiscount = HttpContext.Session.GetObjectFromJson<decimal>("TotalDiscount");
            var totalPrice = HttpContext.Session.GetObjectFromJson<decimal>("TotalPrice");
            var userIdFromSession = HttpContext.Session.GetInt32("StudentId");
            if (userIdFromSession.HasValue)
            {
                UserID = userIdFromSession.Value;
            }


            TempData["CartData"] = cartData;
            TempData["TotalAmount"] = totalAmount;

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            // Giải mã dữ liệu giỏ hàng từ Cart

            var x = UserID;
            // Tạo đối tượng Order mới
            Order newOrder = new Order
            {
                StudentId = UserID,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount,
                Status = "Pending"
                // Thiết lập các thuộc tính khác của Order nếu có
            };

            // Lưu đơn hàng mới vào cơ sở dữ liệu
            await _orderBusiness.Save(newOrder);

            // Tạo các OrderDetail từ cartItems
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var item in cartData.Items)
            {
                orderDetails.Add(new OrderDetail
                {
                    OrderId = newOrder.OrderId,
                    CourseId = item.CourseId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price,
                    TotalPrice =item.Price - item.DiscountPrice
                });
            }

            // Lưu các OrderDetail vào cơ sở dữ liệu
            foreach (var orderDetail in orderDetails)
            {
                await _orderDetailBusiness.Add(orderDetail);
            }

            // Xóa giỏ hàng sau khi tạo đơn hàng thành công
            HttpContext.Session.Remove("Cart");
            HttpContext.Session.Remove("cartQuantity");

            TempData.Remove("CartData");
            TempData.Remove("TotalAmount");
            return RedirectToPage("../Courses");
        }
    }
}
