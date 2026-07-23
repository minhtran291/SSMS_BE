using System.Net;

namespace SSMS.Application.Exceptions
{
    public sealed class ExternalServiceException : BaseException
    {
        public ExternalServiceException(string message) 
            : base(message, (int)HttpStatusCode.BadGateway)
        {
        }

        public ExternalServiceException(string message, Exception innerException) 
            : base(message, (int)HttpStatusCode.BadGateway, innerException)
        {
        }
    }
}
