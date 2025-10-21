using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Data;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class DonHangController : BaseController
    {
        private CSDL_PetShopEntities _db = new CSDL_PetShopEntities();

        // GET: Admin/DonHang
        public ActionResult Index(string searchTerm = "", string status = "", string dateFilter = "")
        {
            var query = _db.DonHangs.AsQueryable();

            // Tìm ki?m theo mã ??n hàng ho?c tên khách hàng
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(d => 
                    d.ID.ToString().Contains(searchTerm) || 
                    d.KhachHang.TenKH.Contains(searchTerm));
            }

            // L?c theo tr?ng thái
            if (!string.IsNullOrEmpty(status) && status != "all")
            {
                query = query.Where(d => d.TrangThai == status);
            }

            // L?c theo ngày
            if (!string.IsNullOrEmpty(dateFilter))
            {
                DateTime filterDate;
                if (DateTime.TryParse(dateFilter, out filterDate))
                {
                    query = query.Where(d => d.NgayDat.HasValue &&
                        d.NgayDat.Value.Year == filterDate.Year &&
                        d.NgayDat.Value.Month == filterDate.Month &&
                        d.NgayDat.Value.Day == filterDate.Day);
                }
            }

            var orders = query
                .OrderByDescending(d => d.NgayDat)
                .ToList();

            ViewBag.SearchTerm = searchTerm;
            ViewBag.Status = status;
            ViewBag.DateFilter = dateFilter;

            return View(orders);
        }

        // GET: Admin/DonHang/Details/5
        public ActionResult Details(int id)
        {
            var order = _db.DonHangs.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Admin/DonHang/Edit/5
        public ActionResult Edit(int id)
        {
            var order = _db.DonHangs.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            ViewBag.StatusList = new SelectList(new[]
            {
                 new { Value = "Đang xử lý", Text = "Đang xử lý" },
                new { Value = "Đang giao", Text = "Đang giao" },
                new { Value = "Đã giao", Text = "Đã giao" },
                new { Value = "Đã hủy", Text = "Đã hủy" }
            }, "Value", "Text", order.TrangThai);

            return View(order);
        }

        // POST: Admin/DonHang/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string TrangThai)
        {
            var order = _db.DonHangs.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            try
            {
                order.TrangThai = TrangThai;
                _db.SaveChanges();
                TempData["Success"] = "Cập nhật trạng thái giao hàng thành công!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction("Edit", new { id = id });
            }
        }

        // POST: Admin/DonHang/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var order = _db.DonHangs.Find(id);
                if (order != null)
                {
                    // Xóa chi ti?t ??n hàng tr??c
                    _db.ChiTietDonHangs.RemoveRange(order.ChiTietDonHangs);
                    // Xóa ??n hàng
                    _db.DonHangs.Remove(order);
                    _db.SaveChanges();
                    TempData["Success"] = "Xóa đơn hàng thành công!";
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
