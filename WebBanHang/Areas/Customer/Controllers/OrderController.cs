using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Helpers;
using WebBanHang.Models;

namespace WebBanHang.Areas.Customer.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;
        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("CART");
            if (cart == null)
            {
                cart = new Cart();
            }
            ViewBag.CART = cart;
            return View();
        }
        public IActionResult ProcessOrder(Order order)
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("CART");
            if (ModelState.IsValid)
            {
                //B1: Lữu trữ đơn hàng
                //B1.1: Thêm order vào csdl
                order.OrderDate = DateTime.Now;
                order.Total = cart.Total;
                order.State = "Pending";
                //Thêm vào csdl
                _db.Orders.Add(order);
                _db.SaveChanges();
                //B1.2: Thêm OrderDetail vào csdl
                foreach(var item in cart.Items)
                {
                    //Tạo đối tượng
                    var orderDetail = new OrderDetail { OrderId=order.Id,ProductId=item.Product.Id, Quantity=item.Quantity};
                    //Thêm vào OrderDetail
                    _db.OrderDetails.Add(orderDetail);
                    _db.SaveChanges();
                }
                //Xoá sesion
                HttpContext.Session.Remove("CART");
                return View("Result");
            }
            ViewBag.CART = cart;
            return View("Index",order);
        }
    }
}
