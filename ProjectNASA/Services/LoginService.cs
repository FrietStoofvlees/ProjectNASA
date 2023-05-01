using CommunityToolkit.Maui.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectNASA.Services
{
    public class LoginService : ILoginService
    {
        public async Task<User> Login(string username, string password)
        {
            User user = new()
            {
                Username = username,
                Password = password
            };

            return await Task.FromResult(user);
            //await Toast.Make("Please fill in your username and password").Show();
        }
    }
}
