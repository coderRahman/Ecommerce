using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ecommerce.Data.Repository.IRepository;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.ViewModels;

namespace Ecommerce.Controllers
{
    public class OrderController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll(string status = null)
        {
            List<OrderDetailsVM> orderListVM = new List<OrderDetailsVM>();

            IEnumerable<OrderHeader> OrderHeaderList;

            if (User.IsInRole(SD.CustomerRole))
            {
                //retrieve all orders for that customer
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                OrderHeaderList = _unitOfWork.OrderHeader.GetAll(u => u.UserId == claim.Value, null, "ApplicationUser");
            }
            else
            {
                OrderHeaderList = _unitOfWork.OrderHeader.GetAll(null, null, "ApplicationUser");
            }

            if (status == "cancelled")
            {
                OrderHeaderList = OrderHeaderList.Where(o => o.Status == SD.StatusCancelled || o.Status == SD.StatusRefunded || o.Status == SD.PaymentStatusRejected);
            }
            else
            {
                if (status == "completed")
                {
                    OrderHeaderList = OrderHeaderList.Where(o => o.Status == SD.StatusCompleted);
                }
                else
                {
                    OrderHeaderList = OrderHeaderList.Where(o => o.Status == SD.StatusReady || o.Status == SD.StatusInProcess || o.Status == SD.StatusSubmitted || o.Status == SD.PaymentStatusPending);
                }
            }

            foreach (OrderHeader item in OrderHeaderList)
            {
                OrderDetailsVM individual = new OrderDetailsVM
                {
                    OrderHeader = item,
                    OrderDetails = _unitOfWork.OrderDetails.GetAll(o => o.OrderId == item.Id).ToList()
                };
                orderListVM.Add(individual);
            }

            return Json(new { data = orderListVM });
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult OrderDetails(int id)
        {

           var  orderDetailsVM = new OrderDetailsVM()
            {
                OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(m => m.Id == id),
                OrderDetails = _unitOfWork.OrderDetails.GetAll(m => m.OrderId == id).ToList()

            };

            orderDetailsVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == orderDetailsVM.OrderHeader.UserId);
            return View(orderDetailsVM);
        }
        [HttpPost]

        
        public IActionResult OrderPrepare(int orderId)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == orderId);
            orderHeader.Status = SD.StatusInProcess;
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
        }

        public IActionResult OrderReady(int orderId)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == orderId);
            orderHeader.Status = SD.StatusReady;
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
        }
        [HttpPost]
        public IActionResult OrderCancel(int orderId)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == orderId);
            orderHeader.Status = SD.StatusCancelled;
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult OrderRefund(int orderId)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == orderId);
            

            orderHeader.Status = SD.StatusRefunded;
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        public IActionResult ManageOrder()
        {
         List<OrderDetailsVM> orderDetailsVM = new List<OrderDetailsVM>();

            List<OrderHeader> OrderHeaderList = _unitOfWork.OrderHeader
                .GetAll(o => o.Status == SD.StatusSubmitted || o.Status == SD.StatusInProcess)
                .OrderByDescending(u => u.PickUpTime).ToList();


            foreach (OrderHeader item in OrderHeaderList)
            {
                OrderDetailsVM individual = new OrderDetailsVM
                {
                    OrderHeader = item,
                    OrderDetails = _unitOfWork.OrderDetails.GetAll(o => o.OrderId == item.Id).ToList()
                };
                orderDetailsVM.Add(individual);
            }
            return View(orderDetailsVM);
        }
    }
}
