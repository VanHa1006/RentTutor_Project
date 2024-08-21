using BusinessAccess.Business;
using BusinessAccess.Services;
using DataAccess.Models;
using DataAccess.VnPayModels;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using RentTutorPresentation.Helpers;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace RentTutorPresentation.Pages.Student
{
    public class CheckoutModel : PageModel
    {
        private readonly RenTurtorToStudentContext _context;
        private readonly IOrderService _orderBusiness;
        private readonly IOrderDetailServices _orderDetailBusiness;
        private readonly VNPayService _vnPayService;
        private readonly IConfiguration _configuration;
        private readonly Utils _utils;
        public int UserID { get; set; }

        public CheckoutModel(RenTurtorToStudentContext context, IOrderService orderBusiness,
            IOrderDetailServices orderDetailBusiness, VNPayService vnPayService, IConfiguration configuration, Utils utils)
        {
            _context = context;
            _orderBusiness = orderBusiness;
            _orderDetailBusiness = orderDetailBusiness;
            _vnPayService = vnPayService;
            _configuration = configuration;
            _utils = utils;
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

        public IActionResult OnPost()
        {
            var cartData = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            var totalAmount = HttpContext.Session.GetObjectFromJson<decimal>("TotalAmount");
            var totalPrice = HttpContext.Session.GetObjectFromJson<decimal>("TotalPrice");
            var userIdFromSession = HttpContext.Session.GetInt32("StudentId");

            if (userIdFromSession.HasValue)
            {
                UserID = userIdFromSession.Value;
            }

            if (totalAmount <= 0 || totalAmount > 1000000000)
            {
                ModelState.AddModelError(string.Empty, "Số tiền không hợp lệ.");
                return Page();
            }

            if (totalPrice <= 0 || totalPrice > 1000000000)
            {
                ModelState.AddModelError(string.Empty, "Số tiền không hợp lệ.");
                return Page();
            }

            string tmnCode = _configuration["VNPAY:TmnCode"];
            string hashSecret = _configuration["VNPAY:HashSecret"];
            string vnpUrl = _configuration["VNPAY:VnpUrl"];
            string returnUrl = _configuration["VNPAY:ReturnUrl"];
            string txnRef = DateTime.Now.Ticks.ToString();

            var Prices = (long)totalAmount * 100;

            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", tmnCode);
            vnpay.AddRequestData("vnp_Amount", Prices.ToString());
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", _utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang");
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", returnUrl);
            vnpay.AddRequestData("vnp_TxnRef", txnRef);

            string paymentUrl = vnpay.CreateRequestUrl(vnpUrl, hashSecret);
            return Redirect(paymentUrl);
        }
    }
}

