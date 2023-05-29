using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataReader;

namespace DataServices.MyNoSql.Providers;

    public class TradingInstrumentCache : ICache<ITradingInstrument>
    {
        private readonly IMyNoSqlServerDataReader<TradingInstrumentMyNoSqlEntity> _readRepository;
        private const string TableName = "instruments";

        public TradingInstrumentCache(IMyNoSqlSubscriber tcpConnection)
        {
            var readRepository =
                new MyNoSqlReadRepository<TradingInstrumentMyNoSqlEntity>(tcpConnection, TableName);
            _readRepository = readRepository;
        }

        public IEnumerable<ITradingInstrument> GetAll()
        {
            var partitionKey = TradingInstrumentMyNoSqlEntity.GeneratePartitionKey();
            return GetAll(partitionKey);
        }

        public IEnumerable<ITradingInstrument> GetAll(string partitionKey)
        {
            return _readRepository.Get(partitionKey);
        }

        public ITradingInstrument Get(string id)
        {
            var partitionKey = TradingInstrumentMyNoSqlEntity.GeneratePartitionKey();
            var rowKey = TradingInstrumentMyNoSqlEntity.GenerateRowKey(id);
            return Get(rowKey, partitionKey);
        }

        public ITradingInstrument Get(string id, string partitionKey)
        {
            return _readRepository.Get(partitionKey, id);
        }

        public void SubscribeOnChanges(Type type, Action<IReadOnlyList<ITradingInstrument>> changes)
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeOnChanges(Type type)
        {
            throw new NotImplementedException();
        }
    }
