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
        private UserRepository _userRepository;
        private StudentRepository _studentRepository;
        public UnitOfWork() { }

        public UserRepository UserRepository
        {
            get
            {
                return _userRepository ??= new UserRepository();
            }
        }

        public StudentRepository StudentRepository
        {
            get
            {
                return _studentRepository ??= new StudentRepository();
            }
        }
    }
}