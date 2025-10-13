using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteBanHang.Data;

namespace WebsiteBanHang.Models
{
    public class OrderDetailsViewModel
    {
        public DonHang DonHang { get; set; }
        public KhachHang KhachHang { get; set; }

        public IEnumerable<ChiTietDonHang> chiTietDonHangs { get; set; }
    }
}