using System.Net;

namespace SSMS.Application.Exceptions
{
    public sealed class AppValidationException : BaseException
    {
        public Dictionary<string, string[]> Errors { get; }

        public AppValidationException(Dictionary<string, string[]> errors) 
            : base("Validation Failed", (int)HttpStatusCode.BadRequest)
        {
            Errors = errors;
        }
    }
}
