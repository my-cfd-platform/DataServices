using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataReader;

namespace DataServices.MyNoSql.Providers;

    public class TradingInstrumentCache : IInstrumentCache
    {
        private readonly IMyNoSqlServerDataReader<TradingInstrumentMyNoSqlEntity> _readRepository;
        private const string TableName = "instruments";
        private readonly List<ITradingInstrument> _instrumentLocalCache;

        public TradingInstrumentCache(IMyNoSqlSubscriber tcpConnection)
        {
            var readRepository =
                new MyNoSqlReadRepository<TradingInstrumentMyNoSqlEntity>(tcpConnection, TableName);
            _readRepository = readRepository;

            _instrumentLocalCache = GetAll().ToList();
            _readRepository.SubscribeToUpdateEvents(
                items =>
                {
                    lock (_instrumentLocalCache)
                    {
                        foreach (var entity in items)
                        {
                            var existingInstrument = _instrumentLocalCache.FirstOrDefault(i => i.Id == entity.Id);
                            if (existingInstrument != null)
                            {
                                _instrumentLocalCache.Remove(existingInstrument);
                            }

                            _instrumentLocalCache.Add(entity);
                        }
                    }
                },
                items =>
                {
                    lock (_instrumentLocalCache)
                    {
                        foreach (var entity in items)
                        {
                            var existingInstrument = _instrumentLocalCache.FirstOrDefault(i => i.Id == entity.Id);
                            if (existingInstrument != null)
                            {
                                _instrumentLocalCache.Remove(existingInstrument);
                            }
                        }
                    }
                });

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

        public ITradingInstrument? FindByCurrency(string first, string second)
        {
            lock (_instrumentLocalCache)
            {
                return _instrumentLocalCache
                    .FirstOrDefault(i => i.Base == first && i.Quote == second ||
                                         i.Base == second && i.Quote == first);
            }
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
