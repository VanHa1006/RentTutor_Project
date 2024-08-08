using BusinessAccess.DAO;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Repository
{
    public class LoginRepository
    {
        private readonly UserDAO _userDAO;

        public LoginRepository(UserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        public User CheckLogin(string email, string passwordHash)
        {
            try
            {
                return _userDAO.CheckLogin(email, passwordHash);
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine($"An error occurred in UserRepository.CheckLogin: {ex.Message}");
                // Handle or rethrow exception
                throw;
            }
        }
    }
}
