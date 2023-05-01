using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectNASA.Services
{
    public interface ILoginService
    {
        Task<User> Login(string username, string password);
    }
}
