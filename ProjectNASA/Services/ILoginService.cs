﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectNASA.Services
{
    public interface ILoginService
    {
        bool Login(string username, string email, string password);
    }
}
