namespace ProjectNASA.Exceptions
{
    [Serializable]
    public class UserNotFoundException : Exception
    {
        public string UserName { get; set; }
        public UserNotFoundException(string message, string username)
            : base(message)
        {
            UserName = username;
        }
        public UserNotFoundException(string message, string username, Exception innerException)
            : base(message, innerException)
        {
            UserName = username;
        }
    }
}
