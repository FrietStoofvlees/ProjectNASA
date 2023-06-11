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

            if (Preferences.ContainsKey(nameof(AppHelpers.User)))
            {
                Preferences.Remove(nameof(AppHelpers.User));
            }

            string userDetails = JsonSerializer.Serialize(user);
            Preferences.Set(nameof(AppHelpers.User), userDetails);
            AppHelpers.User = user;

            return true;

            //return await Task.FromResult(user);
        }

        public bool Logout()
        {
            if (Preferences.ContainsKey(nameof(AppHelpers.User)))
            {
                Preferences.Remove(nameof(AppHelpers.User));
            }

            return true;
        }

        public bool Logout(string username, string password)
        {
            if (Preferences.ContainsKey(nameof(AppHelpers.User)))
            {
                Preferences.Remove(nameof(AppHelpers.User));
            }

            return true;
        }
    }
}
