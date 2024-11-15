namespace IntelliTect.SyncUp.Data.Utilities;

public static class QueryableExtensions
{
    /// <summary>
    /// Perform an untracked query without any tenant filtering.
    /// </summary>
    public static IQueryable<T> IgnoreTenancy<T>(this IQueryable<T> query)
        where T : class
        => query.IgnoreQueryFilters().AsNoTrackingWithIdentityResolution();
}