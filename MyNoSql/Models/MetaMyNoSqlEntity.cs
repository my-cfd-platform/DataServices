using DataServices.MyNoSql.Interfaces;
using MyNoSqlServer.Abstractions;

namespace DataServices.MyNoSql.Models;

public class MetaMyNoSqlEntity : MyNoSqlDbEntity, IMeta
{
    public static string GenerateRowKey(string id) => id;
    public static string GeneratePartitionKey()
    {
        return "mt";
    }

    public string Id
    {
        get => RowKey;
        init => RowKey = value;
    }

    public static MetaMyNoSqlEntity Create(IMeta src)
    {
        return new MetaMyNoSqlEntity
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = GenerateRowKey(src.Id)
        };
    }
}