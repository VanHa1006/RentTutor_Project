using BusinessAccess.DAO;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
                var user = await _dbSet.FirstOrDefaultAsync(x => x.UserId == userId && x.Status == "Active");
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

		public async Task<bool> ActiveUserAsync(int userId)
		{
			try
			{
				var user = await _dbSet.FirstOrDefaultAsync(x => x.UserId == userId && x.Status =="Deactive");
				if (user != null)
				{
					user.Status = "Active";

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
    }
}

