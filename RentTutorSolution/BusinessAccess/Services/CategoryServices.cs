using BusinessAccess.Base;
using Common;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services
{
    public interface ICategoryServices
    {
        Task<IBusinessResult> GetAll(int page, int size);
        Task<IBusinessResult> GetByIdAsync(string id);
        Task<IBusinessResult> UpdateAsync(Category category);
        Task<IBusinessResult> Save(Category category);
        Task<IBusinessResult> DeleteAsync(int id);
        Task<IBusinessResult> Search(string searchTerm, int page, int size);
        Task<IBusinessResult> GetAllCategories();
        Task<IBusinessResult> GetById(int id);
    }
    public class CategoryServices : ICategoryServices
    {
        private readonly UnitOfWork _unitOfWork;

        public CategoryServices()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> DeleteAsync(int id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
                if (category != null)
                {
                    var result = await _unitOfWork.CategoryRepository.RemoveAsync(category);
                    if (result)
                        return new BusinessResult(1, "success");
                    else
                        return new BusinessResult(0, "error");
                }
                return new BusinessResult(0, "no content");
            }
            catch (Exception)
            {

                return new BusinessResult(0, "Category has at least 1 course");
            }
        }

        public async Task<IBusinessResult> GetAll(int page, int size)
        {
            try
            {
                #region Business rule
                #endregion

                //var currencies = _DAO.GetAll();
                var category = await _unitOfWork.CategoryRepository.GetPagingListAsync(
                    selector: x => x,
                    page: page,
                    size: size,
                    include: x => x.Include(p => p.Courses)
                    );
                if (category == null)
                {
                    return new BusinessResult(4, "No currency data");
                }
                else
                {
                    return new BusinessResult(1, "Get currency list success", category);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetAllCategories()
        {
            try
            {
                #region Business rule
                #endregion

                //var currencies = _DAO.GetAll();
                var category = await _unitOfWork.CategoryRepository.GetAllAsync();
                if (category == null)
                {
                    return new BusinessResult(4, "No currency data");
                }
                else
                {
                    return new BusinessResult(1, "Get currency list success", category);
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
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
                if (category != null)
                {
                    return new BusinessResult(1, "Get course category successfully", category);
                }
                else
                {
                    return new BusinessResult(-1, "Get course category fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetByIdAsync(string id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.SingleOrDefaultAsync(
                    selector: x => x,
                    predicate: x => x.CategoryId.ToString() == id,
                    include: x => x.Include(p => p.Courses)
                    );
                if (category != null)
                {
                    return new BusinessResult(1, "Get product successfully", category);
                }
                else
                {
                    return new BusinessResult(-1, "Get product fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }


        public async Task<IBusinessResult> Save(Category category)
        {
            try
            {
                int result = await _unitOfWork.CategoryRepository.CreateAsync(category);
                if (result > 0)
                {
                    return new BusinessResult(1, "success");
                }
                else
                {
                    return new BusinessResult(2, "fail");
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
                var productBrands = await _unitOfWork.CategoryRepository.GetPagingListAsync(
                    selector: x => x,
                    predicate: x => x.CategoryName.Contains(searchTerm),
                    page: page,
                    size: size,
                    include: x => x.Include(p => p.Courses)
                    );

                if (productBrands != null)
                {
                    return new BusinessResult(1, "Create successfully", productBrands);
                }
                else
                {
                    return new BusinessResult(1, "Create fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> UpdateAsync(Category category)
        {
            try
            {
                int result = await _unitOfWork.CategoryRepository.UpdateAsync(category);
                if (result > 0)
                {
                    return new BusinessResult(1, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new BusinessResult(2, "fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }
    }
}
