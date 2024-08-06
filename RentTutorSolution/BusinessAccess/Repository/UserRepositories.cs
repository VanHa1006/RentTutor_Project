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
    public class UserRepositories : GenericRepository<User>
    {
        public UserRepositories() { }

        public async Task<bool> RemoveCustomerAsync(int userId)
        {
            try
            {
                var user = await _dbSet.FirstOrDefaultAsync(x => x.UserId == userId);
                if (user != null)
                {
                    user.Status = "Deactive";

                    _dbSet.Update(user);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    Console.WriteLine($"User with ID {userId} not found.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing user: {ex.Message}");
                return false;
            }
        }

        public async Task<List<User>> GetAllActiveStudents()
        {
            try
            {
                var users = await _dbSet.Where(x => x.Status == ("Active") && x.Role ==("Student")).ToListAsync();
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving active users: {ex.Message}");
                return new List<User>();
            }

        }

    }
}

