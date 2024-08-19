using BusinessAccess.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RentTutorPresentation.Pages.Admin
{
    public class Order_DetailModel : PageModel
    {
        private readonly RenTurtorToStudentContext _context;
        private readonly IOrderService _orderBusiness;

        public Order_DetailModel(RenTurtorToStudentContext context, IOrderService orderBusiness)
        {
            _context = context;
            _orderBusiness = orderBusiness;
        }

        public Order Order { get; set; } = default!;
        public IEnumerable<OrderDetail> OrderDetails { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = (Order)(await _orderBusiness.GetById(id.Value)).Data;

            if (order == null)
            {
                return NotFound();
            }
            else
            {
                Order = order;
                OrderDetails = await _context.OrderDetails.Where(od => od.OrderId == order.OrderId).ToListAsync();
            }
            return Page();
        }
    }
}
