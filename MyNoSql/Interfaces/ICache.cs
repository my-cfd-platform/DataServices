namespace DataServices.MyNoSql.Interfaces
{
    public interface ICache<T>
    {
        IEnumerable<T> GetAll();
        T Get(string id);
        void SubscribeOnChanges(Type type, Action<IReadOnlyList<T>> priceChanges);
        void UnsubscribeOnChanges(Type type);
    }
}