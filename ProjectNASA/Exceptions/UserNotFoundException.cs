using System;
using System.Collections.Generic;
using System.Linq;
namespace ProjectNASA.Exceptions
{
    [Serializable]
    public class UserNotFoundException : Exception
    {
        public string UserName { get; set; }
        public UserNotFoundException(string message, string username, Exception innerException)
            : base(message, innerException)
        {
            UserName = username;
        }
    }
}
