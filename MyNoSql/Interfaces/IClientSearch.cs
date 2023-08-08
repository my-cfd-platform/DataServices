using DataServices.Models.Clients.Search;

namespace DataServices.MyNoSql.Interfaces;

public interface IClientSearch
{
    string Id { get; }
    string Name { get; set; }
    ClientSearchEntity Data { get; set; }
}