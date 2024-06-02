namespace Iridium.Core.Attributes.Base;

public class EndpointSettingsAttribute : Attribute
{
    public string templateEndpoint;
    public string paginatedListEndPoint;
    public string getListEndPoint;
    public string getEndPoint;
    public string insertEndPoint;
    public string updateEndPoint;
    public string deleteEndPoint;
    public string dropdownEndPoint;
    public EndpointSettingsAttribute(string templateEndpoint, string paginatedListEndpoint, string getListEndPoint, string getEndPoint,
        string insertEndPoint, string updateEndPoint, string deleteEndPoint, string dropdownEndPoint, string paginatedListEndPoint)
    {
        this.templateEndpoint = templateEndpoint;
        this.paginatedListEndPoint= paginatedListEndPoint;
        this.getListEndPoint = getListEndPoint;
        this.getEndPoint = getEndPoint;
        this.insertEndPoint = insertEndPoint;
        this.updateEndPoint = updateEndPoint;
        this.deleteEndPoint = deleteEndPoint;
        this.dropdownEndPoint = dropdownEndPoint;
        this.paginatedListEndPoint = paginatedListEndPoint;
    }
}