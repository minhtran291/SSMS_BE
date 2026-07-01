using System.Net;

namespace SSMS.Application.Exceptions
{
    public sealed class BadRequestException : BaseException
    {
        public BadRequestException(string message) : base(message, (int)HttpStatusCode.BadRequest) { }
    }
}
