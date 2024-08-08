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
    public class UserRepository : GenericRepository<User>
    {

        public async Task<bool> RemoveCustomerAsync(int userId)
        {
            try
            {
                var user = await _dbSet.FirstOrDefaultAsync(x => x.UserId == userId);
                if (user != null)
                {
                    user.Status = "Disactive";

                    _dbSet.Update(user);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    Console.WriteLine($"Customer with ID {userId} not found.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing customer: {ex.Message}");
                return false;
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                var users = await _dbSet.ToListAsync();
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving all user: {ex.Message}");
                return new List<User>();
            }

        }

        public async Task<List<User>> GetAllStudent()
        {
            try
            {
                var users = await _dbSet.Where(x => x.Role == "Student").ToListAsync();
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving all student: {ex.Message}");
                return new List<User>();
            }

        }

        public async Task<List<User>> GetAllTurtor()
        {
            try
            {
                var users = await _dbSet.Where(x => x.Role == "Tutor").ToListAsync();
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving all tutor: {ex.Message}");
                return new List<User>();
            }

        }
    }
}
