namespace DataServices.MyNoSql.Interfaces
{
    public interface IInstrumentCache: ICache<ITradingInstrument>
    {
        ITradingInstrument? FindByCurrency(string first, string second);
    }
}