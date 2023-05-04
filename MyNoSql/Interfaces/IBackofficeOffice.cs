namespace DataServices.MyNoSql.Interfaces;

public interface IBackofficeOffice
{
    string Id { get; set;}
    string Name { get; set;}
    bool IsDisabled { get; set;}
    string BrandId { get; set;}
}