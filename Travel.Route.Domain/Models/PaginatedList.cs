namespace Travel.Route.Domain.Models
{
    public class PaginatedList<TEntity>
    {
        public PaginatedList(IEnumerable<TEntity> items, int total, int page, int pageSize)
        {
            Items = items;
            Total = total;
            Page = page;
            PageSize = pageSize;
        }

        public IEnumerable<TEntity> Items { get; set; }

        public int Total { get; set; }

        public int TotalPages => (int)Math.Ceiling(Total / (double)PageSize);

        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
