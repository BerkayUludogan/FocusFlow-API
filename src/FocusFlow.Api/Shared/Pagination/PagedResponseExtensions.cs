using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Shared.Pagination;

public static class PagedResponseExtensions
{
    public static async Task<PagedResponse<T>> ToPagedResponseAsync<T>(
        this IQueryable<T> query,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var safePage = page < 1 ? 1 : page;
        var safePageSize = pageSize switch
        {
            < 1 => 20,
            > 100 => 100,
            _ => pageSize
        };

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((safePage - 1) * safePageSize)
            .Take(safePageSize)
            .ToListAsync(cancellationToken);

        return new PagedResponse<T>
        {
            Items = items,
            Page = safePage,
            PageSize = safePageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)safePageSize)
        };
    }
}