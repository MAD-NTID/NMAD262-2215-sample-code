namespace RestApiWithDatabase.Exceptions
{
    public interface IUserErrorException
    {
        public int getStatusCode();
        public string getMessage();
    }
}