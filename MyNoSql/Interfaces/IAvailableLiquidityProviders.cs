namespace DataServices.MyNoSql.Interfaces;

public interface IAvailableLiquidityProviders
{
    IEnumerable<string> GetLiquidityProviders();
}