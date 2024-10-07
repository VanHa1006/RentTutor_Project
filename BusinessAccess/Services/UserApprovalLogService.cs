using BusinessAccess.Base;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services
{
    public interface IUserApprovalLogService
    {
        Task<IBusinessResult> RejectRequestRegisterTutor(int tutorId, string decision, string reason);
    }

    public class UserApprovalLogService : IUserApprovalLogService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public UserApprovalLogService(IEmailService emailService)
        {
            _unitOfWork ??= new UnitOfWork();
            _emailService = emailService;
        }
        public async Task<IBusinessResult> RejectRequestRegisterTutor(int tutorId, string decision, string reason)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(tutorId);
                if (user == null)
                {
                    return new BusinessResult(-1, "User not found");
                }

                user.Status = "RejectRequest";
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.UserRepository.SaveAsync();

                var subject = decision;  // Set the email's subject to the decision
                var body = $"Dear {user.FullName},\n\n" +
                           $"Your tutor registration form has been processed.\n\n" +
                           $"Reason: {reason}\n\n" +
                           $"Best regards,\nThe Admin Team";

                await _emailService.SendEmailAsync(user.Email, subject, body);

                return new BusinessResult(1, "User activated successfully");
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }


        public async Task<IBusinessResult> AgainRequestRegisterTutor(int tutorId)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(tutorId);
                if (user == null)
                {
                    return new BusinessResult(-1, "User not found");
                }

                user.Status = "Pending";
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.UserRepository.SaveAsync();

                return new BusinessResult(1, "User activated successfully");
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }
    }
}