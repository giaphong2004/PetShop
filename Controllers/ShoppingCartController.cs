//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Net;
//using System.Web.Mvc;
//using WebsiteBanHang.Models;


//namespace WebsiteBanHang.Controllers
//{
//    public class ShoppingCartController : Controller
//    {
//        private CSDL_PetShopEntities dBContext = new CSDL_PetShopEntities();
//        //Key
//        private string CartSession = "CartSession";
//        // GET: ShoppingCart
//        public ActionResult Cart()
//        {
//            return View();
//        }
//        public ActionResult Additem(int? Id)
//        {
//            if (Id == null)
//            {
//                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
//            }
//            if (Session[CartSession] == null)
//            {
//                List<CartItem> ListCart = new List<CartItem>
//                {
//                    new CartItem(dBContext.SanPhams.Find(Id), 1)
//                };
//                Session[CartSession] = ListCart;
//            }
//            else
//            {
//                List<CartItem> ListCart = (List<CartItem>)Session[CartSession];
//                int check = IsExistingCheck(Id);
//                if (check == -1)
//                {
//                    ListCart.Add(new CartItem(dBContext.SanPhams.Find(Id), 1));
//                }
//                else
//                {
//                    ListCart[check].Quantity++;
//                }
//                Session[CartSession] = ListCart;
//            }
//            return RedirectToAction("Cart");
//        }
//        private int IsExistingCheck(int? Id)
//        {
//            List<CartItem> Listcart = (List<CartItem>)Session[CartSession];
//            for (int i = 0; i < Listcart.Count; i++)
//            {
//                if (Listcart[i].Product.IdSP == Id)
//                {
//                    return i;
//                }
//            }
//            return -1;
//        }
        
//        public ActionResult XoaGioHang(int id)
//        {
//            //Kiểm tra masp
//            CSDL_PetShopEntities db = new CSDL_PetShopEntities();
//            SanPham sp = db.SanPhams.SingleOrDefault(n => n.IdSP == id);
//            //Nếu get sai masp thì sẽ trả về trang lỗi 404
//            if (sp == null)
//            {
//                Response.StatusCode = 404;
//                return null;
//            }
//            //Lấy giỏ hàng ra từ session
//            List<SanPham> Listcart = (List<SanPham>)Session[CartSession];
//            SanPham sanpham = Listcart.SingleOrDefault(n => n.IdSP == id);
//            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
//            if (sanpham != null)
//            {
//                Listcart.RemoveAll(n => n.IdSP == id);

//            }
//            if (Listcart.Count == 0)
//            {
//                return RedirectToAction("Index", "Home");
//            }
//            return RedirectToAction("cart");
//        }



//        [HttpPost]
//        public ActionResult UpdateCart(FormCollection field)
//        {
//            string[] quantities = field.GetValues("quantity");
//            List<CartItem> Listcart = (List<CartItem>)Session[CartSession];
//            for (int i = 0; i < Listcart.Count; i++)
//            {
//                Listcart[i].Quantity = Convert.ToInt32(quantities[i]);
//            }
//            Session[CartSession] = Listcart;
//            return RedirectToAction("Cart");
//        }

//        public ActionResult ClearCart()
//        {
//            Session[CartSession] = null;
//            return RedirectToAction("Cart");
//        }

//        public ActionResult CheckOut()
//        {
//            return View();
//        }
//        public ActionResult Succes()
//        {
//            return View();
//        }
//        [HttpPost]
//        public ActionResult ProcessOrder(FormCollection field)
//        {
//            List<CartItem> Listcart = (List<CartItem>)Session[CartSession];

//            var order = new WebsiteBanHang.Models.Order()
//            {
//                TenKH = field["cusName"],
//                OrderName = field["cusPhone"],
//                Email = field["cusEmail"],
//                DiaChi = field["cusAddress"],
//                NgayĐH = DateTime.Now,
//                TrangthaiDH = "Processing"
//            };
//            dBContext.Orders.Add(order);
//            dBContext.SaveChanges();


//            foreach (CartItem cart in Listcart)
//            {
//                OrderSP orderDetail = new OrderSP()
//                {
//                    OrderId = order.OrderId,
//                    ProductId = cart.Product.IdSP,
//                    Quantity = Convert.ToInt32(cart.Quantity),
//                    Price = Convert.ToInt32(cart.Product.GiaSP)
//                };
//                dBContext.OrderSPs.Add(orderDetail);
//                dBContext.SaveChanges();
//            }
//            Session.Remove(CartSession);

//            return View("ProcessOrder");
//        }
//    }
//}