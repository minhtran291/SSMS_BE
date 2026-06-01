using Microsoft.AspNetCore.Http;

namespace SSMS.Application.Exceptions
{
    public sealed class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message, StatusCodes.Status404NotFound) { }
    }
}
