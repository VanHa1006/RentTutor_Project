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
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository() { }
        public async Task<List<Order>> GetListOrder()
        {
            return await _context.Orders.Include(x => x.Student).ToListAsync();
        }
    }
}
