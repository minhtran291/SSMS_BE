namespace SSMS.Application.DTOs
{
    public abstract record BaseSearchDTO
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int PageIndex => Page > 0 ? Page - 1 : 0;
    }
}
