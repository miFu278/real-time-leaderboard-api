namespace LeaderboardSystem.Application.Common.Models
{
    public class PaginatedList<T>(List<T> items, int count, int pageNumber, int pageSize)
    {
        public List<T> Items { get; set; } = items;
        public int PageNumber { get; set; } = pageNumber;
        public int TotalPages { get; set; } = (int)Math.Ceiling(count / (double)pageSize);
        public int TotalCount { get; set; } = pageSize;
        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;
    }
}
