using AccountsManager;
using CommentFlows;
using ManagerAccessFlows;
using Newtonsoft.Json;
using Pd;
using ReportGrpc;
using StatusFlows;
// ReSharper disable UnusedMember.Global
#pragma warning disable CS8618

namespace DataServices.Models.Clients.Search;

public class ClientSearchModel
{
    public string SearchAccounts { get; set; }
    public string SearchPersonal { get; set; }
    public string SearchManager { get; set; }
    public string SearchStatus { get; set; }
    public string SearchComments { get; set; }
    public string SearchHistory { get; set; }

    public List<string> SearchOrder { get; set; } = new();


    public ClientSearchModel()
    {

    }

    public ClientSearchModel(Dictionary<Type, dynamic> data)
    {
        foreach (var (type, item) in data)
        {
            var info = GetType().GetProperty(type.Name)!;
            info.SetValue(this, item.ToString());
            SearchOrder.Add(type.Name);
        }
    }

    public Dictionary<Type, dynamic> Get()
    {
        var ordered = new Dictionary<Type, dynamic>();
        foreach (var searchModelName in SearchOrder)
        {
            var type = _searchTypes[searchModelName];
            var info = GetType().GetProperty(searchModelName)!;
            var value = info.GetValue(this)!.ToString();
            ordered.Add(type, JsonConvert.DeserializeObject(value, type)!);
        }

        return ordered;
    }

    private readonly Dictionary<string, Type> _searchTypes = new()
    {
        {"SearchAccounts", typeof(SearchAccounts)},
        {"SearchPersonal", typeof(SearchPersonal)},
        {"SearchManager", typeof(SearchManager)},
        {"SearchStatus", typeof(SearchStatus)},
        {"SearchComments", typeof(SearchComments)},
        {"SearchHistory", typeof(SearchHistory)},
    };

}

