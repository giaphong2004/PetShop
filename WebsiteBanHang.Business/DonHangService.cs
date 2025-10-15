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
                .Include(dh => dh.ChiTietDonHangs.Select(ct => ct.SanPham))
                .FirstOrDefault(dh => dh.ID == orderId);

            return donHang;

        }

        public List<DonHang> LayDonHangTheoUserId(int userId)
        {
            var khachHangIds = _db.KhachHangs
                         .Where(kh => kh.UserID == userId)
                         .Select(kh => kh.ID)
                         .ToList();

            // 2. Nếu không có ID nào, trả về danh sách rỗng.
            if (!khachHangIds.Any())
            {
                return new List<DonHang>();
            }

            // 3. Tìm TẤT CẢ các đơn hàng có KhachHangID nằm trong danh sách ID ở trên.
            return _db.DonHangs
                      .Where(dh => khachHangIds.Contains(dh.KhachHangID))
                      .OrderByDescending(dh => dh.NgayDat)
                      .ToList();
        }
    }
}
