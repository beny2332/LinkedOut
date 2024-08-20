using LinkedOut.DAL;
using LinkedOut.Models;
using Microsoft.AspNetCore.Mvc;

namespace LinkedOut.Services
{
    public class UserService
    {
        private DataLayer db;
        public UserService(DataLayer _db) {db = _db;}
        public async Task <UserModel> GetUserById(int id)
        {
            return db.Users.Find(id);
        }

        public async Task<UserModel> GetUserByUserNameAndPassword(string un, string uhpw)
        {
            UserModel user = db.Users.FirstOrDefault(u=>u.userName == un);
            bool isVerified = BCrypt.Net.BCrypt.Verify(uhpw, user.password);

            return isVerified ? user : null;
        }

        public async Task<int> register(UserModel user)
        {
            string uhpw = user.password;
            // hash the password
            string hashedpw = BCrypt.Net.BCrypt.HashPassword(uhpw);
            // save the hashed pw in the user
            user.password = hashedpw;
            db.Users.Add(user);
            db.SaveChanges();
            UserModel created = db.Users.FirstOrDefault(u => u.userName == user.userName);

            return created.id;
        }

    }
}
