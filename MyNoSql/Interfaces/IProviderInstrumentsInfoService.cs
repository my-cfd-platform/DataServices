using DataServices.Models.ProviderInstruments;

namespace DataServices.MyNoSql.Interfaces;

public interface IProviderInstrumentsInfoService
{
    Task<Dictionary<string, string>> Instruments(string providerId);
    IEnumerable<string> GetAvailableInfoProviders();
    Task<IEnumerable<ProviderInstrumentInfo>> InstrumentsInfo(string providerId);
}