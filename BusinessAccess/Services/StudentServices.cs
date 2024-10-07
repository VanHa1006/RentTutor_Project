using BusinessAccess.Base;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services
{
    public interface IStudentServices
    {
        Task<IBusinessResult> DeleteUser(int id);
        Task<IBusinessResult> GetByIdAsync(int id);
        Task<IBusinessResult> Save(Student user);
    }

    public class StudentServices : IStudentServices
    {
        private readonly UnitOfWork _unitOfWork;

        public StudentServices()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> DeleteUser(int id)
        {
            try
            {
                var removeUser = await _unitOfWork.StudentRepository.RemoveStudentAsync(id);
                if (removeUser != null)
                {
                    return new BusinessResult(1, "Remove successfully");
                }
                else
                {
                    return new BusinessResult(-1, "Remove fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetByIdAsync(int id)
        {
            try
            {
                var student = await _unitOfWork.StudentRepository.GetByIdAsync(id);
                if (student != null)
                {
                    return new BusinessResult(1, "Get student successfully", student);
                }
                else
                {
                    return new BusinessResult(-1, "Get student fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(Student user)
        {
            try
            {
                // Kiểm tra email có tồn tại trong database không
                var existingUser = await _unitOfWork.StudentRepository.FindAsync(u => u.StudentNavigation.Email == user.StudentNavigation.Email);
                var existingTutor = await _unitOfWork.TutorRepository.FindAsync(u => u.TutorNavigation.Email == user.StudentNavigation.Email);
                if (existingUser != null || existingTutor != null)
                {
                    return new BusinessResult(-2, "Email is already registered");
                }

                user.StudentNavigation.Status = "Active";
                user.StudentNavigation.Role = "Student";
                user.StudentNavigation.Phone = "";
                user.StudentNavigation.Address = "";
                user.StudentNavigation.Username = "User";
                user.StudentNavigation.Birthday = DateOnly.MinValue;
                user.StudentNavigation.CreatedAt = DateTime.Now;
                user.StudentNavigation.UpdatedAt = DateTime.Now;

                var newUser = await _unitOfWork.StudentRepository.CreateAsync(user);
                if (newUser >= 1)
                {
                    return new BusinessResult(1, "Create successfully");
                }
                else
                {
                    return new BusinessResult(-1, "Create fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }
    }
}
