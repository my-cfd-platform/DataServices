namespace DataServices.Models.Auth.Permissions
{
    /// <summary>
    /// Rules for changes:
    /// Delete or add in any order, do not rename when permission is in use
    /// </summary>
    public enum PermissionResource
    {
        #region ClientView

        [PermissionResourceInfo("Client View")]
        ClientView,
        [PermissionResourceInfo(ClientView, "Statuses")]
        ClientViewStatuses,
        [PermissionResourceInfo(ClientView, "Personal data")]
        ClientViewPersonalData,
        [PermissionResourceInfo(ClientView, "CRM data")]
        ClientViewCrmData,
        [PermissionResourceInfo(ClientView, "Ownership", PermissionActions.View)]
        ClientViewOwnership,
        [PermissionResourceInfo(ClientView, "Additional phones", PermissionActions.View, PermissionActions.Edit)]
        ClientViewAdditionalPhones,
        [PermissionResourceInfo(ClientView, "External Data ", PermissionActions.View, PermissionActions.Edit)]
        ClientViewExternalData,
        [PermissionResourceInfo(ClientView, "Utm's", PermissionActions.View, PermissionActions.Edit)]
        ClientViewUtm,
        [PermissionResourceInfo(ClientView, "Comments", PermissionActions.View, PermissionActions.Add)]
        ClientViewComments,
        [PermissionResourceInfo(ClientView, "Accounts", PermissionActions.View, PermissionActions.Edit)]
        ClientViewAccounts,
        [PermissionResourceInfo(ClientView, "Dealing info")]
        ClientViewDealingInfo,
        [PermissionResourceInfo(ClientView, "Deposits", PermissionActions.View, PermissionActions.Edit)]
        ClientViewDeposits,
        [PermissionResourceInfo(ClientView, "Withdrawals", PermissionActions.View, PermissionActions.Edit)]
        ClientViewWithdrawals,
        [PermissionResourceInfo(ClientView, "Transactions")]
        ClientViewTransactions,
        [PermissionResourceInfo(ClientView, "Active deals")]
        ClientViewActiveDeals,
        [PermissionResourceInfo(ClientView, "Auth logs")]
        ClientViewAuthLogs,
        [PermissionResourceInfo(ClientView, "Audit logs")]
        ClientViewAuditLogs,
        [PermissionResourceInfo(ClientView, "Documents", PermissionActions.View, PermissionActions.Add, PermissionActions.Edit)]
        ClientViewDocuments,
        [PermissionResourceInfo(ClientView, "KYC Status", PermissionActions.View, PermissionActions.Edit)]
        ClientViewKycStatus,
        #endregion
        #region Personal Data

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
        [PermissionResourceInfo(ClientViewPersonalData, "Sex", PermissionActions.View, PermissionActions.Edit)]
        ClientViewPersonalDataSex,
        
        #endregion

        #region ClientsList

        [PermissionResourceInfo("Clients List")]
        ClientsList,
        [PermissionResourceInfo(ClientsList, "Filter")]
        ClientsListFilter,
        [PermissionResourceInfo(ClientsList, "Table")]
        ClientsListTable,
        [PermissionResourceInfo(ClientsList, "Multi actions")]
        ClientsListMultiActions,
        [PermissionResourceInfo(ClientsList, "Download data", PermissionActions.Edit)]
        ClientsListDownloadData,
        [PermissionResourceInfo(ClientsListFilter, "Brand id")]
        ClientsListFilterBrandId,
        [PermissionResourceInfo(ClientsListFilter, "Country of registration")]
        ClientsListFilterCountryOfRegistration,
        [PermissionResourceInfo(ClientsListFilter, "Backoffice user id")]
        ClientsListFilterBackofficeUserId,
        [PermissionResourceInfo(ClientsListFilter, "Retention manager id ")]
        ClientsListFilterRetentionManagerId,
        [PermissionResourceInfo(ClientsListFilter, "Trading status")]
        ClientsListFilterTradingStatus,
        [PermissionResourceInfo(ClientsListFilter, "CRM status")]
        ClientsListFilterCrmStatus,
        [PermissionResourceInfo(ClientsListFilter, "Activation date")]
        ClientsListFilterActivationDate,
        [PermissionResourceInfo(ClientsListFilter, "Utm campaign")]
        ClientsListFilterUtmCampaign,
        [PermissionResourceInfo(ClientsListFilter, "Aff id")]
        ClientsListFilterAffId,
        [PermissionResourceInfo(ClientsListFilter, "Owner")]
        ClientsListFilterOwner,
        [PermissionResourceInfo(ClientsListFilter, "Next call date")]
        ClientsListFilterNextCallDate,
        [PermissionResourceInfo(ClientsListFilter, "Last contact date")]
        ClientsListFilterLastContactDate,
        [PermissionResourceInfo(ClientsListFilter, "Is internal")]
        ClientsListFilterIsInternal,
        [PermissionResourceInfo(ClientsListTable, "Registered")]
        ClientsListTableRegistered,
        [PermissionResourceInfo(ClientsListTable, "Full name")]
        ClientsListTableFullName,
        [PermissionResourceInfo(ClientsListTable, "Phone")]
        ClientsListTablePhone,
        [PermissionResourceInfo(ClientsListTable, "Brand Id")]
        ClientsListTableBrandId,
        [PermissionResourceInfo(ClientsListTable, "Office")]
        ClientsListTableOffice,
        [PermissionResourceInfo(ClientsListTable, "Next call date")]
        ClientsListTableNextCallDate,
        [PermissionResourceInfo(ClientsListTable, "Balance")]
        ClientsListTableBalance,
        [PermissionResourceInfo(ClientsListTable, "CRM status")]
        ClientsListTableCrmStatus,
        [PermissionResourceInfo(ClientsListTable, "Trading status")]
        ClientsListTableTradingStatus,
        [PermissionResourceInfo(ClientsListTable, "Utm campaign")]
        ClientsListTableUtmCampaign,
        [PermissionResourceInfo(ClientsListTable, "Activation date")]
        ClientsListTableActivationDate,
        [PermissionResourceInfo(ClientsListTable, "Last contact date")]
        ClientsListTableLastContactDate,
        [PermissionResourceInfo(ClientsListTable, "Aff id")]
        ClientsListTableAffId,
        [PermissionResourceInfo(ClientsListTable, "Last comment")]
        ClientsListTableLastComment,
        [PermissionResourceInfo(ClientsListTable, "Email")]
        ClientsListTableEmail,
        [PermissionResourceInfo(ClientsListTable, "Landing page")]
        ClientsListTableLandingPage,
        [PermissionResourceInfo(ClientsListMultiActions, "Change crm status")]
        ClientsListMultiActionsChangeCrmStatus,
        [PermissionResourceInfo(ClientsListMultiActions, "Change trading status")]
        ClientsListMultiActionsChangeTradingStatus,
        [PermissionResourceInfo(ClientsListMultiActions, "Change office")]
        ClientsListMultiActionsChangeOffice,
        [PermissionResourceInfo(ClientsListMultiActions, "Change Retention manager")]
        ClientsListMultiActionsChangeRetentionManager,
        [PermissionResourceInfo(ClientsListMultiActions, "Change Conversion manager")]
        ClientsListMultiActionsChangeConversionManager,
        [PermissionResourceInfo(ClientsListTable, "Retention manager")]
        ClientsListRetentionManager,
        [PermissionResourceInfo(ClientsListTable, "Conversion manager")]
        ClientsListTableConversionManager,
        [PermissionResourceInfo(ClientsListTable, "Country")]
        ClientsListTableCountry,
        [PermissionResourceInfo(ClientsListTable, "Id")]
        ClientsListTableId,
        [PermissionResourceInfo(ClientsListTable, "Device")]
        ClientsListTableDevice,
        [PermissionResourceInfo(ClientsListTable, "Redirected from")]
        ClientsListTableRedirectedFrom,
        [PermissionResourceInfo(ClientsListTable, "Ip")]
        ClientsListTableIp,
        [PermissionResourceInfo(ClientsListTable, "Process id")]
        ClientsListTableProcessId,
        [PermissionResourceInfo(ClientsListTable, "Cxd token")]
        ClientsListTableCxdToken,
        [PermissionResourceInfo(ClientsListTable, "Is internal")]
        ClientsListTableIsInternal,
        [PermissionResourceInfo(ClientsListFilter, "Registered")]
        ClientsListFilterRegistered,
        [PermissionResourceInfo(ClientsListTable, "CRM status label")]
        ClientsListTableCrmStatusLabel,
        [PermissionResourceInfo(ClientsListFilter, "CRM status label")]
        ClientsListFilterCrmStatusLabel,
        [PermissionResourceInfo(ClientsListTable, "Is Archived")]
        ClientsListTableIsArchived,
        [PermissionResourceInfo(ClientsListMultiActions, "Change Is Archived", PermissionActions.Edit)]
        ClientsListMultiActionsChangeIsArchived,
        [PermissionResourceInfo(ClientsListTable, "Multi Registration")]
        ClientsListTableIsMultiRegistration,
        [PermissionResourceInfo(ClientsListTable, "Sex")]
        LogsTableSex,
        [PermissionResourceInfo(ClientsListTable, "Age")]
        LogsTableAgeCategory,
        [PermissionResourceInfo(ClientsListTable, "Occupation")]
        LogsTableOccupationType,
        [PermissionResourceInfo(ClientsListTable, "Experience")]
        LogsTableExperienceType,
        [PermissionResourceInfo(ClientsListTable, "First deposit amount")]
        LogsTableFirstDepositAmount,
        [PermissionResourceInfo(ClientsListTable, "Redeposit potential")]
        LogsTableDepositPotentialType,
        [PermissionResourceInfo(ClientsListTable, "Expected Deposit")]
        LogsTableExpectedDepositAmount,
        #endregion

        #region KYC

        [PermissionResourceInfo("Kyc")]
        Kyc,
        [PermissionResourceInfo(Kyc, "Documents", PermissionActions.View, PermissionActions.Edit, PermissionActions.Delete)]
        KycDocuments,
        [PermissionResourceInfo(Kyc, "Personal data", PermissionActions.View, PermissionActions.Edit)]
        KycPersonalData,
        [PermissionResourceInfo(Kyc, "KYC Status", PermissionActions.View, PermissionActions.Edit)]
        KycStatus,
        #endregion

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
        [PermissionResourceInfo(ClientViewStatuses, "CRM status", PermissionActions.View, PermissionActions.Edit)]
        ClientViewStatusesCrmStatus,
        [PermissionResourceInfo(ClientViewStatuses, "Trading status", PermissionActions.View, PermissionActions.Edit)]
        ClientViewStatusesTradingStatus,
        [PermissionResourceInfo(ClientViewOwnership, "Office",PermissionActions.View, PermissionActions.Edit)]
        ClientViewOwnershipOffice,
        [PermissionResourceInfo(ClientViewOwnership, "Conversion manager",PermissionActions.View, PermissionActions.Edit)]
        ClientViewOwnershipConversionManager,
        [PermissionResourceInfo(ClientViewOwnershipConversionManager, "View all managers")]
        ClientViewOwnershipAllConversionManagers,
        [PermissionResourceInfo(ClientViewOwnership, "Retention manager",PermissionActions.View, PermissionActions.Edit)]
        ClientViewOwnershipRetentionManager,
        [PermissionResourceInfo(ClientViewOwnershipRetentionManager, "View all managers")]
        ClientViewOwnershipAllRetentionManagers,
        [PermissionResourceInfo(ClientViewCrmData, "Next call date", PermissionActions.View, PermissionActions.Edit)]
        ClientViewCrmDataNextCallDate,
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
        [PermissionResourceInfo(TransactionsReportTable, "Country of citizenship")]
        TransactionsReportTableCountryOfCitizenship,
        [PermissionResourceInfo(TransactionsReportTable, "Country of residence")]
        TransactionsReportTableCountryOfResidence,
        [PermissionResourceInfo(TransactionsReportTable, "Country of registration")]
        TransactionsReportTableCountryOfRegistration,
        [PermissionResourceInfo(TransactionsReportTable, "Office")]
        TransactionsReportTableOffice,
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