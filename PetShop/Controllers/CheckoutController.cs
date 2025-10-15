using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Models;
using WebsiteBanHang.Data;
using WebsiteBanHang.Business;

namespace WebsiteBanHang.Controllers
{
    public class CheckoutController : Controller
    {
        private CSDL_PetShopEntities _db = new CSDL_PetShopEntities();
        private UserService _userService = new UserService();
        // GET: Checkout
        public ActionResult Index()
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem>;
            if(cart ==null || !cart.Any())
            {
                return RedirectToAction("Index", "Home");
            }

            return View(cart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceOrder(string TenKH,string SDT,string Email,string DiaChi)
        {
            var cart = Session["Cart"] as List<CartItem>;
            if(cart == null || !cart.Any())
            {
                return RedirectToAction("Index", "Home");
            }
            DonHang donHang = null;
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    // 2. Tạo và lưu thông tin khách hàng mới
                    var khachHang = new KhachHang
                    {
                        TenKH = TenKH,
                        SDT = SDT,
                        Email = Email,
                        DiaChi = DiaChi
                    };

                    //KIỂM TRA NẾU NGƯỜI DÙNG ĐÃ ĐĂNG NHẬP
                    if (Request.IsAuthenticated) 
                    {
                        var user = _userService.GetUserByUsername(User.Identity.Name);
                        if (user != null)
                        {
                            khachHang.UserID = user.ID;
                        }
                    }
                    _db.KhachHangs.Add(khachHang);
                    _db.SaveChanges(); // Lưu để lấy được ID của khách hàng mới

                    // 3. Tạo và lưu đơn hàng
                    donHang = new DonHang
                    {
                        KhachHangID = khachHang.ID,
                        NgayDat = System.DateTime.Now,
                        TrangThai = "Đang xử lý"
                    };
                    _db.DonHangs.Add(donHang);
                    _db.SaveChanges(); // Lưu để lấy được ID của đơn hàng mới

                    // 4. Lưu chi tiết đơn hàng (các sản phẩm trong giỏ)
                    foreach (var item in cart)
                    {
                        var chiTiet = new ChiTietDonHang
                        {
                            DonHangID = donHang.ID,
                            SanPhamID = item.SanPham.ID,
                            SoLuong = item.soLuong,
                            DonGia = item.SanPham.GiaSP
                        };
                        _db.ChiTietDonHangs.Add(chiTiet);
                    }
                    _db.SaveChanges();

                    // Nếu tất cả các bước trên thành công, xác nhận transaction
                    transaction.Commit();

                    // 5. Xóa giỏ hàng sau khi đã đặt hàng thành công
                    Session.Remove("Cart");

                }
                catch (System.Exception)
                {
                    // Nếu có bất kỳ lỗi nào xảy ra, hủy bỏ tất cả thay đổi
                    transaction.Rollback();
                    // Có thể thêm trang báo lỗi ở đây
                    return View("Error");
                }
            }
            return RedirectToAction("OrderSuccess", new { orderId = donHang.ID });
        }

        public ActionResult OrderSuccess(int orderId)
        {
            ViewBag.orderId = orderId;
            return View();
        }
    }
}