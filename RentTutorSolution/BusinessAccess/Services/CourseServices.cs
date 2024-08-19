using BusinessAccess.Base;
using Common;
using DataAccess.Models;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services
{
    public interface ICourseServices
    {
        Task<IBusinessResult> GetAll(int page, int size);
        Task<IBusinessResult> GetAllCourses();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> FindId(int id);
        Task<IBusinessResult> UpdateAsync(Course course);
        Task<IBusinessResult> Save(Course course);
        Task<IBusinessResult> DeleteAsync(int id);
        Task<IBusinessResult> Search(string searchTerm, int page, int size);
    }

    public class CourseService : ICourseServices
    {
        private readonly UnitOfWork _unitOfWork;
        public CourseService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> DeleteAsync(int id)
        {
            try
            {
                var course = await _unitOfWork.CourseRepository.GetByIdAsync(id);
                if (course != null)
                {
                    var result = await _unitOfWork.CourseRepository.RemoveAsync(course);
                    if (result)
                        return new BusinessResult(1, "success");
                    else
                        return new BusinessResult(0, "error");
                }
                return new BusinessResult(0, "no content");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IBusinessResult> GetAll(int page, int size)
        {
            try
            {
                #region Business rule
                #endregion

                //var currencies = _DAO.GetAll();
                var courses = await _unitOfWork.CourseRepository.GetPagingListAsync(
                    selector: x => x,
                    page: page,
                    predicate: x => x.Status.Equals("Active"),
                    size: size,
                    include: x => x.Include(p => p.Category).Include(p => p.Tutor.TutorNavigation)
                    );
                if (courses == null)
                {
                    return new BusinessResult(4, "No currency data");
                }
                else
                {
                    return new BusinessResult(1, "Get currency list success", courses);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetAllCourses()
        {
            try
            {
                #region Business rule
                #endregion

                //var currencies = _DAO.GetAll();
                var courses = await _unitOfWork.CourseRepository.GetAllAsync();
                if (courses == null)
                {
                    return new BusinessResult(4, "No currency data");
                }
                else
                {
                    return new BusinessResult(1, "Get currency list success", courses);
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
                var course = await _unitOfWork.CourseRepository.SingleOrDefaultAsync(
                    selector: x => x,
                    predicate: x => x.CourseId == id,
                    include: x => x.Include(p => p.Category).Include(p => p.Tutor.TutorNavigation).Include(p => p.Tutor)
                    );
                if (course != null)
                {
                    return new BusinessResult(1, "Get course successfully", course);
                }
                else
                {
                    return new BusinessResult(-1, "Get course fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }
        public async Task<IBusinessResult> FindId(int id)
        {
            try
            {
                var course = await _unitOfWork.CourseRepository.GetByIdAsync(id);
                if (course != null)
                {
                    return new BusinessResult(1, "Get course successfully", course);
                }
                else
                {
                    return new BusinessResult(-1, "Get course fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(Course course)
        {
            try
            {
                int result = await _unitOfWork.CourseRepository.CreateAsync(course);
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
                var courses = await _unitOfWork.CourseRepository.GetPagingListAsync(
                    selector: x => x,
                    predicate: x => x.CourseName.Contains(searchTerm),
                    page: page,
                    size: size,
                    include: x => x.Include(p => p.Category)
                    );

                if (courses != null)
                {
                    return new BusinessResult(1, "Search successfully", courses);
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

        public async Task<IBusinessResult> UpdateAsync(Course course)
        {
            try
            {
                int result = await _unitOfWork.CourseRepository.UpdateAsync(course);
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
