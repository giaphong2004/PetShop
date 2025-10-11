using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteBanHang.Data;

namespace WebsiteBanHang.Models
{
    public class CartItem
    {
        public SanPham SanPham { get; set; }
        public int soLuong { get; set; }
    }
}