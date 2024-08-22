using BusinessAccess.Base;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessAccess.Services;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace BusinessAccess.Services
{
    public interface IOrderService
    {
        Task<IBusinessResult> GetAll(int page, int size);
        Task<IBusinessResult> GetAllForStudent(int page, int size, int? studentId);
        Task<IBusinessResult> GetAllCourseForStudentStudy(int page, int size, int? studentId);
        Task<IBusinessResult> GetStatusOrdersByStudentId(string status, int page, int size, int? studentId);
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> FindById(int id);
        Task<IBusinessResult> UpdateAsync(Order order);
        Task<IBusinessResult> AcceptRequestForStudent(int id, string decision, string reason);
        Task<IBusinessResult> DoneCourseForStudent(int id);
        Task<IBusinessResult> RejectRequestForStudent(int id, string decision, string reason);
        Task<IBusinessResult> Save(Order order);
        Task<IBusinessResult> DeleteAsync(int id);
        Task<IBusinessResult> GetAllForTutorActive(int page, int size, int? tutorId);
        Task<IBusinessResult> GetAllForTutorPending(int page, int size, int? tutorId);
        Task<IBusinessResult> Search(string searchTerm, int page, int size);
    }

    public class OrderService : IOrderService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly OrderDetailServices _orderDetailBusiness;
        private readonly IEmailService _emailService;

        public OrderService(IEmailService emailService)
        {
            _unitOfWork ??= new UnitOfWork();
            _orderDetailBusiness ??= new OrderDetailServices();
            _emailService = emailService;
        }



        public async Task<IBusinessResult> DeleteAsync(int id)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
                if (order != null)
                {
                    var result = await _unitOfWork.OrderRepository.RemoveAsync(order);
                    if (result)
                        return new BusinessResult(1, "success");
                    else
                        return new BusinessResult(0, "error");
                }
                return new BusinessResult(0, "no content");
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetAllForTutorActive(int page, int size, int? tutorId)
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetPagingListAsync(
                    selector: x => x,
                    page: page,
                    size: size,
                    predicate: x => (x.OrderDetails.Any(od => od.Course.TutorId == tutorId) && x.Status == "Studying") || (x.OrderDetails.Any(od => od.Course.TutorId == tutorId) && x.Status == "Disapprove"),
                    include: x => x.Include(o => o.OrderDetails).ThenInclude(od => od.Course).Include(p => p.Student.StudentNavigation)
                );

                // Assuming Paginate<Order> has an Items or Results property that is IEnumerable<Order>
                if (orders == null || !orders.Items.Any())  // Replace Items with the correct property
                {
                    return new BusinessResult(4, "No order data");
                }
                else
                {
                    return new BusinessResult(1, "Get order list success", orders);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetAllForTutorPending(int page, int size, int? tutorId)
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetPagingListAsync(
                    selector: x => x,
                    page: page,
                    size: size,
                    predicate: x => x.OrderDetails.Any(od => od.Course.TutorId == tutorId) && x.Status == "Pending",
                    include: x => x.Include(o => o.OrderDetails).ThenInclude(od => od.Course).Include(p => p.Student.StudentNavigation)
                );

                // Assuming Paginate<Order> has an Items or Results property that is IEnumerable<Order>
                if (orders == null || !orders.Items.Any())  // Replace Items with the correct property
                {
                    return new BusinessResult(4, "No order data");
                }
                else
                {
                    return new BusinessResult(1, "Get order list success", orders);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> FindById(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return new BusinessResult(4, "No order found");
            }
            else
            {
                return new BusinessResult(1, "Get order success", order);
            }
        }

        public async Task<IBusinessResult> GetAll(int page, int size)
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetPagingListAsync(
                    selector: x => x,
                    page: page,
                    size: size,
                    include: x => x.Include(p => p.OrderDetails)
                    );
                if (orders == null)
                {
                    return new BusinessResult(4, "No order data");
                }
                else
                {
                    //orders = orders.Include(o => o.User).ToList();

                    return new BusinessResult(1, "Get order list success", orders);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetAllAsync();
                if (orders == null)
                {
                    return new BusinessResult(4, "No order data");
                }
                else
                {
                    //orders = orders.Include(o => o.User).ToList();

                    return new BusinessResult(1, "Get order list success", orders);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetAllForStudent(int page, int size, int? studentId)
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetPagingListAsync(
                    selector: x => x,
                    page: page,
                    predicate: x => x.StudentId == studentId,
                    size: size,
                    include: x => x.Include(p => p.OrderDetails).ThenInclude(od => od.Course)
                    );
                if (orders == null)
                {
                    return new BusinessResult(4, "No order data");
                }
                else
                {
                    //orders = orders.Include(o => o.User).ToList();

                    return new BusinessResult(1, "Get order list success", orders);
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
                var order = await _unitOfWork.OrderRepository.SingleOrDefaultAsync(selector: x => x,
                    predicate: x => x.OrderId == id,
                    include: x => x.Include(p => p.OrderDetails));
                if (order == null)
                {
                    return new BusinessResult(4, "No order found");
                }
                else
                {
                    return new BusinessResult(1, "Get order success", order);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }



        public async Task<IBusinessResult> Save(Order order)
        {
            try
            {
                int result = await _unitOfWork.OrderRepository.CreateAsync(order);
                if (result > 0)
                {

                    foreach (var orderDetail in order.OrderDetails)
                    {
                        orderDetail.OrderId = order.OrderId;
                        //await _unitOfWork.OrderDetailRepository.CreateAsync(orderDetail);
                        await _orderDetailBusiness.Add(orderDetail);
                    }
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
                var order = await _unitOfWork.OrderRepository.GetPagingListAsync(
                    selector: x => x,
                    predicate: x => x.Status.Contains(searchTerm),
                    page: page,
                    size: size
                    );

                if (order != null)
                {
                    return new BusinessResult(1, "Create successfully", order);
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

        public async Task<IBusinessResult> UpdateAsync(Order order)
        {
            try
            {
                int result = await _unitOfWork.OrderRepository.UpdateAsync(order);
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

        public async Task<IBusinessResult> AcceptRequestForStudent(int id, string decision, string reason)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.SingleOrDefaultAsync(selector: x => x,
                    predicate: x => x.OrderId == id,
                    include: x => x.Include(x => x.OrderDetails)
                       .ThenInclude(od => od.Course)
                       .Include(x => x.Student.StudentNavigation));

                if (order == null)
                {
                    return new BusinessResult(-1, "Order not found");
                }

                // Assuming there's only one course associated with the order
                var courseName = order.OrderDetails.FirstOrDefault()?.Course?.CourseName;

                order.Status = "Studying";
                _unitOfWork.OrderRepository.Update(order);
                await _unitOfWork.OrderRepository.SaveAsync();

                var subject = $"{decision}";  // Set the email's subject to the course name
                var body = $"Dear {order.Student.StudentNavigation.FullName},\n\n" +
                           $"I have received your course {courseName} support letter.\n\n" +
                           $"I will meet you on this link, be on time so we can get started right away.\n\n" +
                           $"{reason}\n\n" +
                           $"Best regards,\nThe Admin Team";

                await _emailService.SendEmailAsync(order.Student.StudentNavigation.Email, subject, body);
                return new BusinessResult(1, "Send request successfully");
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }

        }

        public async Task<IBusinessResult> RejectRequestForStudent(int id, string decision, string reason)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.SingleOrDefaultAsync(selector: x => x,
                    predicate: x => x.OrderId == id,
                    include: x => x.Include(x => x.OrderDetails)
                       .ThenInclude(od => od.Course)
                       .Include(x => x.Student.StudentNavigation));

                if (order == null)
                {
                    return new BusinessResult(-1, "Order not found");
                }

                // Assuming there's only one course associated with the order
                var courseName = order.OrderDetails.FirstOrDefault()?.Course?.CourseName;

                order.Status = "Disapprove";
                _unitOfWork.OrderRepository.Update(order);
                await _unitOfWork.OrderRepository.SaveAsync();

                var subject = $"{decision}";  // Set the email's subject to the course name
                var body = $"Dear {order.Student.StudentNavigation.FullName},\n\n" +
                           $"I have received your course {courseName} support letter.\n\n" +
                           $"Thank you for your interest in our course. There is currently a problem with this course.\n\n" +
                           $"{reason}\n\n" +
                           $"Best regards,\nThe Admin Team";

                await _emailService.SendEmailAsync(order.Student.StudentNavigation.Email, subject, body);
                return new BusinessResult(1, "Send request successfully");
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetAllCourseForStudentStudy(int page, int size, int? studentId)
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetPagingListAsync(
                    selector: x => x,
                    page: page,
                    predicate: x => x.StudentId == studentId,
                    size: size,
                    include: x => x
                            .Include(p => p.OrderDetails)
                            .ThenInclude(od => od.Course)        
                            .ThenInclude(c => c.Category)          
                            .Include(p => p.OrderDetails)
                            .ThenInclude(od => od.Course)
                            .ThenInclude(c => c.Tutor.TutorNavigation));             
                if (orders == null)
                {
                    return new BusinessResult(4, "No order data");
                }
                else
                {
                    //orders = orders.Include(o => o.User).ToList();

                    return new BusinessResult(1, "Get order list success", orders);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetStatusOrdersByStudentId(string status, int page, int size, int? studentId)
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetPagingListAsync(
                    selector: x => x,
                    page: page,
                    predicate: x => x.StudentId == studentId && x.Status == status,
                    size: size,
                    include: x => x
                            .Include(p => p.OrderDetails)
                            .ThenInclude(od => od.Course)
                            .ThenInclude(c => c.Category)
                            .Include(p => p.OrderDetails)
                            .ThenInclude(od => od.Course)
                            .ThenInclude(c => c.Tutor.TutorNavigation));
                if (orders == null)
                {
                    return new BusinessResult(4, "No order data");
                }
                else
                {
                    //orders = orders.Include(o => o.User).ToList();

                    return new BusinessResult(1, "Get order list success", orders);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> DoneCourseForStudent(int id)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.SingleOrDefaultAsync(selector: x => x,
                    predicate: x => x.OrderId == id,
                    include: x => x.Include(x => x.OrderDetails)
                       .ThenInclude(od => od.Course)
                       .Include(x => x.Student.StudentNavigation));

                if (order == null)
                {
                    return new BusinessResult(-1, "Order not found");
                }

                // Assuming there's only one course associated with the order
                var courseName = order.OrderDetails.FirstOrDefault()?.Course?.CourseName;

                order.Status = "Done";
                _unitOfWork.OrderRepository.Update(order);
                await _unitOfWork.OrderRepository.SaveAsync();
                return new BusinessResult(1, "Send request successfully");
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }
    }
}
