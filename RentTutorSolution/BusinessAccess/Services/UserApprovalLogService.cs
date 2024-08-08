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

        public UserApprovalLogService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> RejectRequestRegisterTutor(int tutorId, string decision, string reason)
        {
            try
            {
                // Step 1: Change the user's status to "Student"
                var user = await _unitOfWork.UserRepository.GetByIdAsync(tutorId);
                if (user == null)
                {
                    return new BusinessResult(-1, "User not found");
                }

                user.Status = "Active";
                user.Role = "Student";
                _unitOfWork.UserRepository.Update(user);

                // Step 2: Create and save the approval log entry
                var approvalLog = new UserApprovalLog
                {
                    TutorId = tutorId,
                    Decision = decision,
                    Reason = reason,
                    AdminId = 1,
                    DecisionDate = DateTime.Now,
                };
                _unitOfWork.UserApprovalLogRepository.Create(approvalLog);

                // Step 3: Save all changes to the database
                await _unitOfWork.UserApprovalLogRepository.SaveAsync();

                return new BusinessResult(1, "Approval log saved and user status updated successfully");
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }
    }
}