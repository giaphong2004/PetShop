🐾 PET SHOP – Website Bán Hàng Thú Cưng

Chào mừng bạn đến với dự án PET SHOP!
Đây là website bán hàng thú cưng mình đang phát triển trong thời gian gần đây. Mình sẽ liên tục cập nhật tiến độ và tính năng tại repo này để mọi người tiện theo dõi.

🧭 Tổng Quan Dự Án

PET SHOP là một website thương mại điện tử đơn giản mô phỏng mô hình bán hàng online, tập trung vào giao diện thân thiện và luồng nghiệp vụ cơ bản của một web bán hàng.

Luồng hoạt động chính:

Trang chủ → Xem sản phẩm → Chi tiết sản phẩm → Giỏ hàng → Thanh toán → Xác nhận thông tin → Thanh toán thành công → Xem hóa đơn

Người dùng cũng có thể đăng ký / đăng nhập tài khoản để quản lý đơn hàng của mình.

⚙️ Công Nghệ Sử Dụng

Backend: ASP.NET FramWork (C#)

Database: SQL Server (hiện mình đang dùng bản 2012, bạn có thể dùng bản khác)

Frontend: HTML, CSS, JavaScript (tham khảo giao diện từ trang chủ Pet Mart)

🧩 Cấu Trúc Dự Án
PetShop/
│
├── PetShop/                     → Giao diện chính của website
├── WebsiteBanHang.Business/     → Xử lý logic & quy tắc nghiệp vụ
├── WebsiteBanHang.Data/         → Làm việc với Database (Entity, Repository, ...)
└── README.md                    → File mô tả dự án (chính là file bạn đang đọc 😄)

🖥️ Yêu Cầu Cài Đặt

Hệ điều hành: Windows

Vì dự án sử dụng công nghệ ASP.NET Framework – một nền tảng cũ của Microsoft, nên chỉ chạy ổn định trên Windows.

Công cụ:

Visual Studio (khuyên dùng bản 2022 trở lên)

.NET Framework SDK

Cơ sở dữ liệu:

SQL Server 2012 trở lên

Import file database từ link: 📦 Google Drive – Database

🚀 Cách Chạy Dự Án

Clone project về máy:

git clone https://github.com/giaphong2004/PetShop.git


Mở project trong Visual Studio

Kiểm tra chuỗi kết nối (connection string) trong appsettings.json

Chạy SQL Server và import database

Bấm Run ▶️ để khởi động website

🧠 Ghi Chú Phát Triển

Mình sẽ cập nhật tiến độ và các tính năng mới tại đây sau mỗi giai đoạn.

Dự án hướng đến việc học hỏi, thực hành mô hình 3-layer và làm quen với cách tổ chức code backend rõ ràng.
