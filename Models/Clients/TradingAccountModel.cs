using AccountsManager;
using DataServices.Extensions;

namespace DataServices.Models.Clients
{
    public class TradingAccountModel
    {
        public string TraderId { get; set; }
        
        public string AccountId { get; set; }
        
        public string Currency { get; set; }
        
        public double Balance { get; set; }
        
        public double Bonus { get; set; }
        
        public string TradingGroup { get; set; }
        
        public DateTime TimeStamp { get; set; }
        
        public bool TradingDisabled { get; set; }
        
        public bool IsInternal { get; set; }
        
        public static TradingAccountModel FromGrpc(AccountGrpcModel src)
        {
            return new TradingAccountModel
            {
                TraderId = src.TraderId,
                AccountId = src.Id,
                Currency = src.Currency,
                TradingGroup = src.TradingGroup,
                Balance = src.Balance,
                //Bonus = src.Bonus,
                TimeStamp = src.LastUpdateDate.EpochMilToDateTime(),
                TradingDisabled = src.TradingDisabled,
            };
        }
    }    
}