namespace DataServices.Models.Auth.Permissions
{
    /// <summary>
    /// Rules for changes:
    /// - new permission must be added to the end of file
    /// - old permission must be marked by [Obsolete] attribute (that will remove them from UI)
    /// </summary>
    public enum PermissionResource
    {
        [PermissionResourceInfo("Client View")]
        ClientView,
        [PermissionResourceInfo("Logs")]
        Logs,
        [PermissionResourceInfo("Kyc")]
        Kyc,
        [PermissionResourceInfo("Traders Online")]
        TradersOnline,
        [PermissionResourceInfo("Deposits")]
        Deposits,
        [PermissionResourceInfo("Withdrawals", PermissionActions.View, PermissionActions.Edit)]
        Withdrawals,
        [PermissionResourceInfo("Transactions Report")]
        TransactionsReport,
        [PermissionResourceInfo("Affiliates", PermissionActions.View, PermissionActions.Edit)]
        Affiliates,
        [PermissionResourceInfo("Phone Pools", PermissionActions.View, PermissionActions.Edit)]
        PhonePools,
        [PermissionResourceInfo("Permissions")]
        Permissions,
        [PermissionResourceInfo("Onboardings", PermissionActions.View, PermissionActions.Edit)]
        Onboardings,
        [PermissionResourceInfo("Brands", PermissionActions.View, PermissionActions.Edit)]
        Brands,
        [PermissionResourceInfo("System Audit Logs", PermissionActions.View)]
        SystemAuditLogs,

        [PermissionResourceInfo(Logs, "Filter")]
        LogsFilter,
        [PermissionResourceInfo(Logs, "Table")]
        LogsTable,
        [PermissionResourceInfo(Logs, "Multi actions")]
        LogsMultiActions,
        [PermissionResourceInfo(ClientView, "Statuses")]
        ClientViewStatuses,
        [PermissionResourceInfo(ClientView, "Personal data")]
        ClientViewPersonalData,
        [PermissionResourceInfo(ClientView, "Crm data")]
        ClientViewCrmData,
        [PermissionResourceInfo(ClientView, "Ownership", PermissionActions.View)]
        ClientViewOwnership,
        [PermissionResourceInfo(ClientView, "Additional phones", PermissionActions.View, PermissionActions.Edit)]
        ClientViewAdditionalPhones,
        [PermissionResourceInfo(ClientView, "External Data ", PermissionActions.View, PermissionActions.Edit)]
        ClientViewExternalData,
        [PermissionResourceInfo(ClientView, "Utm's", PermissionActions.View, PermissionActions.Edit)]
        ClientViewUtm,
        [PermissionResourceInfo(ClientView, "Comments", PermissionActions.View, PermissionActions.Edit)]
        ClientViewComments,
        [PermissionResourceInfo(ClientView, "Accounts", PermissionActions.View, PermissionActions.Edit)]
        ClientViewAccounts,
        [PermissionResourceInfo(ClientView, "Dealing info")]
        ClientViewDealingInfo,
        [PermissionResourceInfo(ClientView, "Deposits", PermissionActions.View, PermissionActions.Edit)]
        ClientViewDeposits,
        [PermissionResourceInfo(ClientView, "Transactions")]
        ClientViewTransactions,
        [PermissionResourceInfo(ClientView, "Active deals")]
        ClientViewActiveDeals,
        [PermissionResourceInfo(ClientView, "Auth logs")]
        ClientViewAuthLogs,
        [PermissionResourceInfo(ClientView, "Audit logs")]
        ClientViewAuditLogs,

