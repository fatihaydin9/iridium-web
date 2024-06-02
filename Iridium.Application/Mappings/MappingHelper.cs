using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.Mappings;

public static class MappingHelper
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(
        this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
    {
        return PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);
    }

    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable,
        IConfigurationProvider configuration) where TDestination : class
    {
        return queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();
    }
}