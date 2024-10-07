using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace BusinessAccess.Services
{
    public class VNPayService
    {
        /*private readonly IConfiguration _configuration;

        public VNPayService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
            private readonly string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
            private readonly string vnp_TmnCode = "LZINB6FH"; // Thay bằng mã TMN của bạn
            private readonly string vnp_HashSecret = "XQNQ8LFI4IVW9XR5WDVQP7RAZWJ5WG33"; // Thay bằng bí mật hash của bạn
            public string CreatePaymentUrl(string orderId, decimal amount, string orderDescription)
            {
                var vnpayData = new SortedDictionary<string, string>
                {
                    { "vnp_Version", "2.0.0" },
                    { "vnp_TmnCode", vnp_TmnCode },
                    { "vnp_Amount", ((int)(amount * 100)).ToString() }, // VNPay yêu cầu số tiền tính bằng đơn vị tiền tệ nhỏ nhất (VND)
                    { "vnp_OrderId", orderId },
                    { "vnp_OrderInfo", orderDescription },
                    { "vnp_ReturnUrl", "http://your-return-url.com" }, // Thay bằng URL quay lại của bạn
                    { "vnp_TxnRef", orderId },
                    { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") },
                    { "vnp_IpAddr", HttpContext.Current.Request.UserHostAddress } // Địa chỉ IP của khách hàng
                };

                // Xây dựng chuỗi truy vấn
                var queryString = new StringBuilder();
                foreach (var kv in vnpayData)
                {
                    if (!string.IsNullOrEmpty(kv.Value))
                    {
                        queryString.Append(HttpUtility.UrlEncode(kv.Key) + "=" + HttpUtility.UrlEncode(kv.Value) + "&");
                    }
                }

                // Xóa dấu '&' cuối cùng
                if (queryString.Length > 0)
                {
                    queryString.Remove(queryString.Length - 1, 1);
                }

                // Tạo mã băm bảo mật và thêm vào chuỗi truy vấn
                string secureHash = CreateSecureHash(vnp_HashSecret, vnpayData);
                queryString.Append("&vnp_SecureHash=" + HttpUtility.UrlEncode(secureHash));

                // Tạo URL thanh toán
                return vnp_Url + "?" + queryString.ToString();
            }

            // Phương thức tạo mã băm bảo mật (như đã định nghĩa trước đó)
            private string CreateSecureHash(string hashSecret, SortedDictionary<string, string> vnpayData)
            {
                var data = new StringBuilder();
                foreach (var kv in vnpayData)
                {
                    if (!string.IsNullOrEmpty(kv.Value))
                    {
                        data.Append(kv.Key + "=" + kv.Value + "&");
                    }
                }

                if (data.Length > 0)
                {
                    data.Remove(data.Length - 1, 1);
                }

                using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(hashSecret)))
                {
                    var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data.ToString()));
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }
            }

            public bool ValidateSignature(string receivedSecureHash, IQueryCollection queryParams)
            {
                var vnPayConfig = _configuration.GetSection("VNPay");
                var vnp_TmnCode = vnPayConfig["vnp_TmnCode"];
                var vnp_HashSecret = vnPayConfig["vnp_HashSecret"];
                var vnp_Url = vnPayConfig["vnp_Url"];
                var vnp_ReturnUrl = vnPayConfig["vnp_ReturnUrl"];
                // Lấy danh sách các tham số từ queryParams và loại bỏ "vnp_SecureHash"
                var sortedParams = queryParams
                    .Where(kv => kv.Key != "vnp_SecureHash")
                    .OrderBy(kv => kv.Key)
                    .ToDictionary(kv => kv.Key, kv => kv.Value.ToString());

                // Tạo lại chuỗi dữ liệu cần mã hóa
                var data = new StringBuilder();
                foreach (var kv in sortedParams)
                {
                    data.Append(kv.Key + "=" + kv.Value + "&");
                }

                data.Remove(data.Length - 1, 1); // Xóa ký tự '&' cuối cùng

                // Tạo chữ ký số từ chuỗi dữ liệu
                var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(vnp_HashSecret));
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data.ToString()));
                var computedHashString = BitConverter.ToString(computedHash).Replace("-", "").ToLower();

                // So sánh chữ ký số vừa tạo với chữ ký số nhận được
                return computedHashString.Equals(receivedSecureHash, StringComparison.OrdinalIgnoreCase);
        }*/
    }
}
