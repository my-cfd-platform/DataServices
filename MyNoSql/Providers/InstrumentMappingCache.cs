using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataReader;

namespace DataServices.MyNoSql.Providers;

public class InstrumentMappingCache : ICache<IProviderInstrumentMap>
{
    private readonly IMyNoSqlServerDataReader<ProviderInstrumentEntity> _readRepository;
    private const string TableName = "instrument-mapping";

    public InstrumentMappingCache(IMyNoSqlServerDataReader<ProviderInstrumentEntity> readRepository)
    {
        _readRepository = readRepository;
    }

    public InstrumentMappingCache(IMyNoSqlSubscriber tcpConnection)
    {
        var readRepository =
            new MyNoSqlReadRepository<ProviderInstrumentEntity>(tcpConnection, TableName);
        _readRepository = readRepository;
    }

    public IEnumerable<IProviderInstrumentMap> GetAll()
    {
        var partitionKey = ProviderInstrumentEntity.GeneratePartitionKey();
        return GetAll(partitionKey);
    }

    public IEnumerable<IProviderInstrumentMap> GetAll(string partitionKey)
    {
        return _readRepository.Get(partitionKey);
    }

    public IProviderInstrumentMap Get(string id)
    {
        var partitionKey = ProviderInstrumentEntity.GeneratePartitionKey();
        var rowKey = ProviderInstrumentEntity.GenerateRowKey(id);
        return Get(rowKey, partitionKey);
    }

    public IProviderInstrumentMap Get(string id, string partitionKey)
    {
        return _readRepository.Get(partitionKey, id);
    }

    public void SubscribeOnChanges(Type type, Action<IReadOnlyList<IProviderInstrumentMap>> priceChanges)
    {
        throw new NotImplementedException();
    }

    public void UnsubscribeOnChanges(Type type)
    {
        throw new NotImplementedException();
    }
}
