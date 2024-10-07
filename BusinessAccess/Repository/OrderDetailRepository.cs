using BusinessAccess.DAO;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Repository
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>
    {
        public OrderDetailRepository() { }

        public async Task<List<OrderDetail>> GetByOrderId(int id)
        {
            return await _context.OrderDetails.Where(x => x.OrderId == id).ToListAsync();
        }
    }
}
