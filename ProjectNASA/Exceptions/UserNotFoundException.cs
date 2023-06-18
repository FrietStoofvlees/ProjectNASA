using System;
using System.Collections.Generic;
using System.Linq;
namespace ProjectNASA.Exceptions
{
    [Serializable]
    public class UserNotFoundException : Exception
    {
        public string UserName { get; set; }
        public UserNotFoundException() { }
        public UserNotFoundException(string message) : base(message) { }
        public UserNotFoundException(string message, string username)
            : base(message)
        {
            UserName = username;
        }
    }
}
