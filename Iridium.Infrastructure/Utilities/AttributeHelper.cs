using Iridium.Core.Attributes.Base;
using Iridium.Core.Models;

namespace Iridium.Infrastructure.Utilities;

public static class AttributeHelper
{
    public static List<FormComponentModel> GetDtoFormComponents<T>()
    {
        var result = new List<KeyValuePair<string, FormComponentModel>>();
        var dtoType = typeof(T);
        var props = dtoType.GetProperties();

        foreach (var prop in props)
        {
            var attrsList = prop.GetCustomAttributes(true).ToList();
            foreach (var attr in attrsList)
                if (attr is FormComponentAttribute formComponentAttribute)
                {
                    var componentModel = new FormComponentModel
                    {
                        CascadeField = formComponentAttribute.cascadeField,
                        ColumnGrid = formComponentAttribute.columnGrid,
                        DisplayName = formComponentAttribute.displayName,
                        InputType = formComponentAttribute.inputType,
                        IsFilterable = formComponentAttribute.isFilterable,
                        IsVisible = formComponentAttribute.isVisible,
                        MaskRegex = formComponentAttribute.maskRegex,
                        IsEditable = formComponentAttribute.isEditable
                    };

                    var keyValueRepresentation = KeyValuePair.Create(prop.Name, componentModel);
                    result.Add(keyValueRepresentation);
                }
        }

        return result.Select(s => s.Value).ToList();
    }


    public static EndpointSettingsModel GetDtoEndPointSettings<T>()
    {
        var classType = typeof(T);
        var attribute =
            classType.GetCustomAttributes(typeof(EndpointSettingsAttribute), true).FirstOrDefault() as
                EndpointSettingsAttribute;

        var result = new EndpointSettingsModel();

        if (attribute != null)
            result = new EndpointSettingsModel
            {
                InsertEndPoint = attribute.insertEndPoint,
                DeleteEndPoint = attribute.deleteEndPoint,
                DropdownEndPoint = attribute.dropdownEndPoint,
                GetEndPoint = attribute.getEndPoint,
                GetListEndPoint = attribute.getListEndPoint,
                GetPaginatedListEndPoint = attribute.paginatedListEndPoint,
                TemplateEndPoint = attribute.templateEndpoint,
                UpdateEndPoint = attribute.updateEndPoint
            };

        return result;
    }
}