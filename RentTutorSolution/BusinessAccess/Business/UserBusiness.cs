using BusinessAccess.Base;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Business
{
	public interface IUserBusiness
	{
		Task<IBusinessResult> GetAllStudent(int page, int size);
		Task<IBusinessResult> GetByIdAsync(int id);
		Task<IBusinessResult> DeactiveUser(int id);
		Task<IBusinessResult> ActiveUser(int id);
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
		public async Task<IBusinessResult> DeactiveUser(int id)
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

		public async Task<IBusinessResult> ActiveUser(int id)
		{
			try
			{
				var removeUser = await _unitOfWork.UserRepository.ActiveUserAsync(id);
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

		public async Task<IBusinessResult> GetAllStudent(int page, int size)
		{
			try
			{
				var customer = await _unitOfWork.UserRepository.GetPagingListAsync(
				   selector: x => x,
				   page: page,
				   size: size
				   );
				if (customer != null)
				{
					return new BusinessResult(1, "Get all user successfully", customer);
				}
				else
				{
					return new BusinessResult(-1, "Get all customer fail");
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
				var customer = await _unitOfWork.UserRepository.GetByIdAsync(id);
				if (customer != null)
				{
					return new BusinessResult(1, "Get user successfully", customer);
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

		public async Task<IBusinessResult> Search(string searchTerm, int page, int size)
		{
			try
			{
				var user = await _unitOfWork.UserRepository.GetPagingListAsync(
					selector: x => x,
					predicate: x => x.FullName.Contains(searchTerm),
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