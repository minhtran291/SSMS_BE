namespace SSMS.Application.Common
{
    public sealed class PagedResult<T>
    {
        public IReadOnlyList<T> Items { get; init; } = [];
        public int CurrentPage { get; init; }
        public int PageSize { get; init; }
        public int TotalRecord { get; init; }
        public int TotalPages 
            => (int)Math.Ceiling((double)TotalRecord / PageSize);
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}
