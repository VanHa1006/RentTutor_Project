using BusinessAccess.Business;
using BusinessAccess.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RentTutorPresentation.Helpers;

namespace RentTutorPresentation.Pages.Student
{
    public class Payment_successfulModel : PageModel
    {
        private readonly RenTurtorToStudentContext _context;
        private readonly IOrderService _orderBusiness;
        private readonly IOrderDetailServices _orderDetailBusiness;
        private readonly VNPayService _vnPayService;
        private readonly IConfiguration _configuration;
        private readonly Utils _utils;
        public int UserID { get; set; }

        public Payment_successfulModel(RenTurtorToStudentContext context, IOrderService orderBusiness,
            IOrderDetailServices orderDetailBusiness, VNPayService vnPayService, IConfiguration configuration, Utils utils)
        {
            _context = context;
            _orderBusiness = orderBusiness;
            _orderDetailBusiness = orderDetailBusiness;
            _vnPayService = vnPayService;
            _configuration = configuration;
            _utils = utils;
        }
        public async Task<IActionResult> OnGet()
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
                Status = "Success"
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
                    TotalPrice = item.Price - item.DiscountPrice
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
            return Page();
        }
    }
}
