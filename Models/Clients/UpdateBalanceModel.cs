using System.ComponentModel.DataAnnotations;
using AccountsManager;

namespace DataServices.Models.Clients
{
    public class UpdateBalanceModel
    {
        [Required]
        public string TraderId { get; set; }

        [Required]
        public string AccountId { get; set; }

        [Required]
        public double Delta { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        public UpdateBalanceReason Reason { get; set; }

        public bool AllowNegativeBalance { get; set; }

        [Required]
        public bool IsLive { get; set; }

        public string ReferenceTransactionId { get; set; } = string.Empty;

        [Required]
        public string ChangeBalanceApiKey { get; set; }
    }
}