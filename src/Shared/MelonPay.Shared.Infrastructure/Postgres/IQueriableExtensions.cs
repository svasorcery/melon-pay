﻿using MelonPay.Shared.Abstractions.Queries.Pagination;
using Microsoft.EntityFrameworkCore;

namespace MelonPay.Shared.Infrastructure.Postgres
{
    public static class IQueriableExtensions
    {
        public static Task<Paged<T>> PaginateAsync<T>(this IQueryable<T> data, IPagedQuery query,
           CancellationToken cancellationToken = default)
           => data.PaginateAsync(query.Page, query.Results, cancellationToken);

        public static async Task<Paged<T>> PaginateAsync<T>(this IQueryable<T> data, int page, int results,
            CancellationToken cancellationToken = default)
        {
            if (page <= 0)
            {
                page = 1;
            }

            results = results switch
            {
                <= 0 => 10,
                > 100 => 100,
                _ => results
            };

            var totalResults = await data.CountAsync(cancellationToken);
            var totalPages = totalResults <= results ? 1 : (int)Math.Floor((double)totalResults / results);
            var result = await data.Skip((page - 1) * results).Take(results).ToListAsync(cancellationToken);

            return new Paged<T>(result, page, results, totalPages, totalResults);
        }

        public static Task<List<T>> SkipAndTakeAsync<T>(this IQueryable<T> data, IPagedQuery query,
            CancellationToken cancellationToken = default)
            => data.SkipAndTakeAsync(query.Page, query.Results, cancellationToken);

        public static async Task<List<T>> SkipAndTakeAsync<T>(this IQueryable<T> data, int page, int results,
            CancellationToken cancellationToken = default)
        {
            if (page <= 0)
            {
                page = 1;
            }

            results = results switch
            {
                <= 0 => 10,
                > 100 => 100,
                _ => results
            };

            return await data
                .Skip((page - 1) * results)
                .Take(results)
                .ToListAsync(cancellationToken);
        }
    }
}
