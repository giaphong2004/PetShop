//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using WebsiteBanHang.Models;
//using System.Data.Entity;
//namespace WebsiteBanHang.Controllers
//{
//    public class DetailController : Controller
//    {
//        // GET: Product
//        public ActionResult Detail(int id)
//        {
//            CSDL_PetShopEntities dBContext = new CSDL_PetShopEntities();
//            SanPham product = dBContext.SanPhams.FirstOrDefault(x => x.IdSP == id);
//            return View(product);
//        }
//    }
//}