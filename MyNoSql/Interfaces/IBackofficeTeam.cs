﻿using DataServices.MyNoSql.Enums;

namespace DataServices.MyNoSql.Interfaces;

public interface IBackofficeTeam
{
    public string Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> OfficeIds { get; set; }
    public string TeamLeadId { get; set; }
    public TeamType Type { get; set; }
}