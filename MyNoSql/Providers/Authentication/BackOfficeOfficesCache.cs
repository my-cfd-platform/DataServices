﻿using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Interfaces.Authentication;
using DataServices.MyNoSql.Models.Authentication;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataReader;

namespace DataServices.MyNoSql.Providers.Authentication;

public class BackOfficeOfficesCache : ICache<IBackofficeOffice>
{
    private readonly IMyNoSqlServerDataReader<BackofficeOfficeMyNoSqlEntity> _readRepository;
    private const string TableName = "backoffice-office";

    public BackOfficeOfficesCache(IMyNoSqlServerDataReader<BackofficeOfficeMyNoSqlEntity> readRepository)
    {
        _readRepository = readRepository;
    }

    public BackOfficeOfficesCache(IMyNoSqlSubscriber tcpConnection)
    {
        var readRepository =
            new MyNoSqlReadRepository<BackofficeOfficeMyNoSqlEntity>(tcpConnection, TableName);
        _readRepository = readRepository;
    }

    public IEnumerable<IBackofficeOffice> GetAll()
    {
        var partitionKey = BackofficeOfficeMyNoSqlEntity.GeneratePartitionKey();
        return GetAll(partitionKey);
    }

    public IEnumerable<IBackofficeOffice> GetAll(string partitionKey)
    {
        return _readRepository.Get(partitionKey);
    }

    public IBackofficeOffice Get(string id)
    {
        var partitionKey = BackofficeOfficeMyNoSqlEntity.GeneratePartitionKey();
        var rowKey = BackofficeOfficeMyNoSqlEntity.GenerateRowKey(id);
        return Get(rowKey, partitionKey);
    }

    public IBackofficeOffice Get(string id, string partitionKey)
    {
        return _readRepository.Get(partitionKey, id);
    }

    public void SubscribeOnChanges(Type type, Action<IReadOnlyList<IBackofficeOffice>> changes)
    {
        throw new NotImplementedException();
    }

    public void UnsubscribeOnChanges(Type type)
    {
        throw new NotImplementedException();
    }
}