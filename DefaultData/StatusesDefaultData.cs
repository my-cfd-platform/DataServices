// ReSharper disable ArrangeObjectCreationWhenTypeNotEvident
using DataServices.Models.Backoffice;
using DataServices.MyNoSql.Interfaces;


namespace DataServices.DefaultData;

public static class StatusesDefaultData
{
    public static readonly IReadOnlyDictionary<string, IReadOnlyList<IStatus>> Data =
        new Dictionary<string, IReadOnlyList<IStatus>>
        {
            #region Statuses
            {
                StatusType.CrmStatus, new List<IStatus>
                {
                    new StatusModel { Id = "new", Name = "New", Type = StatusType.CrmStatus },
                    new StatusModel { Id = "fullyActivated", Name = "Fully Activated", Type = StatusType.CrmStatus },
                    new StatusModel { Id = "noAnswer", Name = "No Answer", Type = StatusType.CrmStatus },
                    new StatusModel { Id = "highPriority", Name = "High Priority", Type = StatusType.CrmStatus },
                    new StatusModel
                    {
                        Id = "callback", Name = "Callback", Type = StatusType.CrmStatus,
                        Labels = new[] { "Potential", "No Money", "Not Reached", "Objections" }
                    },
                    new StatusModel { Id = "notValidDeletedAccount", Name = "NotValid - Deleted Account", Type = StatusType.CrmStatus },
                    new StatusModel { Id = "notValidWrongNumber", Name = "NotValid - Wrong Number", Type = StatusType.CrmStatus },
                    new StatusModel { Id = "notValidNoPhoneNumber", Name = "NotValid -  No Phone number", Type = StatusType.CrmStatus },
                    new StatusModel { Id = "notValidDuplicateUser", Name = "NotValid - Duplicate User", Type = StatusType.CrmStatus },
                    new StatusModel { Id = "notValidTestLead", Name = "NotValid - Test Lead", Type = StatusType.CrmStatus },
                    new StatusModel { Id = "notValidUnderage", Name = "NotValid - Underage", Type = StatusType.CrmStatus },
                    new StatusModel { Id = "notValidNoLanguageSupport", Name = "NotValid - No language support", Type = StatusType.CrmStatus },
                    new StatusModel { Id = "notValidNeverRegistered", Name = "NotValid - Never registered", Type = StatusType.CrmStatus },
                    new StatusModel
                        { Id = "notValidNonEligibleCountries", Name = "NotValid - Non Eligible countries", Type = StatusType.CrmStatus },
                    new StatusModel
                    {
                        Id = "notInterested", Name = "Not interested", Type = StatusType.CrmStatus,
                        Labels = new[] { "Without Reason", "Wrong Advertisement", "Not Potential" }
                    },
                    new StatusModel { Id = "transfer", Name = "Transfer", Type = StatusType.CrmStatus },
                    new StatusModel { Id = "followUp", Name = "Follow-up", Type = StatusType.CrmStatus },
                    new StatusModel { Id = "conversionRenew", Name = "Conversion Renew", Type = StatusType.CrmStatus }

                }
            },
            #endregion

            #region Trading Statuses
            {
                StatusType.TradingStatus, new List<IStatus>
                {
                    new StatusModel { Id = "newTrader", Name = "New Trader", Type = StatusType.TradingStatus },
                    new StatusModel { Id = "needABonus", Name = "Need a bonus", Type = StatusType.TradingStatus },
                    new StatusModel { Id = "noAnswerNoMoney", Name = "No Answer No Money", Type = StatusType.TradingStatus },
                    new StatusModel { Id = "noAnswerWithFunds", Name = "No Answer With Funds", Type = StatusType.TradingStatus },
                    new StatusModel { Id = "active", Name = "Active", Type = StatusType.TradingStatus },
                    new StatusModel { Id = "noAvailableFunds", Name = "No Available Funds", Type = StatusType.TradingStatus },
                    new StatusModel { Id = "notTrading", Name = "Not Trading", Type = StatusType.TradingStatus },
                    new StatusModel { Id = "potentialRedeposit", Name = "Potential Redeposit", Type = StatusType.TradingStatus },
                    new StatusModel { Id = "backToTrade", Name = "Back To Trade", Type = StatusType.TradingStatus },
                    new StatusModel { Id = "retentionRenew", Name = "Retention Renew", Type = StatusType.TradingStatus },
                    new StatusModel { Id = "transfer", Name = "Transfer", Type = StatusType.TradingStatus },
                }
            },
            #endregion
            /*
            #region Reset Next Call Date Statuses
            {
                "resetNextCallDateStatus", new List<IStatus>
                {
                    new StatusModel { Id = "noAnswer", Name = "No Answer", Type = "resetNextCallDateStatus" },
                    new StatusModel { Id = "notInterested", Name = "Not Interested", Type = "resetNextCallDateStatus" },
                    new StatusModel { Id = "notValidDeletedAccount", Name = "Not Valid - Deleted Account", Type = "resetNextCallDateStatus" },
                    new StatusModel { Id = "notValidWrongNumber", Name = "Not Valid - Wrong Number", Type = "resetNextCallDateStatus" },
                    new StatusModel { Id = "notValidNoPhoneNumber", Name = "Not Valid - No Phone Number", Type = "resetNextCallDateStatus" },
                    new StatusModel { Id = "notValidDuplicateUser", Name = "Not Valid - Duplicate User", Type = "resetNextCallDateStatus" },
                    new StatusModel { Id = "notValidTestLead", Name = "Not Valid - Test Lead", Type = "resetNextCallDateStatus" },
                    new StatusModel { Id = "notValidUnderage", Name = "Not Valid - Underage", Type = "resetNextCallDateStatus" },
                    new StatusModel { Id = "notValidNoLanguageSupport", Name = "Not Valid - No Language Support", Type = "resetNextCallDateStatus" },
                    new StatusModel { Id = "notValidNeverRegistered", Name = "Not Valid - Never Registered", Type = "resetNextCallDateStatus" },
                    new StatusModel { Id = "notValidNonEligibleCountries", Name = "Not Valid - Non Eligible Countries", Type = "resetNextCallDateStatus" },
                }
            },
            #endregion

            #region Trading Reset Next Call Date Statuses
            {
                "resetTradingNextCallDateStatus", new List<IStatus>
                {
                    new StatusModel { Id = "transfer", Name = "Transfer", Type = "resetTradingNextCallDateStatus" },
                }
            },
            #endregion
            */
        };
}