using BusinessAccess.Services;
using DataAccess.Models;
using DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RentTutorPresentation.Pages.Tutor
{
    public class Student_resquestModel : PageModel
    {
        private readonly IOrderService _orderBusiness;
        private readonly IOrderDetailServices _orderDetailBusiness;

        public Student_resquestModel(IOrderService orderBusiness, IOrderDetailServices orderDetailBusiness)
        {
            _orderBusiness = orderBusiness;
            _orderDetailBusiness = orderDetailBusiness;
        }
        public Paginate<OrderDetail> OrderDetail { get; set; } = default!;
        public Paginate<Order> Order { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty(SupportsGet = true)]

        public int PageIndex { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public int Size { get; set; } = 100;
        public int? TutorId { get; set; }

        public Order Orders { get; set; } = default!;

        private async Task<Paginate<OrderDetail>> GetOrderDetailStudent()
        {
            var result = await _orderDetailBusiness.GetAllForTutorPending(PageIndex, Size, TutorId);
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

        public async Task OnGetAsync()
        {
            TutorId = HttpContext.Session.GetInt32("TutorId");
            if (!TutorId.HasValue)
            {
                // Xử lý trường hợp tutorId không có trong session
            }

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Order = await Search();
            }
            else
            {
                OrderDetail = await GetOrderDetailStudent();
            }
        }

        public async Task<IActionResult> OnPostAcceptAsync(int OrderDetailId, string Decision, string Reason)
        {
            try
            {
                var result = await _orderDetailBusiness.AcceptRequestForStudent(OrderDetailId, Decision, Reason);
                if (result.Status > 0)
                {
                    var user = await _orderBusiness.GetById(OrderDetailId);
                    ViewData["SuccessMessage"] = result.Message;
                    return RedirectToPage("./Orders-manage");
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

        public async Task<IActionResult> OnPostRejectAsync(int OrderDetailId, string Decision, string Reason)
        {
            try
            {
                var result = await _orderDetailBusiness.RejectRequestForStudent(OrderDetailId, Decision, Reason);
                if (result.Status > 0)
                {
                    var user = await _orderBusiness.GetById(OrderDetailId);
                    ViewData["SuccessMessage"] = result.Message;
                    return RedirectToPage("./Orders-manage");
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
