using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBanHang.Models
{
    //lớp biểu diễn 1 phần tử của giỏ hàng
    public class CartItem
    {

        public Product Product { get; set; }
        public int Quantity{ get; set; }
    }
    public class Cart
    {
        private List<CartItem> _items;
        public Cart()
        {
            _items = new List<CartItem>();
        }
        public List<CartItem> Items{ get { return _items; } }
        //Các phương thức xử lý trên Cart
        //Thêm sản phẩm vào giỏ
        public void Add(Product p, int qty)
        {
            var item = _items.FirstOrDefault(x => x.Product.Id == p.Id);
            if(item == null)
            {
                _items.Add(new CartItem { Product = p, Quantity =qty});
            }
            else
            {
                item.Quantity += 1;
            }
        }
        //Cập nhật số lượng sản phẩm vào giỏ
        public void Update (int productId, int qty)
        {
            var item = _items.FirstOrDefault(x => x.Product.Id == productId);
            if (item != null)//Nếu tồn tại
            {
                if (qty > 0)
                {
                    item.Quantity = qty;
                }
                else
                {
                    _items.Remove(item);
                }
            }
            else
            {
                item.Quantity += 1;
            }
        }
        //Xoá sản phẩm
        public void Remove (int productId)
        {
            var item = _items.FirstOrDefault(x => x.Product.Id == productId);
            if (item != null)//Nếu tồn tại
            {
                _items.Remove(item);
            }
        }
        //PHương thức tính tổng thành tiền
        public double Total
        {
            get
            {
                double total = _items.Sum(x => x.Quantity * x.Product.Price);
                return total;
            }
        }
        //Phương thức tính tổng số lượng sản phẩm trong giỏ
        public double Quantity
        {
            get
            {
                double total = _items.Sum(x => x.Quantity);
                return total;
            }
        }
    }
}
