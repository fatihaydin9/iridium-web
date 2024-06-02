using Iridium.Core.Types;

namespace Iridium.Core.Attributes.Base;

public class FormComponentAttribute : Attribute
{
    public string cascadeField = "NO_CASCADE";
    public int columnGrid = 12;
    public string displayName = "";
    public FormInputType inputType = FormInputType.InputText;
    public bool isEditable;
    public bool isFilterable = true;
    public bool isRequired = true;
    public bool isVisible = true;
    public string maskRegex = "NO_MASK";

    public FormComponentAttribute(string displayName, bool isRequired, FormInputType inputType,
        bool isVisible, bool isEditable, bool isFilterable, int columnGrid, string maskRegex, string cascadeField)
    {
        this.displayName = displayName;
        this.isRequired = isRequired;
        this.inputType = inputType;
        this.isVisible = isVisible;
        this.isEditable = isEditable;
        this.isFilterable = isFilterable;
        this.columnGrid = columnGrid;
        this.maskRegex = maskRegex;
        this.cascadeField = cascadeField;
    }
}