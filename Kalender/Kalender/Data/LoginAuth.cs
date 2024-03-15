using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalender.Data
{
    public class LoginAuth
    {
        private readonly IMongoCollection<Models.User> _userCollection;

        public LoginAuth()
        {
            var database = DB.GetDatabase();
            _userCollection = database.GetCollection<Models.User>("User");
        }

        public async Task<bool> LoginAuthAsync(string username, string password)
        {
            var user = await _userCollection.Find(u => u.Name == username).FirstOrDefaultAsync();

            if (user != null && user.Password == password)
            {
                return true;
            }

            return false;
        }
        public async Task<bool> SignUpAuthAsync(string username, string password)
        {
            var user = await _userCollection.Find(u => u.Name == username).FirstOrDefaultAsync();

            if (user != null)
            {
                return true;
            }

            return false;
        }
    }
}
