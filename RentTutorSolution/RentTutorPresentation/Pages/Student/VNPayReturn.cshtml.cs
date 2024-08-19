using BusinessAccess.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RentTutorPresentation.Pages.Student
{
    public class VNPayReturnModel : PageModel
    {
        private readonly VNPayService _vnPayService;

        /*public VNPayReturnModel(VNPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        public string Message { get; set; }

        public void OnGet()
        {
            var queryParams = HttpContext.Request.Query;
            string vnp_SecureHash = queryParams["vnp_SecureHash"];

            if (_vnPayService.ValidateSignature(vnp_SecureHash, queryParams))
            {
                string orderId = queryParams["vnp_TxnRef"];
                string vnp_ResponseCode = queryParams["vnp_ResponseCode"];

                if (vnp_ResponseCode == "00")
                {
                    Message = "Thanh toán thành công!";
                }
                else
                {
                    Message = "Thanh toán thất bại!";
                }
            }
            else
            {
                Message = "Chữ ký không hợp lệ!";
            }
        }*/
    }
}
