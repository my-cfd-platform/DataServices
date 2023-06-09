﻿using DataServices.Models.Auth.Permissions;

namespace DataServices.MyNoSql.Interfaces;

public interface IBackofficeRole
{
    string Id { get; }
    public string Name { get; set; }
    public IEnumerable<Permission> Permissions { get; set; }
}