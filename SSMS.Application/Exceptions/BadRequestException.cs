using Microsoft.AspNetCore.Http;

namespace SSMS.Application.Exceptions
{
    public sealed class BadRequestException : BaseException
    {
        public BadRequestException(string message) : base(message, StatusCodes.Status400BadRequest) { }
    }
}
