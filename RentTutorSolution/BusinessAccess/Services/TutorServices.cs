﻿using BusinessAccess.Base;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
    }

    public class TutorServices : ITutorServices
    {
        private readonly UnitOfWork _unitOfWork;

        public TutorServices()
        {
            _unitOfWork ??= new UnitOfWork();
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
    }
}
