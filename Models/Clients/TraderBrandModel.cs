using DataServices.Extensions;
using TraderCredentials;

namespace DataServices.Models.Clients;

public class TraderBrandModel
{
    public string TraderId { get; set; }

    public string Brand { get; set; }

    public DateTime RegistrationDate { get; set; }

    public static TraderBrandModel FromGrpc(SearchEmailByIdModel src)
    {
        return new TraderBrandModel
        {
            TraderId = src.TraderId,
            Brand = src.Brand,
            RegistrationDate = src.RegistrationDate.EpochMicToDateTime()
        };
    }
}
