namespace IntelliTect.SyncUp.Data.Coalesce;

public abstract class AppBehaviors<T> : StandardBehaviors<T, AppDbContext>
    where T : class
{
    protected AppBehaviors(CrudContext<AppDbContext> context) : base(context)
    {
    }
}