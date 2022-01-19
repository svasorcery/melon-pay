﻿namespace MelonPay.Shared.Abstractions.Queries.Pagination
{
    public abstract class PagedQuery : IPagedQuery
    {
        public int Page { get; set; }
        public int Results { get; set; }
        public string OrderBy { get; set; }
        public string SortOrder { get; set; }
    }

    public abstract class PagedQuery<T> : PagedQuery, IPagedQuery<Paged<T>>
    {
    }
}
