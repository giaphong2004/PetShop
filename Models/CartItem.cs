using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models
{
    public class CartItem
    {
        public SanPham Product { get; set; }
        public int Quantity { get; set; }
        public CartItem(SanPham sanPham, int quantity)
        {
            Product = sanPham;
            Quantity = quantity;
        }
    }
}