﻿namespace DataServices.MyNoSql.Interfaces;

public interface IProviderRouterSource
{
    public string Id { get; }
    public string LpId { get; set; }
    public string RemoteUrl { get; set; }
    public List<string> InstrumentIds {get;set;}
}