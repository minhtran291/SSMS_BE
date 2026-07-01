using System.Net;

namespace SSMS.Application.Exceptions
{
    public class ConflictException : BaseException
    {
        public ConflictException(string message) : base(message, (int)HttpStatusCode.Conflict) { }
    }
}
