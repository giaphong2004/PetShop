using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Data;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class KhachHangController : BaseController
    {
        private CSDL_PetShopEntities _db = new CSDL_PetShopEntities();
        
        // GET: Admin/KhachHang
        public ActionResult Index(string searchTerm = "")
        {
            var query = _db.KhachHangs.AsQueryable();

            // Tìm kiếm theo Tên, Email hoặc Username (nếu có)
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(kh =>
                    kh.TenKH.Contains(searchTerm) ||
                    kh.Email.Contains(searchTerm) ||
                    (kh.User != null && kh.User.UserName.Contains(searchTerm))
                );
            }
            
            var khachHang = query.OrderByDescending(kh => kh.ID).ToList();
            ViewBag.SearchTerm = searchTerm;
            
            return View(khachHang);
        }

        // GET: Admin/KhachHang/Details/5
        public ActionResult Details(int id)
        {
            var khachHang = _db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}