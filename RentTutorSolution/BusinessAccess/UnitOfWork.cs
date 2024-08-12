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
        private TutorRepository _tutorRepository;
        private UserApprovalLogRepository _userApprovalLogRepository;
        private CourseRepository _courseRepository;
        public UnitOfWork() { }

        public UserRepository UserRepository
        {
            get
            {
                return _userRepository ??= new UserRepository();
            }
        }

        public CourseRepository CourseRepository
        {
            get
            {
                return _courseRepository ??= new CourseRepository();
            }
        }

        public StudentRepository StudentRepository
        {
            get
            {
                return _studentRepository ??= new StudentRepository();
            }
        }

        public TutorRepository TutorRepository
        {
            get
            {
                return _tutorRepository ??= new TutorRepository();
            }
        }

        public UserApprovalLogRepository UserApprovalLogRepository
        {
            get
            {
                return _userApprovalLogRepository ??= new UserApprovalLogRepository();
            }
        }
    }
}