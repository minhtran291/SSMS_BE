namespace SSMS.Application.DTOs.Product
{
    public record ProductSearchDTO : BaseSearchDTO
    {
        public string Keyword { get; set; } = string.Empty;
        public bool IncludeDeleted { get; set; } = false;
    }
}
