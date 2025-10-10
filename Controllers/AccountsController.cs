//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Text;
//using System.Web.Security;
//using WebsiteBanHang.Models;
//using System.Security.Cryptography;

//namespace WebsiteBanHang.Controllers
//{
//    public class AccountsController : Controller

//    {
//        public ActionResult Index()
//        {
           
//                return View();
            
            
//        }
//        // GET: Account
       
//        public ActionResult Login()
//        {
//            return View();
//        }

//        [HttpPost]
//        public ActionResult Login(User model)
//        {
//            using (CSDL_PetShopEntities context = new CSDL_PetShopEntities())
//            {
//                model.UserPassword = GetMD5(model.UserPassword);
//                bool IsValidUser = context.Users.Any(user => user.UserName.ToLower() ==
//                model.UserName.ToLower() && user.UserPassword == model.UserPassword);
//                if (IsValidUser)
//                {

//                    // Lưu thông tin người dùng vào session
//                    Session["user"] = context.Users.FirstOrDefault(user => user.UserName.ToLower() == model.UserName.ToLower());

//                    FormsAuthentication.SetAuthCookie(model.UserName, false);
//                    return RedirectToAction("Danhmucsanpham", "Admin/HomeAdmin/Danhmucsanpham");
//                }
//                ModelState.AddModelError("", "Mật khẩu hoặc tài khoản không chính xác");
//                return View();
//            }
//        }




//        public ActionResult Signup()
//        {
//            return View();
//        }
//        [HttpPost]
//        public ActionResult Signup(User model)
//        {
//            using (CSDL_PetShopEntities context = new CSDL_PetShopEntities())
//            {
//                model.UserPassword = GetMD5(model.UserPassword);
//                context.Users.Add(model);
//                context.SaveChanges();
//            }
//            return RedirectToAction("Login");
//        }

//        public ActionResult Logout()
//        {
//            FormsAuthentication.SignOut();
//            return RedirectToAction("Login");
//        }

//        public static string GetMD5(string str)
//        {
//            MD5 md5 = new MD5CryptoServiceProvider();
//            byte[] fromData = Encoding.UTF8.GetBytes(str);
//            byte[] targetData = md5.ComputeHash(fromData);
//            string byte2String = null;

//            for (int i = 0; i < targetData.Length; i++)
//            {
//                byte2String += targetData[i].ToString("x2");
//            }
//            return byte2String;
//        }
//    }
//}