        [PermissionResourceInfo(LogsTable, "Registered")]
        LogsTableRegistered,
        [PermissionResourceInfo(LogsTable, "Full name")]
        LogsTableFullName,
        [PermissionResourceInfo(LogsTable, "Phone")]
        LogsTablePhone,
        [PermissionResourceInfo(LogsTable, "Brand Id")]
        LogsTableBrandId,
        [PermissionResourceInfo(LogsTable, "Office")]
        LogsTableOffice,
        [PermissionResourceInfo(LogsTable, "Next call date")]
        LogsTableNextCallDate,
        [PermissionResourceInfo(LogsTable, "Balance")]
        LogsTableBalance,
        [PermissionResourceInfo(LogsTable, "Crm status")]
        LogsTableCrmStatus,
        [PermissionResourceInfo(LogsTable, "Trading status")]
        LogsTableTradingStatus,
        [PermissionResourceInfo(LogsTable, "Utm campaign")]
        LogsTableUtmCampaign,
        [PermissionResourceInfo(LogsTable, "Activation date")]
        LogsTableActivationDate,
        [PermissionResourceInfo(LogsTable, "Last contact date")]
        LogsTableLastContactDate,
        [PermissionResourceInfo(LogsTable, "Aff id")]
        LogsTableAffId,
        [PermissionResourceInfo(LogsTable, "Last comment")]
        LogsTableLastComment,
        [PermissionResourceInfo(LogsTable, "Email")]
        LogsTableEmail,
        [PermissionResourceInfo(LogsTable, "Landing page")]
        LogsTableLandingPage,
        [PermissionResourceInfo(LogsMultiActions, "Change crm status")]
        LogsMultiActionsChangeCrmStatus,
        [PermissionResourceInfo(LogsMultiActions, "Change trading status")]
        LogsMultiActionsChangeTradingStatus,
        [PermissionResourceInfo(LogsMultiActions, "Change office")]
        LogsMultiActionsChangeOffice,
        [PermissionResourceInfo(LogsMultiActions, "Change Retention manager")]
        LogsMultiActionsChangeRetentionManager,
        [PermissionResourceInfo(LogsMultiActions, "Change Conversion manager")]
        LogsMultiActionsChangeConversionManager,
        [PermissionResourceInfo(Kyc, "Documents", PermissionActions.View, PermissionActions.Edit, PermissionActions.Delete)]
        KycDocuments,
        [PermissionResourceInfo(Kyc, "Personal data", PermissionActions.View, PermissionActions.Edit)]
        KycPersonalData,
        [PermissionResourceInfo(Deposits, "Deposits", PermissionActions.View, PermissionActions.Edit)]
        DepositsDeposits,
        [PermissionResourceInfo(Deposits, "Fund account", PermissionActions.View, PermissionActions.Edit)]
        DepositsFundAccount,
        [PermissionResourceInfo(Deposits, "Cascading settings", PermissionActions.View, PermissionActions.Edit)]
        DepositsCascadingSettings,
        [PermissionResourceInfo(Deposits, "Card fund", PermissionActions.View, PermissionActions.Edit)]
        DepositsCardFund,
        [PermissionResourceInfo(Deposits, "Bonus operation", PermissionActions.View, PermissionActions.Edit)]
        DepositsBonusOperation,
        [PermissionResourceInfo(Deposits, "Payment systems settings", PermissionActions.View, PermissionActions.Edit)]
        DepositsPaymentSystemsSettings,
        
