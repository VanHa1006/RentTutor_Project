using BusinessAccess.Base;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Business
{
	public interface IUserBusiness
	{
		Task<IBusinessResult> GetAll(int page, int size);
		Task<IBusinessResult> Search(string searchTerm, int page, int size);
        Task<IBusinessResult> RegisterTutor(User user);
    }
    public class UserBusiness : IUserBusiness
	{
		private readonly UnitOfWork _unitOfWork;

        public UserBusiness()
		{
			_unitOfWork ??= new UnitOfWork();

        }

		public async Task<IBusinessResult> GetAll(int page, int size)
		{
			try
			{
				var student = await _unitOfWork.UserRepository.GetPagingListAsync(
				   selector: x => x,
                   predicate: x => x.Role == "Student",
                   page: page,
				   size: size
				   );
				if (student != null)
				{
					return new BusinessResult(1, "Get all student successfully", student);
				}
				else
				{
					return new BusinessResult(-1, "Get all student fail");
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
					predicate: x => x.Username.Contains(searchTerm)&& x.Role == "Student",
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

        public async Task<IBusinessResult> RegisterTutor(User user)
        {
            try
            {
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
    }
}