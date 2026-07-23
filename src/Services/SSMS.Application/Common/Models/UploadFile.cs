namespace SSMS.Application.Common.Models
{
    public sealed class UploadFile
    {
        public required Stream Stream { get; init; }
        public required string FileName { get; init; }
        public string? ContentType { get; init; }
        public long Length { get; init; }
    }
}
