using BusinessAccess.DAO;
using BusinessAccess.Repository;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services
{
    public interface IUserService
    {
        User checkLogin(string userName, string password);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public User checkLogin(string userName, string password) => _userRepository.checkLogin(userName, password);

    }
}
