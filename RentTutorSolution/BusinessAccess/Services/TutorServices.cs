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
                    predicate: x => x.TutorId == id
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
    }
}
