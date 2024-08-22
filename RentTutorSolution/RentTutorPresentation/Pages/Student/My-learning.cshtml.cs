using BusinessAccess.Services;
using DataAccess.Models;
using DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;

namespace RentTutorPresentation.Pages.Student
{
    public class My_learningModel : PageModel
    {
        private readonly IOrderService _orderBusiness;
        private readonly IOrderDetailServices _orderDetailService;

        public My_learningModel(IOrderService orderBusiness, IOrderDetailServices orderDetailService)
        {
            _orderBusiness = orderBusiness;
            _orderDetailService = orderDetailService;
        }

        public Paginate<OrderDetail> OrderDetail { get; set; } = default!;
        public Paginate<Order> Order { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty(SupportsGet = true)]

        public int PageIndex { get; set; } = 1;
        [BindProperty(SupportsGet = true)]

        public int Size { get; set; } = 100;

        public int? StudentId { get; set; }


        [BindProperty(SupportsGet = true)]
        public string SortStatus { get; set; }


        private async Task<Paginate<OrderDetail>> GetOrder()
        {
            var result = await _orderDetailService.GetAllCourseForStudentStudy(PageIndex, Size, StudentId);
            if (result.Status > 0 && result.Data != null)
            {
                var order = result.Data;
                return (Paginate<OrderDetail>)order;
            }
            return null;
        }

        private async Task<Paginate<Order>> Search()
        {
            var result = await _orderBusiness.Search(SearchTerm, PageIndex, Size);
            if (result.Status > 0 && result.Data != null)
            {
                var order = result.Data;
                return (Paginate<Order>)order;
            }
            return null;
        }

        private async Task<Paginate<OrderDetail>> GetStatusOrdersByStudentId()
        {
            var result = await _orderDetailService.GetStatusOrdersByStudentId(SortStatus, PageIndex, Size,StudentId);

            if (result.Status > 0 && result.Data != null)
            {
                return (Paginate<OrderDetail>)result.Data;
            }
            return null;
        }


        public async Task OnGetAsync()
        {
            StudentId = HttpContext.Session.GetInt32("StudentId");
            if (!StudentId.HasValue)
            {
                // Xử lý trường hợp tutorId không có trong session
            }

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Order = await Search();
            }
            else if (!string.IsNullOrEmpty(SortStatus))
            {
                OrderDetail = await GetStatusOrdersByStudentId();
            }
            else
            {
                OrderDetail = await GetOrder();
            }
        }

        public async Task<IActionResult> OnPostDoneCourseAsync(int OrderId)
        {
            try
            {
                var result = await _orderDetailService.DoneCourseForStudent(OrderId);
                if (result.Status > 0)
                {
                    ViewData["SuccessMessage"] = result.Message;
                    await OnGetAsync();  // Gọi lại OnGetAsync để tải lại dữ liệu
                    return Page();
                }
                else
                {
                    ViewData["ErrorMessage"] = result.Message;
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return Page();
            }

        }
    }
}
