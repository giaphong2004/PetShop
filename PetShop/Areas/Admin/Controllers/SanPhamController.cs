using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Data;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class SanPhamController : BaseController
    {
        private CSDL_PetShopEntities _db = new CSDL_PetShopEntities();

        // GET: Admin/SanPham
        public ActionResult Index(string searchTerm = "", int? loaiId = null)
        {
            var query = _db.SanPhams.AsQueryable();
            ViewBag.LoaiColors = new Dictionary<int, string>
            {
                { 1, "#0d6efd" },  // Thức ăn - Xanh dương
                { 2, "#198754" },  // Đồ chơi - Xanh lá
                { 3, "#dc3545" },  // Phụ kiện - Đỏ
                { 4, "#ffc107" },  // Thuốc - Vàng
                { 5, "#6f42c1" }   // Khác - Tím
            };

            // Tìm ki?m theo tên s?n ph?m
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(sp => sp.TenSP.Contains(searchTerm));
            }

            // L?c theo lo?i s?n ph?m
            if (loaiId.HasValue && loaiId.Value > 0)
            {
                query = query.Where(sp => sp.LoaiID == loaiId.Value);
            }

            var products = query
                .OrderByDescending(sp => sp.ID)
                .ToList();

            // L?y danh sách lo?i s?n ph?m cho dropdown
            ViewBag.LoaiList = _db.Loais.ToList();
            ViewBag.SearchTerm = searchTerm;
            ViewBag.LoaiId = loaiId;

            return View(products);
        }

        // GET: Admin/SanPham/Details/5
        public ActionResult Details(int id)
        {
            var product = _db.SanPhams.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            
            // Định nghĩa màu cho từng loại sản phẩm
            ViewBag.LoaiColors = new Dictionary<int, string>
            {
                { 1, "#0d6efd" },  // Thức ăn - Xanh dương
                { 2, "#198754" },  // Đồ chơi - Xanh lá
                { 3, "#dc3545" },  // Phụ kiện - Đỏ
                { 4, "#ffc107" },  // Thuốc - Vàng
                { 5, "#6f42c1" }   // Khác - Tím
            };
            
            return View(product);
        }

        // GET: Admin/SanPham/Create
        public ActionResult Create()
        {
            ViewBag.LoaiList = new SelectList(_db.Loais, "ID", "TenLoai");
           
            return View(new SanPham());
        }

        // POST: Admin/SanPham/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SanPham sanPham, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // X? lý upload ?nh
                    if (imageFile != null && imageFile.ContentLength > 0)
                    {
                        string fileName = System.IO.Path.GetFileName(imageFile.FileName);
                        string path = System.IO.Path.Combine(Server.MapPath("~/Content/img"), fileName);
                        imageFile.SaveAs(path);
                        sanPham.Img = fileName;
                    }

                    _db.SanPhams.Add(sanPham);
                    _db.SaveChanges();
                    TempData["Success"] = "Thêm sản phẩm thành công!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Có lỗi xảy ra: " + ex.Message;
                }
            }

            ViewBag.LoaiList = new SelectList(_db.Loais, "ID", "TenLoai", sanPham.LoaiID);
            return View(sanPham);
        }

        // GET: Admin/SanPham/Edit/5
        public ActionResult Edit(int id)
        {
            var product = _db.SanPhams.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.LoaiList = new SelectList(_db.Loais, "ID", "TenLoai", product.LoaiID);
            return View(product);
        }

        // POST: Admin/SanPham/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SanPham sanPham, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var productInDb = _db.SanPhams.Find(sanPham.ID);
                    if (productInDb == null)
                    {
                        return HttpNotFound();
                    }

                    // X? lý upload ?nh m?i
                    if (imageFile != null && imageFile.ContentLength > 0)
                    {
                        string fileName = System.IO.Path.GetFileName(imageFile.FileName);
                        string path = System.IO.Path.Combine(Server.MapPath("~/Content/img"), fileName);
                        imageFile.SaveAs(path);
                        productInDb.Img = fileName;
                    }

                    // C?p nh?t thông tin
                    productInDb.TenSP = sanPham.TenSP;
                    productInDb.GiaSP = sanPham.GiaSP;
                    productInDb.LoaiID = sanPham.LoaiID;
                    productInDb.MoTa = sanPham.MoTa;
                    productInDb.ThanhPhan = sanPham.ThanhPhan;
                    productInDb.HDSD = sanPham.HDSD;

                    _db.SaveChanges();
                    TempData["Success"] = "Cập nhật sản phẩm thành công!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Có lỗi xảy ra: " + ex.Message;
                }
            }

            ViewBag.LoaiList = new SelectList(_db.Loais, "ID", "TenLoai", sanPham.LoaiID);
            return View(sanPham);
        }

        // POST: Admin/SanPham/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var product = _db.SanPhams.Find(id);
                if (product != null)
                {
                    // Ki?m tra xem s?n ph?m có trong ??n hàng nào không
                    if (product.ChiTietDonHangs.Any())
                    {
                        TempData["Error"] = "Không thể xóa sản phẩm đã có trong đơn hàng!";
                        return RedirectToAction("Index");
                    }

                    _db.SanPhams.Remove(product);
                    _db.SaveChanges();
                    TempData["Success"] = "Xóa sản phẩm thành công!";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction("Index");
            }
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
