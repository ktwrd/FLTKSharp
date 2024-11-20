namespace FLTKSharp.Core;

public enum FltkTreeConnectorStyle
{
    None = 0,
    Dotted = 1,
    Solid = 2,
}
public enum FltkTreeItemDrawMode
{
    Default = 0,
    LabelAndWidget = 1,
    HeightFromWidget = 2,
}
public enum FltkTreeItemSelectType
{
    Deselect = 0,
    Select = 1,
    Toggle = 2,
}
public enum FltkTreeReason
{
    None = 0,
    Selected,
    Deselected,
    Reselected,
    Opened,
    Closed,
    Dragged,
}
public enum FltkTreeReselectMode
{
    Once = 0,
    Always,
}
public enum FltkTreeSelectType
{
    None = 0,
    Single = 1,
    Multi = 2,
    SingleDraggable = 3,
}
public enum FltkTreeSortKind : int
{
    None = 0,
    Ascending = 1,
    Descending = 2
}
