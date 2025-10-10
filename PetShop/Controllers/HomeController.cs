using System.Collections.Generic;
using System.Web.Mvc;
using WebsiteBanHang.Business;
using WebsiteBanHang.Data;

namespace WebsiteBanHang.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SanPhamService SanPhamService = new SanPhamService();
            List<SanPham> danhsachSanPham = SanPhamService.LayTatCaSanPham();
            return View(danhsachSanPham);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}