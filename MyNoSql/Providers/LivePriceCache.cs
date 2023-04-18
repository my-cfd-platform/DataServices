﻿using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataReader;

namespace DataServices.MyNoSql.Providers;

public class LivePriceCache : ICache<ILivePrice>
{
    private readonly IMyNoSqlServerDataReader<LivePriceEntity> _readRepository;
    private readonly Dictionary<Type, List<Action<IReadOnlyList<ILivePrice>>>> _subscribersOnChanges = new();
    private readonly List<Action<IReadOnlyList<ILivePrice>>> _subscribersOnDelete = new();

    private const string TableName = "bidask-snapshots";

    public LivePriceCache(IMyNoSqlSubscriber tcpConnection)
    {
        var readRepository =
            new MyNoSqlReadRepository<LivePriceEntity>(tcpConnection, TableName);
        _readRepository = readRepository;
        _readRepository.SubscribeToUpdateEvents(
            items =>
            {
                lock (_subscribersOnChanges)
                {
                    foreach (var (_, subscribersOnChanges) in _subscribersOnChanges)
                    foreach (var change in subscribersOnChanges)
                    {
                        change(items);
                    }

                }
            },
            items =>
            {
                foreach (var action in _subscribersOnDelete)
                    action(items);
            });

    }

    public IEnumerable<ILivePrice> GetAll()
    {
        var partitionKey = LivePriceEntity.GeneratePartitionKey();
        return _readRepository.Get(partitionKey);
    }

    public ILivePrice Get(string id)
    {
        var partitionKey = LivePriceEntity.GeneratePartitionKey();
        var rowKey = LivePriceEntity.GenerateRowKey(id);
        return _readRepository.Get(partitionKey, rowKey);
    }

    public void SubscribeOnChanges(Type type, Action<IReadOnlyList<ILivePrice>> priceChanges)
    {
        lock (_subscribersOnChanges)
        {
            if (!_subscribersOnChanges.ContainsKey(type))
            {
                _subscribersOnChanges.Add(type, new());
            }

            _subscribersOnChanges[type].Add(priceChanges);
        }
    }

    public void UnsubscribeOnChanges(Type type)
    {
        lock (_subscribersOnChanges)
        {
            if (_subscribersOnChanges.ContainsKey(type))
            {
                _subscribersOnChanges.Remove(type);
            }
        }
    }
}