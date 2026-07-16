using System.Net;

namespace SSMS.Application.Exceptions
{
    public sealed class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message, (int)HttpStatusCode.NotFound) { }
    }
}
