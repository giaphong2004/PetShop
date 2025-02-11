using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Models;
namespace WebsiteBanHang.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult AllProduct()
        {
            CSDL_PetShopEntities db = new CSDL_PetShopEntities();
            //lấy ra danh sách các sản phẩm
            List<SanPham> ketqua = db.SanPhams.ToList();
            return View(ketqua);
        }
    }
}