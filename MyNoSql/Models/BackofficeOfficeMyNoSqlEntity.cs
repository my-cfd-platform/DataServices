using DataServices.Extensions;
using DataServices.MyNoSql.Interfaces;
using MyNoSqlServer.Abstractions;

namespace DataServices.MyNoSql.Models;

public class BackofficeOfficeMyNoSqlEntity : MyNoSqlDbEntity, IBackofficeOffice
{
    public static string GeneratePartitionKey() => "bt";

    public static string GenerateRowKey(string id) => id;

    public string Id
    {
        get => this.RowKey;
        set => this.RowKey = value;
    }

    public string Name { get; set;}
    public bool IsDisabled { get; set;}
    public string BrandId { get; set;}

    public static BackofficeOfficeMyNoSqlEntity Create(IBackofficeOffice src)
    {
        if(src.Id.IsNullOrEmpty()) 
            src.Id = Guid.NewGuid().ToString();
        return new BackofficeOfficeMyNoSqlEntity
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = GenerateRowKey(src.Id),
            Name = src.Name,
            IsDisabled = src.IsDisabled,
            BrandId = src.BrandId,
        };
    }
}