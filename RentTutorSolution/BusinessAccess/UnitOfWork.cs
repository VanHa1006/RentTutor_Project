using BusinessAccess.Repository;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess
{
    public class UnitOfWork
    {
        private RenTurtorToStudentContext _unitOfWorkContext;
        private UserRepositories _userRepository;

        public UnitOfWork() { }

        public UserRepositories UserRepository
        {
            get
            {
                return _userRepository ??= new UserRepositories();
            }
        }
    }
}
