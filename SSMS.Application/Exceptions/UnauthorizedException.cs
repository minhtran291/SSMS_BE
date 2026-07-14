using System.Net;

namespace SSMS.Application.Exceptions
{
    public sealed class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message) : base(message, (int)HttpStatusCode.Unauthorized)
        {
        }
    }
}
