using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace WebBanHang.Models
{
    public class Order
    {
        public int Id{ get; set; }
        public DateTime OrderDate{ get; set; }
        [Required]
        public string CustomerName{ get; set; }
        [Required]
        public string Address{ get; set; }
        [Required]
        public string Phone { get; set; }
        public double Total{ get; set; }
        public string State { get; set; }
    }
}
