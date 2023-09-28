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

public class ClientSearchEntity
{
    public string SearchAccounts { get; set; }
    public string SearchPersonal { get; set; }
    public string SearchManager { get; set; }
    public List<string> SearchStatuses { get; set; }
    public string SearchComments { get; set; }
    public string SearchHistory { get; set; }

    public List<string> SearchOrder { get; set; } = new();


    public ClientSearchEntity()
    {

    }

    public ClientSearchEntity(Dictionary<Type, dynamic> data)
    {
        foreach (var (type, item) in data)
        {
            if (type == typeof(List<SearchStatus>))
            {
                var statuses = (List<SearchStatus>)item;
                var statusInfo = GetType().GetProperty("SearchStatuses")!; ;
                statusInfo.SetValue(this, statuses.Select(x=>x.ToString()).ToList());
                SearchOrder.Add("SearchStatuses");
                continue;
            }
            var info = GetType().GetProperty(type.Name)!;
            info.SetValue(this, item.ToString());
            SearchOrder.Add(type.Name);
        }
    }

    public Dictionary<Type, dynamic> GetAll()
    {
        var ordered = new Dictionary<Type, dynamic>();
        foreach (var searchModelName in SearchOrder)
        {
            var type = _searchTypes[searchModelName];
            var info = GetType().GetProperty(searchModelName)!;
            
            var value = info.GetValue(this);
            if (type == typeof(List<SearchStatus>))
            {
                var list = ((List<string>)value!).Select(x=>JsonConvert.DeserializeObject<SearchStatus>(x)!).ToList();
                ordered.Add(type, list);
                continue;
            }

            
            ordered.Add(type, JsonConvert.DeserializeObject(value!.ToString()!, type)!);
        }

        return ordered;
    }

    private readonly Dictionary<string, Type> _searchTypes = new()
    {
        {"SearchAccounts", typeof(SearchAccounts)},
        {"SearchPersonal", typeof(SearchPersonal)},
        {"SearchManager", typeof(SearchManager)},
        {"SearchStatuses", typeof(List<SearchStatus>)},
        {"SearchComments", typeof(SearchComments)},
        {"SearchHistory", typeof(SearchHistory)},
    };

}

