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
    }
}
