namespace DataServices.MyNoSql.Interfaces;

public interface IInstrumentGroup
{
    string Id { get; }
    string Name { get; }
    int Weight { get; }
    bool Hidden { get; }
}