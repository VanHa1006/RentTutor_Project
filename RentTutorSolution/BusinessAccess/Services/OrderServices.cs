﻿using BusinessAccess.Base;
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
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> FindById(int id);
        Task<IBusinessResult> UpdateAsync(Order order);
        Task<IBusinessResult> Save(Order order);
        Task<IBusinessResult> DeleteAsync(int id);
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
    }
}
