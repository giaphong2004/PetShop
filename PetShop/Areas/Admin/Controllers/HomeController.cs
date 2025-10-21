using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Data;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private CSDL_PetShopEntities _db = new CSDL_PetShopEntities();

        // GET: Admin/Home
        public ActionResult Index()
        {
            // Lấy ngày hôm nay
            var today = DateTime.Today;
            
            // Doanh thu hôm nay
            var todayRevenue = _db.DonHangs
                .Where(d => d.NgayDat.HasValue && 
                       d.NgayDat.Value.Year == today.Year &&
                       d.NgayDat.Value.Month == today.Month &&
                       d.NgayDat.Value.Day == today.Day)
                .SelectMany(d => d.ChiTietDonHangs)
                .Sum(ct => (decimal?)((ct.SoLuong ?? 0) * (ct.DonGia ?? 0))) ?? 0;

            // Đơn hàng mới (hôm nay)
            var newOrders = _db.DonHangs
                .Count(d => d.NgayDat.HasValue && 
                       d.NgayDat.Value.Year == today.Year &&
                       d.NgayDat.Value.Month == today.Month &&
                       d.NgayDat.Value.Day == today.Day);

            // Khách hàng mới (tháng này)
            var newCustomers = _db.KhachHangs.Count();

            // Tổng số sản phẩm
            var totalProducts = _db.SanPhams.Count();

            // Đơn hàng gần nhất
            var recentOrders = _db.DonHangs
                .OrderByDescending(d => d.NgayDat)
                .Take(7)
                .ToList();

            // Thống kê sản phẩm theo loại - Sử dụng ViewModel
            var categoryStats = _db.Loais
                .ToList()
                .Select(l => new CategoryStatViewModel
                {
                    TenLoai = l.TenLoai,
                    SoLuong = l.SanPhams.Count()
                })
                .Where(x => x.SoLuong > 0)
                .OrderByDescending(x => x.SoLuong)
                .ToList();

            // Thống kê doanh thu 3 tháng gần nhất
            var revenueStats = new List<MonthRevenueViewModel>();
            
            for (int i = 2; i >= 0; i--)
            {
                var targetMonth = today.AddMonths(-i);
                var monthRevenue = _db.DonHangs
                    .Where(d => d.NgayDat.HasValue &&
                           d.NgayDat.Value.Year == targetMonth.Year &&
                           d.NgayDat.Value.Month == targetMonth.Month)
                    .ToList()
                    .SelectMany(d => d.ChiTietDonHangs)
                    .Sum(ct => (ct.SoLuong ?? 0) * (ct.DonGia ?? 0));

                revenueStats.Add(new MonthRevenueViewModel
                {
                    Month = targetMonth.Month,
                    Year = targetMonth.Year,
                    MonthName = "Tháng " + targetMonth.Month,
                    Revenue = monthRevenue
                });
            }

            ViewBag.TodayRevenue = todayRevenue;
            ViewBag.NewOrders = newOrders;
            ViewBag.NewCustomers = newCustomers;
            ViewBag.TotalProducts = totalProducts;
            ViewBag.RecentOrders = recentOrders;
            ViewBag.CategoryStats = categoryStats;
            ViewBag.RevenueStats = revenueStats;

            return View();
        }
    }
}