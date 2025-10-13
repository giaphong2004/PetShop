using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebsiteBanHang.Business;
using WebsiteBanHang.Data;
using WebsiteBanHang.Models; 

namespace WebsiteBanHang.Controllers
{
    public class ShoppingCartController : Controller
    {
        
        public ActionResult AddToCart(int id)
        {
            // 1. Lấy giỏ hàng hiện tại từ Session
            List<CartItem> cart = Session["Cart"] as List<CartItem>;

            // 2. Nếu giỏ hàng chưa tồn tại, tạo mới
            if (cart == null)
            {
                cart = new List<CartItem>();
            }

            // 3. Kiểm tra xem sản phẩm đã có trong giỏ chưa
            CartItem item = cart.FirstOrDefault(c => c.SanPham.ID == id);

            if (item != null)
            {
                // Nếu đã có, chỉ tăng số lượng lên 1
                item.soLuong++;
            }
            else
            {
                // Nếu chưa có, thêm sản phẩm mới vào giỏ
                var sanPhamService = new SanPhamService();
                SanPham sanPham = sanPhamService.LaySanPhamTheoId(id);

                // Kiểm tra xem sản phẩm có thực sự tồn tại trong DB không
                if (sanPham != null)
                {
                    CartItem newItem = new CartItem()
                    {
                        SanPham = sanPham,
                        soLuong = 1 // Lần đầu thêm vào thì số lượng là 1
                    };
                    cart.Add(newItem);
                }
            }

            // 4. Lưu lại giỏ hàng vào Session
            Session["Cart"] = cart;

            // 5. Chuyển hướng người dùng đến trang xem giỏ hàng (chúng ta sẽ tạo ở bước sau)
            return RedirectToAction("Index");
        }

        // Action Index để hiển thị giỏ hàng (sẽ làm ở bước sau)
        public ActionResult Index()
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
            }
            return View(cart);
        }

        [HttpPost]
        public ActionResult UpdateCart(int id, int quantity)
        {
            // Lấy giỏ hàng từ Session
            List<CartItem> cart = Session["Cart"] as List<CartItem>;

            // Kiểm tra nếu số lượng mới nhỏ hơn 1 thì xóa sản phẩm
            if (quantity < 1)
            {
                // Gọi hàm xóa (sẽ tạo ở bước sau) hoặc xử lý xóa tại đây
                quantity = 1;
            }

            decimal newTongTien = 0;
            decimal newItemTotal = 0;

            if (cart != null)
            {
                // Tìm món hàng trong giỏ
                CartItem itemToUpdate = cart.FirstOrDefault(item => item.SanPham.ID == id);
                if (itemToUpdate != null)
                {
                    // Cập nhật số lượng mới
                    itemToUpdate.soLuong = quantity;
                    newItemTotal = itemToUpdate.SanPham.GiaSP * itemToUpdate.soLuong;
                }

                // Tính lại tổng tiền của toàn bộ giỏ hàng
                newTongTien = cart.Sum(item => item.SanPham.GiaSP * item.soLuong);

                // Lưu lại giỏ hàng vào Session
                Session["Cart"] = cart;
            }

            // Trả về kết quả dưới dạng JSON để JavaScript có thể xử lý
            var result = new
            {
                itemTotal = newItemTotal, 
                cartTotal = newTongTien,  
                quantity = quantity       
            };

            return Json(result);
        }
        public ActionResult RemoveFromCart(int id)
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem>;
            
            if(cart != null)
            {
                CartItem itemRemove = cart.FirstOrDefault(item => item.SanPham.ID == id);

                if(itemRemove != null)
                {
                    cart.Remove(itemRemove);

                    Session["Cart"] = cart;
                }
            }
            return RedirectToAction("Index");
        }
    }
}