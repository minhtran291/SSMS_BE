using System.Net;

namespace SSMS.Application.Exceptions
{
    public class ForbiddenException : BaseException
    {
        public ForbiddenException(string message) : base(message, (int)HttpStatusCode.Forbidden) { }
    }
}
