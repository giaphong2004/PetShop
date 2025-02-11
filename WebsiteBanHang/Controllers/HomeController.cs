using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Models;



namespace WebsiteBanHang.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CSDL_PetShopEntities db = new CSDL_PetShopEntities();
            //lấy ra danh sách các sản phẩm
            List<SanPham> ketqua  = db.SanPhams.Take(10).ToList();
            return View(ketqua);
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