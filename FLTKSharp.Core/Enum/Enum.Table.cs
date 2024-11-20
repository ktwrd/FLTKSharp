namespace FLTKSharp.Core;

public enum FltkTableContext
{
    None = 0,
    StartPage = 0x01,
    EndPage = 0x02,
    RowHeader = 0x04,
    ColHeader = 0x08,
    Cell = 0x10,
    Table = 0x20,
    RcResize = 0x40,
}
public enum FltkTableResizeFlag
{
    None = 0,
    ColumnLeft = 1,
    ColumnRight = 2,
    RowAbove = 3,
    RowBelow = 4
}
