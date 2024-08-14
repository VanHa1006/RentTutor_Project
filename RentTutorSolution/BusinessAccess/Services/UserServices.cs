using Azure;
using BusinessAccess.Base;
using DataAccess.Models;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services
{
    public interface IUserServices
    {
        Task<IBusinessResult> GetAll(int page, int size);
        Task<IBusinessResult> GetByIdAsync(int id);
        Task<IBusinessResult> UpdateAsync(User user);
        Task<IBusinessResult> Save(User user);
        User GetUserById(int userId);
        Task<IBusinessResult> DeleteAsync(int id);
        Task<IBusinessResult> DeleteUser(int id);
        Task<IBusinessResult> Search(string searchTerm, int page, int size);
        Task<IBusinessResult> GetAllStudents(int page, int size);
        Task<IBusinessResult> GetAllStudentsActive(string searchActive,int page, int size);
        Task<IBusinessResult> GetAllTutor(int page, int size);
    }

    public class UserServices : IUserServices
    {
        private readonly UnitOfWork _unitOfWork;

        public UserServices()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> DeleteAsync(int id)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
                if (user != null)
                {
                    var deleted = await _unitOfWork.UserRepository.RemoveAsync(user);
                    if (deleted)
                    {
                        return new BusinessResult(1, "Delete user successfully");
                    }
                    else
                    {
                        return new BusinessResult(0, "Delete user failed");
                    }
                }
                return new BusinessResult(0, "No content");
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetAll(int page, int size)
        {
            try
            {
                //var customer = await _unitOfWork.CustomerRepository.GetAllAsync();
                var users = await _unitOfWork.UserRepository.GetPagingListAsync(
                   selector: x => x,
                   page: page,
                   size: size
                   );
                if (users != null)
                {
                    return new BusinessResult(1, "Get all user successfully", users);
                }
                else
                {
                    return new BusinessResult(-1, "Get all user fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetAllUsers()
        {
            try
            {
                //var currencies = _DAO.GetAll();
                var users = await _unitOfWork.UserRepository.GetAllUsers();
                if (users == null)
                {
                    return new BusinessResult(4, "No user data");
                }
                else
                {
                    return new BusinessResult(1, "Get user list success", users);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> Search(string searchTerm, int page, int size)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetPagingListAsync(
                    selector: x => x,
                    predicate: x => x.Username.Contains(searchTerm) && x.Role.Equals("Student"),
                    page: page,
                    size: size
                    );

                if (user != null)
                {
                    return new BusinessResult(1, "Search successfully", user);
                }
                else
                {
                    return new BusinessResult(1, "Search fail");
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
                var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
                if (user != null)
                {
                    return new BusinessResult(1, "Get user successfully", user);
                }
                else
                {
                    return new BusinessResult(-1, "Get user fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(User user)
        {
            try
            {
                user.Status = "Active";
                user.Role = "Student";
                user.Username = "Student";
                user.Phone = "";
                user.Address = "";
                user.Birthday = DateOnly.MinValue;
                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;
                var newUser = await _unitOfWork.UserRepository.CreateAsync(user);
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

        public async Task<IBusinessResult> UpdateAsync(User customer)
        {
            try
            {
                customer.UpdatedAt = DateTime.Now;
                var newUser = await _unitOfWork.UserRepository.UpdateAsync(customer);
                if (newUser != null)
                {
                    return new BusinessResult(1, "Update successfully");
                }
                else
                {
                    return new BusinessResult(-1, "Update fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> DeleteUser(int id)
        {
            try
            {
                var removeUser = await _unitOfWork.UserRepository.RemoveCustomerAsync(id);
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

        public async Task<IBusinessResult> GetAllTutor(int page, int size)
        {
            try
            {
                //var customer = await _unitOfWork.CustomerRepository.GetAllAsync();
                var users = await _unitOfWork.UserRepository.GetPagingListAsync(
                   selector: x => x,
                   predicate: x => x.Role == "Tutor",
                   page: page,
                   size: size,
                   include: x => x.Include(p => p.Tutor)
                   );
                if (users != null)
                {
                    return new BusinessResult(1, "Get all Tutors successfully", users);
                }
                else
                {
                    return new BusinessResult(-1, "Get all Tutors fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetAllStudents(int page, int size)
        {
            try
            {
                //var customer = await _unitOfWork.CustomerRepository.GetAllAsync();
                var users = await _unitOfWork.UserRepository.GetPagingListAsync(
                   selector: x => x,
                   predicate: x => x.Role == "Student",
                   page: page,
                   size: size
                   );
                if (users != null)
                {
                    return new BusinessResult(1, "Get all students successfully", users);
                }
                else
                {
                    return new BusinessResult(-1, "Get all students fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetAllStudentsActive(string searchActive,int page, int size)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetPagingListAsync(
                selector: x => x,
                    predicate: x => x.Status.Contains(searchActive) && x.Role.Equals("Student"),
                    page: page,
                    size: size
                    );

                if (user != null)
                {
                    return new BusinessResult(1, "Search successfully", user);
                }
                else
                {
                    return new BusinessResult(1, "Search fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public User GetUserById(int userId)
        {
            var user = _unitOfWork.UserRepository.GetById(userId);
            // Fetch tutor data if user is a tutor
            var tutor = user != null ? _unitOfWork.TutorRepository.GetById(userId) : null;
            // Fetch user data from the database
            if (user != null)
            {
                return new User
                {
                    UserId = user.UserId,
                    FullName = user.FullName,
                    Status = user.Status,
                    Role = user.Role,
                    Username = user.Username,
                    Birthday = user.Birthday,
                    Address = user.Address,
                    Phone = user.Phone,

                    Tutor = tutor != null ?new Tutor
                    {
                        TutorId = user.Tutor.TutorId,
                        Qualifications = user.Tutor.Qualifications,
                        Specialization = user.Tutor.Specialization,
                        Experience =user.Tutor.Experience
                    } : null
                    // Map other properties as needed
                };
            }

            return null;
        }
    }
}
