using System;

namespace RestApiWithDatabase.Exceptions
{
    public class UserErrorException : Exception,IUserErrorException
    {
        private readonly int code;
        private readonly string message;

        public UserErrorException(int statusCode, string message)
            : base(message)
        {
            this.code = statusCode;
            this.message = message;
        }
        
        public int getStatusCode()
        {
            return this.code;
        }

        public string getMessage()
        {
            return this.message;
        }
    }
}