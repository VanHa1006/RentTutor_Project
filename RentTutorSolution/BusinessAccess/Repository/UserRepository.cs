using BusinessAccess.DAO;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Repository
{
    public interface IUserRepository
    {
        User checkLogin(string userName, string password);
    }

    public class UserRepository : IUserRepository
    {

        public User checkLogin(string userName, string password) => UserDAO.Instance.checkLogin(userName, password);
    }
}
