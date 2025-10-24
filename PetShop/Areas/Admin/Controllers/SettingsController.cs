using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebsiteBanHang.Data;
using WebsiteBanHang.Models;
using WebsiteBanHang.Business;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class SettingsController : BaseController
    {
        private CSDL_PetShopEntities _db = new CSDL_PetShopEntities();
        private UserService _userService = new UserService();

        // GET: Admin/Settings
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var user = _db.Users.FirstOrDefault(u => u.UserName == username);
            
            if (user == null)
            {
                return HttpNotFound();
            }

            var model = new AdminProfileViewModel
            {
                ID = user.ID,
                UserName = user.UserName,
                TenNguoiDung = user.TenNguoiDung
            };

            return View(model);
        }

        // POST: Admin/Settings/UpdateProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(AdminProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _db.Users.Find(model.ID);
                    if (user == null)
                    {
                        TempData["Error"] = "Không tìm thấy người dùng!";
                        return RedirectToAction("Index");
                    }

                    // Cập nhật thông tin
                    user.TenNguoiDung = model.TenNguoiDung;
                    
                    // Cập nhật Session
                    Session["FullName"] = model.TenNguoiDung;

                    _db.SaveChanges();
                    TempData["Success"] = "Cập nhật thông tin thành công!";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Có lỗi xảy ra: " + ex.Message;
                }
            }

            return RedirectToAction("Index");
        }

        // POST: Admin/Settings/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var username = User.Identity.Name;
                    
                    // Kiểm tra mật khẩu cũ bằng cách thử login
                    var user = _userService.Login(username, model.OldPassword);
                    
                    if (user == null)
                    {
                        TempData["Error"] = "Mật khẩu cũ không chính xác!";
                        return RedirectToAction("Index");
                    }

                    // Kiểm tra mật khẩu mới và xác nhận
                    if (model.NewPassword != model.ConfirmPassword)
                    {
                        TempData["Error"] = "Mật khẩu mới và xác nhận không khớp!";
                        return RedirectToAction("Index");
                    }

                    // Cập nhật mật khẩu mới
                    bool success = _userService.ChangePassword(username, model.NewPassword);
                    
                    if (success)
                    {
                        TempData["Success"] = "Đổi mật khẩu thành công!";
                    }
                    else
                    {
                        TempData["Error"] = "Không thể cập nhật mật khẩu!";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Có lỗi xảy ra: " + ex.Message;
                }
            }
            else
            {
                TempData["Error"] = "Vui lòng kiểm tra lại thông tin!";
            }

            return RedirectToAction("Index");
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
