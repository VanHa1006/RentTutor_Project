using BusinessAccess.Base;
using BusinessAccess.DAO;
using BusinessAccess.Repository;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services
{

    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly RenTurtorToStudentContext _context;
        public UserService(UserRepository userRepository, RenTurtorToStudentContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public User CheckLogin(string email, string passwordHash)
        {
            try
            {
                var user = _userRepository.CheckLogin(email, passwordHash);
                if (user == null)
                {
                    // Handle login failure (e.g., invalid credentials)
                    Console.WriteLine("Login failed: User not found or invalid credentials.");
                    // You might want to throw an exception or return a specific result
                }
                return user;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred in UserService.CheckLogin: {ex.Message}");
                // Handle or rethrow the exception
                throw;
            }
        }
    }
}
