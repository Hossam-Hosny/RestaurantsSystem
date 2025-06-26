namespace Restaurants.Application.Common;

public class PageResult<T>
{

    public PageResult(IEnumerable<T> items, int totalCount, int pageSize, int pageNumber)
    {
        Items = items;
        TotalItemsCount = totalCount;
        TotalPages = (int) Math.Ceiling( totalCount /(double) pageSize);

        itemsFrom = pageSize * (pageNumber - 1) + 1;
        itemsTo = itemsFrom + pageSize - 1;
    }

    public IEnumerable<T> Items { get; set; }
    public int TotalPages { get; set; }
    public int TotalItemsCount { get; set; }
    public int itemsFrom { get; set; }
    public int itemsTo { get; set; }
}
