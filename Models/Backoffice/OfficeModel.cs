using DataServices.MyNoSql.Interfaces;

namespace DataServices.Models.Backoffice;

  
public class OfficeModel : IBackofficeOffice
{
    public string Id { get; set;}
    public string Name { get; set;}
    public bool IsDisabled { get; set;}
    public string BrandId { get; set;}
}
