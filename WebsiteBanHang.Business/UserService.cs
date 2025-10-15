using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteBanHang.Data;


namespace WebsiteBanHang.Business
{
    using BCrypt.Net;
    public class UserService
    {
        private CSDL_PetShopEntities _db = new CSDL_PetShopEntities();
        public bool IsUserNameExists(string userName)
        {
            return _db.Users.Any(u => u.UserName == userName);
        }

        public User RegisterUser(string userName, string password, string fullName)
        {
            string hashedPassWork = BCrypt.HashPassword(password);

            var newUser = new User()
            {
                UserName = userName,
                PasswordHash = hashedPassWork,
                TenNguoiDung = fullName
            };

            _db.Users.Add(newUser);
            _db.SaveChanges();

            return newUser;
        }

        public User Login(string userName, string password)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserName == userName);
            if (user != null)
            {
                if (BCrypt.Verify(password, user.PasswordHash))
                {
                    return user;
                }
            }
            return null;
        }

        public User GetUserByUsername(string username)
        {
            {
                return _db.Users.FirstOrDefault(u => u.UserName == username);
            }
        }
    }
}

