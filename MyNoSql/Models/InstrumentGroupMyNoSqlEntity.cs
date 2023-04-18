using DataServices.MyNoSql.Interfaces;
using MyNoSqlServer.Abstractions;

namespace DataServices.MyNoSql.Models;

public class InstrumentGroupMyNoSqlEntity : MyNoSqlDbEntity, IInstrumentGroup
{
    public static string GeneratePartitionKey() => "ig";

    public static string GenerateRowKey(string id) => id;

    public string Id => this.RowKey;

    public string Name { get; set; }

    public int Weight { get; set; }
    public bool Hidden { get; set; }

    public static InstrumentGroupMyNoSqlEntity Create(IInstrumentGroup src)
    {
        return new InstrumentGroupMyNoSqlEntity
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = GenerateRowKey(src.Id),
            Name = src.Name,
            Weight = src.Weight,
            Hidden = src.Hidden
        };
    }
}