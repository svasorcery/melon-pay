namespace MelonPay.Shared.Abstractions.Queries.Pagination
{
    public interface IPagedQuery : IQuery
    {
        int Page { get; set; }
        int Results { get; set; }
    }

    public interface IPagedQuery<T> : IPagedQuery, IQuery<T>
    {
    }
}
