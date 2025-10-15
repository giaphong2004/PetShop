using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Models;
using WebsiteBanHang.Business;
using WebsiteBanHang.Data;

namespace WebsiteBanHang.Controllers
{
    [Authorize]//Yêu cầu đăng nhập choc các Action có trong controller này
    public class OrderController : Controller
    {
        private UserService _userService = new UserService();
        private DonHangService _donHangService = new DonHangService();
        // GET: Order
        public ActionResult Index()
        {
            string userName = User.Identity.Name;
            User currentUser = _userService.GetUserByUsername(userName);
            List<DonHang> model = _donHangService.LayDonHangTheoUserId(currentUser.ID);
            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var donHang = _donHangService.LayDonHangTheoID(id.Value);

            if (donHang == null)
            {
                return HttpNotFound();
            }

            // Tạo ViewModel và đổ dữ liệu từ donHang đã lấy được
            var viewModel = new OrderDetailsViewModel
            {
                DonHang = donHang,
                KhachHang = donHang.KhachHang,
                chiTietDonHangs = donHang.ChiTietDonHangs
            };

            return View(viewModel);
        }

    }
}