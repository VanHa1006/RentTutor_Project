using BusinessAccess.Base;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services
{
    public interface IStudentsBusiness
    {
        Task<IBusinessResult> GetAllPage(int page, int size);
        Task<IList<Student>> GetAllStudentsAsync();
        Task<IBusinessResult> GetStudentById(int id);
        Task<IBusinessResult> Search(string searchTerm, int page, int size);
    }
    public class StudentsBusiness : IStudentsBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly RenTurtorToStudentContext _context;
        public StudentsBusiness(RenTurtorToStudentContext context)
        {
            _unitOfWork ??= new UnitOfWork();
            _context = context;
        }
        public async Task<IBusinessResult> GetAllPage(int page, int size)
        {
            try
            {
                #region Business rule
                #endregion

                //var currencies = _DAO.GetAll();
                var students = await _unitOfWork.UserRepository.GetPagingListAsync(
                    selector: x => x,
                    page: page,
                    size: size,
                    include: x => x.Include(p => p.Username)
                    );
                if (students == null)
                {
                    return new BusinessResult(4, "No student data");
                }
                else
                {
                    return new BusinessResult(1, "Get student list success", students);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IList<Student>> GetAllStudentsAsync()
        {
            return await _context.Students
                .Include(s => s.StudentNavigation)
                .Where(s => s.StudentNavigation.Role == "Student")
                .ToListAsync();
        }

        public async Task<IBusinessResult> GetStudentById(int id)
        {
            try
            {
                var product = await _unitOfWork.StudentsRepository.GetByIdAsync(id);
                if (product != null)
                {
                    return new BusinessResult(1, "Get student successfully", product);
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

        public async Task<IBusinessResult> Search(string searchTerm, int page, int size)
        {
            try
            {
                var Students = await _unitOfWork.StudentsRepository.GetPagingListAsync(
                    selector: x => x,
                    predicate: x => x.StudentId.ToString().Contains(searchTerm),
                    page: page,
                    size: size
                    );

                if (Students != null)
                {
                    return new BusinessResult(1, "Create successfully", Students);
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
    }
}
