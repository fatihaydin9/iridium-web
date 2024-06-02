using Iridium.Core.Types;

namespace Iridium.Core.Attributes.Base;

public class FormComponentAttribute : Attribute
{
    public string displayName = "";
    public bool isRequired = true;
    public FormInputType inputType = FormInputType.InputText;
    public bool isVisible = true;
    public bool isEditable = false;
    public bool isFilterable = true;
    public int columnGrid = 12;
    public string maskRegex = "NO_MASK";
    public string cascadeField = "NO_CASCADE";

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