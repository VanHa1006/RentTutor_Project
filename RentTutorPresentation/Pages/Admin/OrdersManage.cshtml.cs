using BusinessAccess.Services;
using DataAccess.Models;
using DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RentTutorPresentation.Pages.Admin
{
    public class AdsTutorManageModel : PageModel
    {
        private readonly IOrderService _orderBusiness;

        public AdsTutorManageModel(IOrderService orderBusiness)
        {
            _orderBusiness = orderBusiness;
        }

        public Paginate<Order> Order { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty(SupportsGet = true)]

        public int PageIndex { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public int Size { get; set; } = 5;


        private async Task<Paginate<Order>> GetOrder()
        {
            var result = await _orderBusiness.GetAll(PageIndex, Size);
            if (result.Status > 0 && result.Data != null)
            {
                var order = result.Data;
                return (Paginate<Order>)order;
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
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Order = await Search();
            }
            else
            {
                Order = await GetOrder();
            }
        }
    }
}

