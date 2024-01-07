using Iridium.Infrastructure.Attributes.Base;
using Iridium.Infrastructure.Models;
using System.Reflection;

namespace Iridium.Infrastructure.Utilities;

public static class AttributeHelper
{
    public static List<FormComponentModel> GetDtoFormComponents<T>()
    {
        List<KeyValuePair<string, FormComponentModel>> result = new List<KeyValuePair<string, FormComponentModel>>();
        var dtoType = typeof(T);
        PropertyInfo[] props = dtoType.GetProperties();

        foreach (PropertyInfo prop in props)
        {
            List<object> attrsList = prop.GetCustomAttributes(true).ToList();
            foreach (object attr in attrsList)
            {
                FormComponentAttribute formComponentAttribute = attr as FormComponentAttribute;

                if (formComponentAttribute != null)
                {
                    var componentModel = new FormComponentModel()
                    {
                        CascadeField = formComponentAttribute.cascadeField,
                        ColumnGrid = formComponentAttribute.columnGrid,
                        DisplayName = formComponentAttribute.displayName,
                        InputType = formComponentAttribute.inputType,
                        IsFilterable = formComponentAttribute.isFilterable,
                        IsVisible = formComponentAttribute.isVisible,   
                        MaskRegex = formComponentAttribute.maskRegex,
                        IsEditable = formComponentAttribute.isEditable,
                    };

                    var keyValueRepresentation = KeyValuePair.Create(prop.Name, componentModel);
                    result.Add(keyValueRepresentation);
                }
            }
        }

        return result.Select(s => s.Value).ToList();
    }


    public static EndpointSettingsModel GetDtoEndPointSettings<T>()
    {
        var classType = typeof(T);
        var attribute = classType.GetCustomAttributes(typeof(EndpointSettingsAttribute), true).FirstOrDefault() as EndpointSettingsAttribute;

        var result = new EndpointSettingsModel();

        if (attribute != null)
        {
            result = new EndpointSettingsModel()
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
        }

        return result;
    }

}