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
    public interface IOrderDetailServices
    {
        Task<IBusinessResult> Add(OrderDetail orderDetail);
        Task<IBusinessResult> Update(OrderDetail orderDetail);
        Task<IBusinessResult> Delete(int id);
        Task<IBusinessResult> DeleteByOrderId(int orderId);
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> GetAllForTutorActive(int page, int size, int? tutorId);
        Task<IBusinessResult> AcceptRequestForStudent(int id, string decision, string reason);
        Task<IBusinessResult> RejectRequestForStudent(int id, string decision, string reason);
        Task<IBusinessResult> GetAllForTutorPending(int page, int size, int? tutorId);
        Task<IBusinessResult> GetStatusOrdersByStudentId(string status, int page, int size, int? studentId);
        Task<IBusinessResult> GetAllCourseForStudentStudy(int page, int size, int? studentId);
        Task<IBusinessResult> DoneCourseForStudent(int id);
    }

    public class OrderDetailServices : IOrderDetailServices
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        public OrderDetailServices(IEmailService emailService)
        {
            _unitOfWork ??= new UnitOfWork();
            _emailService = emailService;
        }

        public async Task<IBusinessResult> DoneCourseForStudent(int id)
        {
            try
            {
                var orderDetail = await _unitOfWork.OrderDetailRepository.SingleOrDefaultAsync(
                    selector: x => x,
                    predicate: x => x.OrderDetailId == id,
                    include: x => x.Include(od => od.Course)
                                   .ThenInclude(c => c.Category)
                                   .Include(od => od.Order.Student.StudentNavigation)
                                   .Include(od => od.Course.Tutor.TutorNavigation));

                if (orderDetail == null)
                {
                    return new BusinessResult(-1, "Order Detail not found");
                }

                // Thay đổi trạng thái của OrderDetail thành 'Done'
                orderDetail.Status = "Done";

                // Lưu lại sự thay đổi vào cơ sở dữ liệu
                await _unitOfWork.OrderDetailRepository.UpdateAsync(orderDetail);
                await _unitOfWork.OrderDetailRepository.SaveAsync();

                return new BusinessResult(1, "Send request successfully");
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
                var order = await _unitOfWork.OrderDetailRepository.SingleOrDefaultAsync(
                    selector: x => x,
                    predicate: x => x.OrderDetailId == id,
                    include: x => x.Include(od => od.Course)
                                   .Include(x => x.Order.Student.StudentNavigation));

                if (order == null)
                {
                    return new BusinessResult(-1, "Order Detail not found");
                }

                // Thay đổi trạng thái của OrderDetail thành 'Studying'
                order.Status = "Studying";

                // Lưu lại sự thay đổi vào cơ sở dữ liệu
                await _unitOfWork.OrderDetailRepository.UpdateAsync(order);
                await _unitOfWork.OrderRepository.SaveAsync();

                // Assuming there's only one course associated with the order
                var courseName = order.Course?.CourseName;

                var subject = $"{decision}";  // Đặt tiêu đề email là tên khóa học
                var body = $"Dear {order.Order.Student.StudentNavigation.FullName},\n\n" +
                           $"I have received your course {courseName} support letter.\n\n" +
                           $"I will meet you on this link, be on time so we can get started right away.\n\n" +
                           $"{reason}\n\n" +
                           $"Best regards,\nThe Admin Team";

                await _emailService.SendEmailAsync(order.Order.Student.StudentNavigation.Email, subject, body);

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
                var order = await _unitOfWork.OrderDetailRepository.SingleOrDefaultAsync(
                    selector: x => x,
                    predicate: x => x.OrderDetailId == id,
                    include: x => x.Include(od => od.Course)
                                   .Include(x => x.Order.Student.StudentNavigation));

                if (order == null)
                {
                    return new BusinessResult(-1, "Order Detail not found");
                }

                // Thay đổi trạng thái của OrderDetail thành 'Studying'
                order.Status = "Disapprove";

                // Lưu lại sự thay đổi vào cơ sở dữ liệu
                await _unitOfWork.OrderDetailRepository.UpdateAsync(order);
                await _unitOfWork.OrderRepository.SaveAsync();

                // Assuming there's only one course associated with the order
                var courseName = order.Course?.CourseName;

                var subject = $"{decision}";  // Đặt tiêu đề email là tên khóa học
                var body = $"Dear {order.Order.Student.StudentNavigation.FullName},\n\n" +
                           $"I have received your course {courseName} support letter.\n\n" +
                           $"I will meet you on this link, be on time so we can get started right away.\n\n" +
                           $"{reason}\n\n" +
                           $"Best regards,\nThe Admin Team";

                await _emailService.SendEmailAsync(order.Order.Student.StudentNavigation.Email, subject, body);

                return new BusinessResult(1, "Send request successfully");
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> Add(OrderDetail orderDetail)
        {
            try
            {
                int result = await _unitOfWork.OrderDetailRepository.CreateAsync(orderDetail);
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

        public async Task<IBusinessResult> Delete(int id)
        {
            try
            {
                var orderDetail = await _unitOfWork.OrderDetailRepository.GetByIdAsync(id);
                if (orderDetail != null)
                {
                    var result = await _unitOfWork.OrderDetailRepository.RemoveAsync(orderDetail);
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

        public async Task<IBusinessResult> DeleteByOrderId(int orderId)
        {
            try
            {
                var orderDetails = await _unitOfWork.OrderDetailRepository.GetByOrderId(orderId);
                if (orderDetails != null)
                {
                    foreach (var orderDetail in orderDetails)
                    {
                        await _unitOfWork.OrderDetailRepository.RemoveAsync(orderDetail);
                    }

                    return new BusinessResult(1, "success");
                }
                else
                {
                    // Trường hợp không tìm thấy OrderDetails
                    return new BusinessResult(0, "No order details found for the given order ID.");
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
                var orderDetails = await _unitOfWork.OrderDetailRepository.GetAllAsync();
                if (orderDetails == null)
                {
                    return new BusinessResult(4, "No order detail data");
                }
                else
                {
                    return new BusinessResult(1, "Get order detail list success", orderDetails);
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
                // Lấy thông tin chi tiết đơn hàng
                var orderDetail = await _unitOfWork.OrderDetailRepository.GetByIdAsync(id);
                if (orderDetail == null)
                {
                    return new BusinessResult(4, "No order detail found");
                }

                // Lấy thông tin sản phẩm (khóa ngoại tới bảng Course)
                var course = await _unitOfWork.CourseRepository.GetByIdAsync(orderDetail.CourseId);
                if (course == null)
                {
                    return new BusinessResult(4, "No course found for this order detail");
                }

                // Trả về thông tin đơn hàng kèm theo thông tin sản phẩm
                var resultData = new
                {
                    OrderDetail = orderDetail,
                    Course = course
                };

                return new BusinessResult(1, "Get order detail and course info success", resultData);
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> Update(OrderDetail orderDetail)
        {
            try
            {
                int result = await _unitOfWork.OrderDetailRepository.UpdateAsync(orderDetail);
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

        public async Task<IBusinessResult> GetAllForTutorActive(int page, int size, int? tutorId)
        {
            try
            {
                var orders = await _unitOfWork.OrderDetailRepository.GetPagingListAsync(
                    selector: x => x,
                    page: page,
                    size: size,
                    predicate: x => x.Course.TutorId == tutorId &&
                                    (x.Status == "Studying" || x.Status == "Disapprove" || x.Status == "Done"),
                    include: x => x.Include(od => od.Course)
                                   .Include(od => od.Order)
                                   .ThenInclude(o => o.Student.StudentNavigation)
                );

                // Kiểm tra xem có bất kỳ order nào sau khi lọc
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
                var orders = await _unitOfWork.OrderDetailRepository.GetPagingListAsync(
                    selector: x => x,
                    page: page,
                    size: size,
                    predicate: x => x.Course.TutorId == tutorId &&
                                    (x.Status == "Pending"),
                    include: x => x.Include(od => od.Course)
                                   .Include(od => od.Order)
                                   .ThenInclude(o => o.Student.StudentNavigation)
                );

                // Kiểm tra xem có bất kỳ order nào sau khi lọc
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

        public async Task<IBusinessResult> GetStatusOrdersByStudentId(string status, int page, int size, int? studentId)
        {
            try
            {
                var orders = await _unitOfWork.OrderDetailRepository.GetPagingListAsync(
                    selector: x => x,
                    page: page,
                    predicate: x => x.Order.StudentId == studentId && x.Status == status,
                    size: size,
                    include: x => x
                            .Include(od => od.Course)
                            .ThenInclude(c => c.Category)
                            .Include(od => od.Course)
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

        public async Task<IBusinessResult> GetAllCourseForStudentStudy(int page, int size, int? studentId)
        {
            try
            {
                var orders = await _unitOfWork.OrderDetailRepository.GetPagingListAsync(
                    selector: x => x,
                    page: page,
                    predicate: x => x.Order.StudentId == studentId,
                    size: size,
                    include: x => x
                            .Include(od => od.Course)
                            .ThenInclude(c => c.Category)
                            .Include(od => od.Course)
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
    }
}
