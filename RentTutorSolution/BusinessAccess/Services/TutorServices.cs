using BusinessAccess.Base;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services
{
    public interface ITutorServices
    {
        Task<IBusinessResult> DeleteUser(int id);
        Task<IBusinessResult> AcceptTutor(int id);
        Task<IBusinessResult> RegisterTutor(Tutor user);
        Task<IBusinessResult> GetAllTutorsPending(int page, int size);
        Task<IBusinessResult> SearchPending(string searchTerm, int page, int size);
        Task<IBusinessResult> GetTutorById(int id);
        Task<IBusinessResult> GetAllTutors();
        Task<IBusinessResult> UpdateTutorAndUserAsync(Tutor updatedTutor);
        Task<IBusinessResult> GetById(int id);

    }

    public class TutorServices : ITutorServices
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public TutorServices(IEmailService emailService)
        {
            _unitOfWork ??= new UnitOfWork();
            _emailService = emailService;
        }


        public async Task<IBusinessResult> DeleteUser(int id)
        {
            try
            {
                var removeUser = await _unitOfWork.TutorRepository.RemoveStudentAsync(id);
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

        public async Task<IBusinessResult> GetAllTutorsPending(int page, int size)
        {
            try
            {
                var users = await _unitOfWork.TutorRepository.GetPagingListAsync(
                   selector: x => x,
                   predicate: x => x.TutorNavigation.Role == "Tutor" && x.TutorNavigation.Status == "Pending",
                   page: page,
                   size: size, 
                   include: x => x.Include(p => p.TutorNavigation).Include(p => p.UserApprovalLogs)
                   );
                if (users != null)
                {
                    return new BusinessResult(1, "Get all tutors successfully", users);
                }
                else
                {
                    return new BusinessResult(-1, "Get all turtors fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> AcceptTutor(int id)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
                if (user == null)
                {
                    return new BusinessResult(-1, "User not found");
                }

                user.Status = "Active";
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.UserRepository.SaveAsync();

                var subject = "Comming Website Rentutor";  // Set the email's subject to the decision
                var body = $"Dear {user.FullName},\n\n" +
                           $"Your request to register as a tutor has been Approve.\n\n" +
                           $"Please create course and invite students to app\n\n" +
                           $"Best regards,\nThe Admin Team";

                await _emailService.SendEmailAsync(user.Email, subject, body);
                return new BusinessResult(1, "User activated successfully");
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> RegisterTutor(Tutor user)
        {
            try
            {
                // Kiểm tra email có tồn tại trong database không
                var existingUser = await _unitOfWork.TutorRepository.FindAsync(u => u.TutorNavigation.Email == user.TutorNavigation.Email);
                var existingTutor = await _unitOfWork.StudentRepository.FindAsync(u => u.StudentNavigation.Email == user.TutorNavigation.Email);
                if (existingUser != null || existingTutor != null)
                {
                    return new BusinessResult(-2, "Email is already registered");
                }

                user.TutorNavigation.Role = "Tutor";
                user.TutorNavigation.UpdatedAt = DateTime.Now;
                user.TutorNavigation.CreatedAt = DateTime.Now;
                user.TutorNavigation.Status = "Pending";
                var newUser = await _unitOfWork.TutorRepository.CreateAsync(user);
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

        public async Task<IBusinessResult> SearchPending(string searchTerm, int page, int size)
        {
            try
            {
                var user = await _unitOfWork.TutorRepository.GetPagingListAsync(
                    selector: x => x,
                    predicate: x => x.TutorNavigation.FullName.Contains(searchTerm) && x.TutorNavigation.Role.Equals("Tutor") && x.TutorNavigation.Status == "Pending",
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

        public async Task<IBusinessResult> GetTutorById(int id)
        {
            try
            {
                var tutor = await _unitOfWork.TutorRepository.SingleOrDefaultAsync(
                    selector: x => x,
                    predicate: x => x.TutorId == id,
                    include: x => x.Include(p => p.TutorNavigation).Include(p => p.UserApprovalLogs)
                    );
                if (tutor != null)
                {
                    return new BusinessResult(1, "Get tutor successfully", tutor);
                }
                else
                {
                    return new BusinessResult(-1, "Get tutor fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetAllTutors()
        {
            try
            {
                #region Business rule
                #endregion

                //var currencies = _DAO.GetAll();
                var tutor = await _unitOfWork.TutorRepository.GetAllAsync();
                if (tutor == null)
                {
                    return new BusinessResult(4, "No currency data");
                }
                else
                {
                    return new BusinessResult(1, "Get currency list success", tutor);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> UpdateTutorAndUserAsync(Tutor updatedTutor)
        {
            try
            {
                // Retrieve the current tutor and user from the database
                var existingTutor = await _unitOfWork.TutorRepository.GetByIdAsync(updatedTutor.TutorId);
                var existingUser = await _unitOfWork.UserRepository.GetByIdAsync(updatedTutor.TutorId); // Assuming you have UserId in Tutor

                if (existingTutor == null)
                {
                    return new BusinessResult(-1, "Tutor not found");
                }

                if (existingUser == null)
                {
                    return new BusinessResult(-1, "User not found");
                }

                // Update tutor properties
                existingTutor.Qualifications = updatedTutor.Qualifications;
                existingTutor.Experience = updatedTutor.Experience;
                existingTutor.Specialization = updatedTutor.Specialization;

                // Update user properties (e.g., status)
                existingUser.Status = "Pending";
                existingUser.UpdatedAt = DateTime.Now; 
                // Save the updated tutor and user
                var updatedTutorResult = await _unitOfWork.TutorRepository.UpdateAsync(existingTutor);
                var updatedUserResult = await _unitOfWork.UserRepository.UpdateAsync(existingUser);

                if (updatedTutorResult != null && updatedUserResult != null)
                {
                    return new BusinessResult(1, "Update successfully");
                }
                else
                {
                    return new BusinessResult(-1, "Update failed");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var tutor = await _unitOfWork.TutorRepository.SingleOrDefaultAsync(
                    selector: x => x,
                    predicate: x => x.TutorId == id,
                    include: x => x.Include(p => p.TutorNavigation)
                    );
                if (tutor != null)
                {
                    return new BusinessResult(1, "Get tutor successfully", tutor);
                }
                else
                {
                    return new BusinessResult(-1, "Get tutor fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }
    }
}
