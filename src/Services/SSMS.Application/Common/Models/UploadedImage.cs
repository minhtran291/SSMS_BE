namespace SSMS.Application.Common.Models
{
    public sealed class UploadedImage
    {
        public required string ImageUrl { get; init; }
        public required string StorageKey {  get; init; }
    }
}
