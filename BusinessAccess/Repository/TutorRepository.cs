﻿using BusinessAccess.DAO;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Repository
{
    public class TutorRepository : GenericRepository<Tutor>
    {
        public async Task<bool> RemoveStudentAsync(int userId)
        {
            try
            {
                var user = await _dbSet.FirstOrDefaultAsync(x => x.TutorId == userId);
                if (user != null)
                {
                    _dbSet.RemoveRange(user);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    Console.WriteLine($"Tutor with ID {userId} not found.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing customer: {ex.Message}");
                return false;
            }
        }

        public async Task<Tutor?> FindAsync(Expression<Func<Tutor, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }
    }
}
