using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalender.Singletons
{
    public class Authorized
    {
        private static readonly Authorized userAuthStatus = new Authorized();
        private bool AuthorizedUser;
        private string Username;
        private string Location;
        private Authorized()
        {
            AuthorizedUser = false;
            Username = null;
            Location = null;
        }
        public static Authorized GetAuthStatus()
        {
            return userAuthStatus;
        }
        public void SetUserLoggedIn(bool isLoggedIn, string username, string location)
        {
            AuthorizedUser = isLoggedIn;
            Username = username;
            Location = location;
        }
        public string WhoIsUser()
        {
            return Username;
        }
        public bool IsUserLoggedIn()
        {
            return AuthorizedUser;
        }
        public string UserLocation()
        {
            return Location;
        }
    }
}
