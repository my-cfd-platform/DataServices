namespace DataServices.MyNoSql.Interfaces
{
    public interface ICache<out T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(string partitionKey);
        T Get(string id);
        T Get(string id, string partitionKey);
        void SubscribeOnChanges(Type type, Action<IReadOnlyList<T>> priceChanges);
        void UnsubscribeOnChanges(Type type);
    }
}