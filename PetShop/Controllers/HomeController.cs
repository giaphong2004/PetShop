using System.Collections.Generic;
using System.Web.Mvc;
using WebsiteBanHang.Business;
using WebsiteBanHang.Data;
using System.Net;

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

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPhamService sanPhamService = new SanPhamService();
            SanPham sanPham = sanPhamService.LaySanPhamTheoId(id.Value);

            //ktra sản phẩm có tồn tại k
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
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