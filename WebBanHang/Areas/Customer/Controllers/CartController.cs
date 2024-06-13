using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Models;
using WebBanHang.Helpers;

namespace WebBanHang.Areas.Customer.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CartController (ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("CART");
            if(cart == null)
            {
                cart = new Cart();
            }
            return View(cart);
        }
        public IActionResult AddToCart(int productId)
        {
            var product = _db.Products.FirstOrDefault(x => x.Id == productId);
            if(product != null)
            {
                Cart cart = HttpContext.Session.GetJson<Cart>("cart");
                if(cart == null)
                {
                    cart = new Cart();
                }
                cart.Add(product, 1);
                HttpContext.Session.SetJson("CART", cart);
                //return Json(new { msg = "Success", qty = cart.Quantity });
                return RedirectToAction("Index");
            }else
                return Json(new { msg = "Error" });
        }
        public IActionResult Remove(int productId)
        {
            var product = _db.Products.FirstOrDefault(x => x.Id == productId);
            if (product != null)
            {
                //Lấy cart từ session
                Cart cart = HttpContext.Session.GetJson<Cart>("cart");
                if (cart!= null)
                {
                    cart.Remove(productId);//Xoá sản phẩm
                    HttpContext.Session.SetJson("CART", cart);//lưu cart vào session
                    return RedirectToAction("Index");
                }
            }
                return NotFound();
        }
        public IActionResult Update(int productId, int qty)
        {
            var product = _db.Products.FirstOrDefault(x => x.Id == productId);
            if (product != null)
            {
                //Lấy cart từ session
                Cart cart = HttpContext.Session.GetJson<Cart>("cart");
                if (cart != null)
                {
                    cart.Remove(productId);//Xoá sản phẩm
                    HttpContext.Session.SetJson("CART", cart);//lưu cart vào session
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
