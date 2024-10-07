using BusinessAccess.Services;
using DataAccess.Models;
using DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RentTutorPresentation.Pages.Tutor
{
    public class Orders_manageModel : PageModel
    {
        private readonly IOrderDetailServices _orderDetailServices;

        public Orders_manageModel(IOrderDetailServices orderDetailServices)
        {
            _orderDetailServices = orderDetailServices;
        }

        public Paginate<Order> Order { get; set; } = default!;
        public Paginate<OrderDetail> OrderDetail { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty(SupportsGet = true)]

        public int PageIndex { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public int Size { get; set; } = 100;
        public int? TutorId { get; set; }

        private async Task<Paginate<OrderDetail>> GetOrderDetail()
        {
            var result = await _orderDetailServices.GetAllForTutorActive(PageIndex, Size, TutorId);
            if (result.Status > 0 && result.Data != null)
            {
                var order = result.Data;
                return (Paginate<OrderDetail>)order;
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
            else
            {
                OrderDetail = await GetOrderDetail();
            }
        }
    }
}
