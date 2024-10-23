
using Microsoft.EntityFrameworkCore;

namespace usecases;

public class PagedList<T>(List<T> items, int count, int pageNumber, int pageSize)
{
    public int CurrentPage { get; private set; } = pageNumber;
    public int TotalPages { get; private set; } = (int) Math.Ceiling(count / (double) pageSize);
    public int PageSize { get; private set; } = pageSize;
    public int TotalCount { get; private set; } = count;
    public bool HasPrevious => (CurrentPage > 1);
    public bool HasNext => (CurrentPage < TotalPages);
    public List<T> Items { get; set; } = items;

    public static async Task<PagedList<T>> Create(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }

    public static PagedList<T> CreateList(List<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}