using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebsiteBanHang.Business;
using WebsiteBanHang.Data;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Controllers
{
    public class AccountController : Controller
    {
        private UserService _userService = new UserService();
        // GET: Account

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string username ,string password,string fullName) 
        {
            if (_userService.IsUserNameExists(username))
            {
                ModelState.AddModelError("","Tên User này đã được sử dụng");
                return View();
            }

            User newUser = _userService.RegisterUser(username, password, fullName);

            return RedirectToAction("Login");
        }
        public ActionResult Index()
        {
            return View();
        }
        // GET: Account/Login
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password, string returnUrl)
        {
            User user = _userService.Login(username, password);
            if (user != null)
            {
                // Tạo FormsAuthenticationTicket với role
                var roles = user.IsAdmin == true ? "Admin" : "User";

                var ticket = new FormsAuthenticationTicket(
                    1,                              // version
                    user.UserName,                  // name
                    DateTime.Now,                   // issue time
                    DateTime.Now.AddHours(2),       // expiration time
                    false,                          // persistent
                    roles,                          // user data (roles)
                    FormsAuthentication.FormsCookiePath
                );

                // Mã hóa ticket
                var encryptedTicket = FormsAuthentication.Encrypt(ticket);

                // Tạo cookie
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(cookie);

                Session["FullName"] = user.TenNguoiDung;

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
            
    }
}