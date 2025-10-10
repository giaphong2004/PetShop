using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteBanHang.Data;

namespace WebsiteBanHang.Business
{
    public class SanPhamService
    {
        private CSDL_PetShopEntities _db = new CSDL_PetShopEntities();

        // Phương thức để lấy về TẤT CẢ sản phẩm
        public List<SanPham> LayTatCaSanPham()
        {
            // Truy cập vào bảng SanPhams trong DB, và chuyển nó thành một danh sách (List)
            return _db.SanPhams.ToList();
        }
    }
}
