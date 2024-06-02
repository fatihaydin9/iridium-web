using Iridium.Core.Types;

namespace Iridium.Core.Models;

public class FormComponentModel
{
    public string DisplayName { get; set; }

    public FormInputType InputType { get; set; }

    public bool IsVisible { get; set; }

    public bool IsFilterable { get; set; }

    public bool IsEditable { get; set; }

    public int ColumnGrid { get; set; }

    public string MaskRegex { get; set; }

    public string CascadeField { get; set; }

}