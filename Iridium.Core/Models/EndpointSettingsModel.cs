namespace Iridium.Core.Models;

public class EndpointSettingsModel
{
    public string TemplateEndPoint { get; set; }

    public string GetListEndPoint { get; set; }

    public string GetPaginatedListEndPoint { get; set; }

    public string GetEndPoint { get; set; }

    public string InsertEndPoint { get; set; }

    public string UpdateEndPoint { get; set; }

    public string DeleteEndPoint { get; set; }

    public string DropdownEndPoint { get; set; }
}