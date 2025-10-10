//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using WebsiteBanHang.Models;
//using System.Data;
//using System.Net;
//using System.Data.Entity;

//namespace WebsiteBanHang.Areas.Admin.Controllers
//{
    
//    //[RouteArea("admin")]
//    //[Route("admin)")]
//    //[Route("admin/homeadmin")]
//    public class HomeAdminController : Controller
//    {
//        private CSDL_PetShopEntities db = new CSDL_PetShopEntities();
       
//        // GET: Admin/HomeAdmin
//        public ActionResult Index()
//        {
//            return View();
//        }
//        //public bool KiemTraQuyen(int RoleID)
//        //{
//        //    //Kiểm tra quyền
//        //    //1.count Phân quyền nhân viên trong database 
//        //    CSDL_PetShopEntities db = new CSDL_PetShopEntities();
//        //    User nvSession = (User)Session["user"];
//        //    var count = db.PhanQuyens.Count(m => m.UserID == nvSession.ID & m.RoleID == RoleID);
//        //    if (count == 0)
//        //    {
//        //        return false;
//        //    }
//        //    else
//        //        return true;
//        //}

//        public bool KiemTraQuyen(int RoleID)
//        {
//            // Kiểm tra quyền
//            if (Session["user"] == null)
//            {
//                return false;
//            }

//            using (CSDL_PetShopEntities db = new CSDL_PetShopEntities())
//            {
//                User nvSession = (User)Session["user"];
//                var count = db.PhanQuyens.Count(m => m.UserID == nvSession.ID && m.RoleID == RoleID);
//                return count > 0;
//            }
//        }


//        public ActionResult Danhmucsanpham()
//        {
//            if (KiemTraQuyen(1) == false)
//            {
//                return Redirect("~/Home/Index");
//            }
//            var sanPhams = db.SanPhams.Include(s => s.Loai);
//            return View(sanPhams.ToList());
//        }
//        // GET: SanPhams/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            SanPham sanPham = db.SanPhams.Find(id);
//            if (sanPham == null)
//            {
//                return HttpNotFound();
//            }
//            return View(sanPham);
//        }

//        // GET: SanPhams/Create
//        public ActionResult Create()

//        {
            
//            ViewBag.CatId = new SelectList(db.Loais, "Id", "Tensanpham");
//            return View();
//        }

//        // POST: SanPhams/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "IdSP,TenSP,img,GiaGiam,Tang,CatId,GiaSP")] SanPham sanPham)
//        {
//            if (ModelState.IsValid)
//            {
//                db.SanPhams.Add(sanPham);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.CatId = new SelectList(db.Loais, "Id", "Tensanpham", sanPham.CatId);
//            return View(sanPham);
//        }

//        // GET: SanPhams/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            SanPham sanPham = db.SanPhams.Find(id);
//            if (sanPham == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.CatId = new SelectList(db.Loais, "Id", "Tensanpham", sanPham.CatId);
//            return View(sanPham);
//        }

//        // POST: SanPhams/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "IdSP,TenSP,img,GiaGiam,Tang,CatId,GiaSP")] SanPham sanPham)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(sanPham).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.CatId = new SelectList(db.Loais, "Id", "Tensanpham", sanPham.CatId);
//            return View(sanPham);
//        }

//        // GET: SanPhams/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            SanPham sanPham = db.SanPhams.Find(id);
//            if (sanPham == null)
//            {
//                return HttpNotFound();
//            }
//            return View(sanPham);
//        }

//        // POST: SanPhams/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            SanPham sanPham = db.SanPhams.Find(id);
//            db.SanPhams.Remove(sanPham);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}