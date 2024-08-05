using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.DAO
{
    public class UserDAO
    {
        private static UserDAO instance = null!;

        private static readonly object instanceLock = new object();
        private readonly SwpW3RentTutorContext _context;

        public UserDAO()
        {
            _context = new SwpW3RentTutorContext();
        }

        public static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }
        public User checkLogin(string userName, string password)
        {
            try
            {
                var check = _context.Users.Where(u => u.Email!.Equals(userName) && u.PasswordHash!.Equals(password)).FirstOrDefault();

                if (check != null)
                {
                    return check;
                }
                throw new Exception();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