        [PermissionResourceInfo(ClientViewStatuses, "Achievement status", PermissionActions.View, PermissionActions.Edit)]
        ClientViewStatusesAchievementStatus,
        [PermissionResourceInfo(ClientViewStatuses, "Crm status", PermissionActions.View, PermissionActions.Edit)]
        ClientViewStatusesCrmStatus,
        [PermissionResourceInfo(ClientViewStatuses, "Trading status", PermissionActions.View, PermissionActions.Edit)]
        ClientViewStatusesTradingStatus,
        [PermissionResourceInfo(ClientViewOwnership, "Office",PermissionActions.View, PermissionActions.Edit)]
        ClientViewOwnershipOffice,
        [PermissionResourceInfo(ClientViewOwnership, "Conversion manager",PermissionActions.View, PermissionActions.Edit)]
        ClientViewOwnershipConversionManager,
        [PermissionResourceInfo(ClientViewOwnership, "Retention manager",PermissionActions.View, PermissionActions.Edit)]
        ClientViewOwnershipRetentionManager,
        [PermissionResourceInfo(ClientViewPersonalData, "Email", PermissionActions.View, PermissionActions.Edit)]
        ClientViewPersonalDataEmail,
        [PermissionResourceInfo(ClientViewPersonalData, "Phone", PermissionActions.View, PermissionActions.Edit)]
        ClientViewPersonalDataPhone,
        [PermissionResourceInfo(ClientViewPersonalData, "FullName", PermissionActions.View, PermissionActions.Edit)]
        ClientViewPersonalDataFullName,
        [PermissionResourceInfo(ClientViewPersonalData, "Birthday", PermissionActions.View, PermissionActions.Edit)]
        ClientViewPersonalDataBirthday,
        [PermissionResourceInfo(ClientViewPersonalData, "Country Of Residence", PermissionActions.View, PermissionActions.Edit)]
        ClientViewPersonalDataCountryOfResidence,
        [PermissionResourceInfo(ClientViewPersonalData, "Country Of Citizenship", PermissionActions.View, PermissionActions.Edit)]
        ClientViewPersonalDataCountryOfCitizenship,
        [PermissionResourceInfo(ClientViewPersonalData, "Address", PermissionActions.View, PermissionActions.Edit)]
        ClientViewPersonalDataAddress,
        [PermissionResourceInfo(ClientViewPersonalData, "City", PermissionActions.View, PermissionActions.Edit)]
        ClientViewPersonalDataCity,
        [PermissionResourceInfo(ClientViewCrmData, "Next call date", PermissionActions.View, PermissionActions.Edit)]
        ClientViewCrmDataNextCallDate,
        [PermissionResourceInfo(Logs, "Download data", PermissionActions.Edit)]
        LogsDownloadData,
        [Obsolete]
        [PermissionResourceInfo(Logs, "Order by last contact date")]
        LogsOrderByLastContactDate,
        [PermissionResourceInfo(LogsFilter, "Brand id")]
        LogsFilterBrandId,
        [PermissionResourceInfo(LogsFilter, "Country of registration")]
        LogsFilterCountryOfRegistration,
        [PermissionResourceInfo(LogsFilter, "Backoffice user id")]
        LogsFilterBackofficeUserId,
        [PermissionResourceInfo(LogsFilter, "Retention manager id ")]
        LogsFilterRetentionManagerId,
        [PermissionResourceInfo(LogsFilter, "Trading status")]
        LogsFilterTradingStatus,
        [PermissionResourceInfo(LogsFilter, "Crm status")]
        LogsFilterCrmStatus,
        [PermissionResourceInfo(LogsFilter, "Activation date")]
        LogsFilterActivationDate,
        [PermissionResourceInfo(LogsFilter, "Utm campaign")]
        LogsFilterUtmCampaing,
        [PermissionResourceInfo(LogsFilter, "Aff id")]
        LogsFilterAffId,
        [PermissionResourceInfo(LogsFilter, "Owner")]
        LogsFilterOwner,
        [PermissionResourceInfo(LogsFilter, "Next call date")]
        LogsFilterNextCallDate,
        [PermissionResourceInfo(LogsFilter, "Last contact date")]
        LogsFilterLastContactDate,
        [PermissionResourceInfo(LogsFilter, "Is internal")]
        LogsFilterIsInternal,
        [PermissionResourceInfo(ClientView, "Is internal/real", PermissionActions.View,  PermissionActions.Edit)]
        ClientViewIsInternal,
        [PermissionResourceInfo(ClientView, "Is withdrawal active/disabled", PermissionActions.View,  PermissionActions.Edit)]
        ClientViewIsWithdrawal,
        [PermissionResourceInfo(TradersOnline, "Table")]
        TradersOnlineTable,
        [PermissionResourceInfo(TradersOnlineTable, "Country")]
        TradersOnlineTableCountry,
        [PermissionResourceInfo(TradersOnlineTable, "Full name")]
        TradersOnlineTableFullName,
        [PermissionResourceInfo(TradersOnlineTable, "Email")]
        TradersOnlineTableEmail,
        [PermissionResourceInfo(TradersOnlineTable, "Office")]
        TradersOnlineTableOffice,
        [PermissionResourceInfo(TradersOnlineTable, "Conversion manager")]
        TradersOnlineTableConversionManager,
        [PermissionResourceInfo(TradersOnlineTable, "Retention manager")]
        TradersOnlineTableRetentionManager,
        [PermissionResourceInfo(TradersOnlineTable, "Last seen")]
        TradersOnlineTableLastSeen,
        [PermissionResourceInfo(TradersOnlineTable, "User agent")]
        TradersOnlineTableUserAgent,
        [PermissionResourceInfo(TransactionsReport, "Table")]
        TransactionsReportTable,
        [PermissionResourceInfo(TransactionsReportTable, "Transaction id")]
        TransactionsReportTablePaymentTransactionId,
        [PermissionResourceInfo(TransactionsReportTable, "Trader id")]
        TransactionsReportTableTraderId,
        [PermissionResourceInfo(TransactionsReportTable, "Account id")]
        TransactionsReportTableAccountId,
        [PermissionResourceInfo(TransactionsReportTable, "Transaction type")]
        TransactionsReportTableTransactionType,
        [PermissionResourceInfo(TransactionsReportTable, "Payment system")]
        TransactionsReportTablePaymentSystem,
        [PermissionResourceInfo(TransactionsReportTable, "Ps transaction id")]
        TransactionsReportTablePsTransactionId,
        [PermissionResourceInfo(TransactionsReportTable, "Payment provider")]
        TransactionsReportTablePaymentProvider,
        [PermissionResourceInfo(TransactionsReportTable, "Ps currency")]
        TransactionsReportTablePsCurrency,
        [PermissionResourceInfo(TransactionsReportTable, "Ps amount")]
        TransactionsReportTablePsAmount,
        [PermissionResourceInfo(TransactionsReportTable, "Currency")]
        TransactionsReportTableCurrency,
        [PermissionResourceInfo(TransactionsReportTable, "Amount")]
        TransactionsReportTableAmount,
        [PermissionResourceInfo(TransactionsReportTable, "Status")]
        TransactionsReportTableStatus,
        [PermissionResourceInfo(TransactionsReportTable, "Transaction date")]
        TransactionsReportTableTransactionDate,
        [PermissionResourceInfo(TransactionsReportTable, "Email")]
        TransactionsReportTableEmail,
        [PermissionResourceInfo(TransactionsReportTable, "First name")]
        TransactionsReportTableFirstName,
        [PermissionResourceInfo(TransactionsReportTable, "Last name")]
        TransactionsReportTableLastName,
        [Obsolete]
        [PermissionResourceInfo(TransactionsReportTable, "Country")]
        TransactionsReportTableCountry,
        [PermissionResourceInfo(TransactionsReportTable, "Country of citizenship")]
        TransactionsReportTableCountryOfCitizenship,
        [PermissionResourceInfo(TransactionsReportTable, "Country of residence")]
        TransactionsReportTableCountryOfResidence,
        [PermissionResourceInfo(TransactionsReportTable, "Country of registration")]
        TransactionsReportTableCountryOfRegistration,
        [PermissionResourceInfo(TransactionsReportTable, "Office")]
        TransactionsReportTableOffice,
        [Obsolete]
        [PermissionResourceInfo(TransactionsReportTable, "Account manager when created")]
        TransactionsReportTableAccountManagerWhenCreated,
        [PermissionResourceInfo(TransactionsReportTable, "Retention manager")]
        TransactionsReportTableRetentionManager,
        [PermissionResourceInfo(TransactionsReportTable, "Is internal")]
        TransactionsReportTableIsInternal,
        [PermissionResourceInfo(TransactionsReportTable, "Trader registration date")]
        TransactionsReportTableTraderRegistrationDate,
        [PermissionResourceInfo(TransactionsReportTable, "Kyc")]
        TransactionsReportTableKyc,
        [PermissionResourceInfo(TransactionsReportTable, "Affiliate id")]
        TransactionsReportTableAffiliateId,
        [PermissionResourceInfo(TransactionsReportTable, "Conversion status")]
        TransactionsReportTableConversionStatus,
        [PermissionResourceInfo(TransactionsReport, "Download data", PermissionActions.Edit)]
        TransactionsReportDownloadData,
        [PermissionResourceInfo(LogsTable, "Retention manager")]
        LogsTableRetentionManager,
        [PermissionResourceInfo(LogsTable, "Conversion manager")]
        LogsTableConversionManager,
        [PermissionResourceInfo(LogsTable, "Country")]
        LogsTableCountry,
        [PermissionResourceInfo(LogsTable, "Id")]
        LogsTableId,
        [PermissionResourceInfo(LogsTable, "Device")]
        LogsTableDevice,
        [PermissionResourceInfo(LogsTable, "Redirected from")]
        LogsTableRedirectedFrom,
        [PermissionResourceInfo(LogsTable, "Ip")]
        LogsTableIp,
        [PermissionResourceInfo(LogsTable, "Process id")]
        LogsTableProcessId,
        [PermissionResourceInfo(LogsTable, "Cxd token")]
        LogsTableCxdToken,
        [PermissionResourceInfo(LogsTable, "Is internal")]
        LogsTableIsInternal,
        [PermissionResourceInfo(LogsFilter, "Registered")]
        LogsFilterRegistered,
        [PermissionResourceInfo(LogsTable, "Crm status label")]
        LogsTableCrmStatusLabel,
        [PermissionResourceInfo(LogsFilter, "Crm status label")]
        LogsFilterCrmStatusLabel,
        [PermissionResourceInfo(ClientView, "KeyValue", PermissionActions.View, PermissionActions.Edit)]
        ClientViewKeyValueData,
        [PermissionResourceInfo("Auto dialer")]
        AutoDialer,
        [PermissionResourceInfo(AutoDialer, "Personal area")]
        AutoDialerPersonalArea,
        [PermissionResourceInfo(AutoDialer, "Campaigns", PermissionActions.View, PermissionActions.Edit)]
        AutoDialerCampaigns,
        [PermissionResourceInfo(AutoDialer, "Statistic")]
        AutoDialerStatistic,
        [PermissionResourceInfo(ClientViewCrmData, "Is Archived", PermissionActions.View, PermissionActions.Edit)]
        ClientViewCrmDataIsArchived,
        [PermissionResourceInfo(LogsTable, "Is Archived")]
        LogsTableIsArchived,
        [PermissionResourceInfo(LogsMultiActions, "Change Is Archived", PermissionActions.Edit)]
        LogsMultiActionsChangeIsArchived,
        [PermissionResourceInfo(AutoDialer, "Users", PermissionActions.View, PermissionActions.Edit)]
        AutoDialerUsersOnline,
        [PermissionResourceInfo(AutoDialer, "Calls")]
        AutoDialerCallLogs,
        [PermissionResourceInfo("Cold Leads")]
        ColdLeads,
        [PermissionResourceInfo(ColdLeads, "Table", PermissionActions.View, PermissionActions.Edit)]
        ColdLeadsTable,
        [PermissionResourceInfo(ColdLeadsTable, "Email")]
        ColdLeadsTableEmail,
        [PermissionResourceInfo(ColdLeadsTable, "Phone")]
        ColdLeadsTablePhone,
        [PermissionResourceInfo(LogsTable, "Multi Registration")]
        LogsTableIsMultiRegistration,
        [PermissionResourceInfo("Welcome Bonuses", PermissionActions.View, PermissionActions.Edit)]
        WelcomeBonuses,
        [PermissionResourceInfo("AB Splits", PermissionActions.View, PermissionActions.Edit)]
        ABSplits,
        [PermissionResourceInfo(TransactionsReportTable, "Transaction Sub Type")]
        TransactionsReportTableTransactionSubType,
        [PermissionResourceInfo(TransactionsReportTable, "Conversion Manager")]
        TransactionsReportTableConversionManager,
        [PermissionResourceInfo("Market Alerts", PermissionActions.View)]
        MarketAlerts,
        [PermissionResourceInfo(MarketAlerts, "Market Movement", PermissionActions.View, PermissionActions.Edit)]
        MarketAlertsMarketMovement,
        [PermissionResourceInfo("Educations", PermissionActions.View, PermissionActions.Edit)]
        Educations,
        [PermissionResourceInfo(Permissions, "Users", PermissionActions.View, PermissionActions.Edit)]
        PermissionsUsers,
        [PermissionResourceInfo(Permissions, "Teams", PermissionActions.View, PermissionActions.Edit)]
        PermissionsTeams,
        [PermissionResourceInfo(Permissions, "Roles", PermissionActions.View, PermissionActions.Edit)]
        PermissionsRoles,
        [PermissionResourceInfo(Permissions, "Offices", PermissionActions.View, PermissionActions.Edit)]
        PermissionsOffices,
        [PermissionResourceInfo(Permissions, "Auto owner", PermissionActions.View, PermissionActions.Edit)]
        PermissionsAutoOwner,
        [PermissionResourceInfo(ClientViewCrmData, "Activation date", PermissionActions.View, PermissionActions.Edit)]
        ClientViewCrmDataActivationDate,
        [PermissionResourceInfo(ClientView, "Questionary tab")]
        ClientViewQuestionaryTab,
        [PermissionResourceInfo(ClientViewQuestionaryTab, "Age", PermissionActions.View, PermissionActions.Edit)]
        ClientViewQuestionaryTabAge,
        [PermissionResourceInfo(ClientViewQuestionaryTab, "Sex", PermissionActions.View, PermissionActions.Edit)]
        ClientViewQuestionaryTabSex,
        [PermissionResourceInfo(ClientViewQuestionaryTab, "Occupation", PermissionActions.View, PermissionActions.Edit)]
        ClientViewQuestionaryTabOccupation,
        [PermissionResourceInfo(ClientViewQuestionaryTab, "Experience", PermissionActions.View, PermissionActions.Edit)]
        ClientViewQuestionaryTabExperience,
        [PermissionResourceInfo(ClientViewCrmData, "First deposit amount")]
        ClientViewCrmDataFirstDepositAmount,
        [PermissionResourceInfo(ClientViewQuestionaryTab, "Redeposit potential", PermissionActions.View, PermissionActions.Edit)]
        ClientViewQuestionaryTabRedepositPotential,
        [PermissionResourceInfo(ClientViewQuestionaryTab, "Expected Deposit", PermissionActions.View, PermissionActions.Edit)]
        ClientViewQuestionaryTabExpectedDeposit,
        [PermissionResourceInfo(LogsTable, "Sex")]
        LogsTableSex,
        [PermissionResourceInfo(LogsTable, "Age")]
        LogsTableAgeCategory,
        [PermissionResourceInfo(LogsTable, "Occupation")]
        LogsTableOccupationType,
        [PermissionResourceInfo(LogsTable, "Experience")]
        LogsTableExperienceType,
        [PermissionResourceInfo(LogsTable, "First deposit amount")]
        LogsTableFirstDepositAmount,
        [PermissionResourceInfo(LogsTable, "Redeposit potential")]
        LogsTableDepositPotentialType,
        [PermissionResourceInfo(LogsTable, "Expected Deposit")]
        LogsTableExpectedDepositAmount,
        [PermissionResourceInfo(ClientView, "AB Split", PermissionActions.View, PermissionActions.Edit)]
        ClientViewABSplit,
        [PermissionResourceInfo("Account types", PermissionActions.View, PermissionActions.Edit)]
        AccountTypes,
        [PermissionResourceInfo(ClientViewCrmData, "Card Deposit Disabled", PermissionActions.View, PermissionActions.Edit)]
        ClientViewCrmDataCardDepositDisabled,
        [PermissionResourceInfo("Notifications", PermissionActions.View, PermissionActions.Edit)]
        Notifications,
        [PermissionResourceInfo(Notifications, "Deposit Failed")]
        NotificationsDepositFailed,
        [PermissionResourceInfo(MarketAlerts, "Margin Call", PermissionActions.View, PermissionActions.Edit)]
        MarketAlertsMarginCall,
        [PermissionResourceInfo(Notifications, "Deposit Approved")]
        NotificationsDepositApproved,
        [PermissionResourceInfo(Notifications, "Withdrawal Created")]
        NotificationsWithdrawalCreated,
        [PermissionResourceInfo(Notifications, "Trader Logged On")]
        NotificationsTraderLoggedOn,
        [PermissionResourceInfo(Notifications, "Margin Call")]
        NotificationsMarginCall,
        [PermissionResourceInfo(ClientViewCrmData, "Account type ID", PermissionActions.View)]
        ClientViewCrmDataAccountTypeId,
        [PermissionResourceInfo(ClientViewCrmData, "Account type manual ID", PermissionActions.View, PermissionActions.Edit)]
        ClientViewCrmDataAccountTypeManualId,
        [PermissionResourceInfo("Calendar", PermissionActions.Edit)]
        Calendar,
        [PermissionResourceInfo("Requests Center", PermissionActions.View, PermissionActions.Download)]
        RequestsCenter,
        [PermissionResourceInfo(RequestsCenter, "Verification")]
        RequestsCenterVerification,
        [PermissionResourceInfo(RequestsCenter, "Withdrawal")]
        RequestsCenterWithdrawal,
        [PermissionResourceInfo(RequestsCenter, "Call Back")]
        RequestsCenterCallBack,
        [PermissionResourceInfo(RequestsCenter, "Phone")]
        RequestsCenterPhone,
        [PermissionResourceInfo(RequestsCenter, "Change Email")]
        RequestsCenterChangeEmail,
        [PermissionResourceInfo(RequestsCenter, "Deposit Help")]
        RequestsCenterDepositHelp,
        [PermissionResourceInfo(RequestsCenter, "Reassign")]
        RequestsCenterReassign,
        [PermissionResourceInfo(RequestsCenter, "Trading Group")]
        RequestsCenterTradingGroup,
        [PermissionResourceInfo(RequestsCenter, "Wire")]
        RequestsCenterWire,
        [PermissionResourceInfo(RequestsCenter, "Bonus Deposit")]
        RequestsCenterBonusDeposit,
        [PermissionResourceInfo(RequestsCenter, "Bonus Withdrawal")]
        RequestsCenterBonusWithdrawal,
        [PermissionResourceInfo(ClientView, "Requests", PermissionActions.View, PermissionActions.Edit, PermissionActions.Add)]
        ClientViewRequests,
        [PermissionResourceInfo(ClientViewRequests, "Verification", PermissionActions.View, PermissionActions.Edit, PermissionActions.Add)]
        ClientViewRequestsVerification,
        [PermissionResourceInfo(ClientViewRequests, "Withdrawal", PermissionActions.View, PermissionActions.Edit, PermissionActions.Add)]
        ClientViewRequestsWithdrawal,
        [PermissionResourceInfo(ClientViewRequests, "CallBack", PermissionActions.View, PermissionActions.Edit, PermissionActions.Add)]
        ClientViewRequestsCallBack,
        [PermissionResourceInfo(ClientViewRequests, "Phone", PermissionActions.View, PermissionActions.Edit, PermissionActions.Add)]
        ClientViewRequestsPhone,
        [PermissionResourceInfo(ClientViewRequests, "Change email", PermissionActions.View, PermissionActions.Edit, PermissionActions.Add)]
        ClientViewRequestsChangeEmail,
        [PermissionResourceInfo(ClientViewRequests, "Deposit help", PermissionActions.View, PermissionActions.Edit, PermissionActions.Add)]
        ClientViewRequestsDepositHelp,
        [PermissionResourceInfo(ClientViewRequests, "Reassign", PermissionActions.View, PermissionActions.Edit, PermissionActions.Add)]
        ClientViewRequestsReassign,
        [PermissionResourceInfo(ClientViewRequests, "Trading group", PermissionActions.View, PermissionActions.Edit, PermissionActions.Add)]
        ClientViewRequestsTradingGroup,
        [PermissionResourceInfo(ClientViewRequests, "Wire", PermissionActions.View, PermissionActions.Edit, PermissionActions.Add)]
        ClientViewRequestsWire,
        [PermissionResourceInfo(ClientViewRequests, "Bonus deposit", PermissionActions.View, PermissionActions.Edit, PermissionActions.Add)]
        ClientViewRequestsBonusDeposit,
        [PermissionResourceInfo(ClientViewRequests, "Bonus withdrawal", PermissionActions.View, PermissionActions.Edit, PermissionActions.Add)]
        ClientViewRequestsBonusWithdrawal,
        [PermissionResourceInfo(ClientViewRequests, "Tech Problem", PermissionActions.View, PermissionActions.Edit, PermissionActions.Add)]
        ClientViewRequestsTechProblem,
        [PermissionResourceInfo(RequestsCenter, "Tech Problem")]
        RequestsCenterTechProblem,
        [PermissionResourceInfo(ClientViewCrmDataChangePassword, "Set manually")]
        ClientViewCrmDataChangePasswordSet,
        [PermissionResourceInfo(ClientViewCrmDataChangePassword, "Generate random")]
        ClientViewCrmDataChangePasswordGenerate,
        [PermissionResourceInfo(ClientViewCrmData, "Change Password")]
        ClientViewCrmDataChangePassword,
        [PermissionResourceInfo(PermissionResource.ClientView, "Communication", PermissionActions.View, PermissionActions.Add, PermissionActions.Import)]
        ClientViewClientCommunication,
        [PermissionResourceInfo(PhonePools, "Whatsapp", PermissionActions.View, PermissionActions.Edit, PermissionActions.Add, PermissionActions.Delete)]
        PhonePoolsWhatsapp,
        [PermissionResourceInfo(Notifications, "Client message received")]
        NotificationsClientMessageReceived,
        [PermissionResourceInfo(PermissionResource.ClientViewClientCommunication, "Phone selection")]
        ClientViewClientCommunicationPhoneSelection,
        [PermissionResourceInfo("Inactivity fee")]
        InactivityFee,
        [PermissionResourceInfo(InactivityFee, "Inactivity fee settings", PermissionActions.View, PermissionActions.Edit)]
        InactivityFeeSettings,
        [PermissionResourceInfo(InactivityFee, "Inactivity fee history")]
        InactivityFeeHistory,
        [PermissionResourceInfo("System messages", PermissionActions.View, PermissionActions.Add, PermissionActions.Edit, PermissionActions.Delete)]
        SystemMessages,
        [PermissionResourceInfo(ClientView, "Sessions", PermissionActions.View, PermissionActions.Delete)]
        ClientViewSessions,
        [PermissionResourceInfo(ClientView, "Wallets", PermissionActions.View, PermissionActions.Edit)]
        ClientViewWallets,
        [PermissionResourceInfo("Voiso Integration", PermissionActions.View, PermissionActions.Edit, PermissionActions.Delete)]
        VoisoIntegration,
    }
}