namespace SSMS.Application.Exceptions
{
    public abstract class BaseException : Exception
    {
        public int StatusCode { get; }

        protected BaseException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        protected BaseException(string message, int statusCode, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
