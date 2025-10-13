using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteBanHang.Data;
using System.Data.Entity;

namespace WebsiteBanHang.Business
{
    public class DonHangService
    {
        private CSDL_PetShopEntities _db = new CSDL_PetShopEntities();
        public DonHang LayDonHangTheoID(int orderId)
        {
            var donHang = _db.DonHangs
                .Include(dh => dh.KhachHang)
                .Include(dh =>dh.ChiTietDonHangs.Select(ct =>ct.SanPham))
                .FirstOrDefault(dh => dh.ID == orderId);

            return donHang;

        }
    }
}
