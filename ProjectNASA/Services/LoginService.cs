using CommunityToolkit.Maui.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace ProjectNASA.Services
{
    public class LoginService : ILoginService
    {
        public bool Login(string username, string email, string password)
        {
            User user = new()
            {
                Username = username,
                Email = email,
                Password = password
            };

            if (Preferences.ContainsKey(nameof(App.User)))
            {
                Preferences.Remove(nameof(App.User));
            }

            string userDetails = JsonSerializer.Serialize(user);
            Preferences.Set(nameof(App.User), userDetails);
            App.User = user;

            return true;

            //return await Task.FromResult(user);
        }
    }
}
