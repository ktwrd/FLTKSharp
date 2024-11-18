namespace FLTKSharp.Core;
public enum FltkAlign
{
    Center = 0x0000,
    Top = 0x0001,
    Bottom = 0x0002,
    Left = 0x0004,
    Right = 0x0008,
    Inside = 0x0010,
    TextOverImage = 0x0020,
    ImageOverText = 0x000,
    NoWrap = 0x000,
    Clip = 0x0040,
    Wrap = 0x0080,
    ImageNextToText = 0x0100,
    TextNextToImage = 0x0120,
    ImageBackdrop = 0x0200,
    TopLeft = 0x0001 | 0x0004,
    TopRight = 0x0001 | 0x0008,
    BottomLeft = 0x0002 | 0x0004,
    BottomRight = 0x0002 | 0x0008,
    LeftTop = 0x0007,
    RightTop = 0x000B,
    LeftBottom = 0x000D,
    RightBottom = 0x000E,
    PositionMask = 0x000F,
    ImageMask = 0x0320,
}
public enum FltkBeepType
{
    Default = 0,
    Message,
    Error,
    Question,
    Password,
    Notification,
}
public enum FltkBoxType
{
    NoBox = 0,
    FlatBox,
    UpBox,
    DownBox,
    UpFrame,
    DownFrame,
    ThinUpBox,
    ThinDownBox,
    ThinUpFrame,
    ThinDownFrame,
    EngraveBox,
    EmbossedBox,
    EngravedFrame,
    EmbossedFrame,
    BorderBox,
    ShadowBox,
    BorderFrame,
    ShadowFrame,
    RoundedBox,
    RShadowBox,
    RoundedFrame,
    RFlatBox,
    RoundUpBox,
    RoundDownBox,
    DiamondUpBox,
    DiamondDownBox,
    OvalBox,
    OShadowBox,
    OvalFrame,
    OFlatBox,
    PlasticUpBox,
    PlasticDownBox,
    PlasticUpFrame,
    PlasticDownFrame,
    PlasticThinUpBox,
    PlasticThinDownBox,
    PlasticRoundUpBox,
    PlasticRoundDownBox,
    GtkUpBox,
    GtkDownBox,
    GtkUpFrame,
    GtkDownFrame,
    GtkThinUpBox,
    GtkThinDownBox,
    GtkThinUpFrame,
    GtkThinDownFrame,
    GtkRoundUpFrame,
    GtkRoundDownFrame,
    GleamUpBox,
    GleamDownBox,
    GleamUpFrame,
    GleamDownFrame,
    GleamThinUpBox,
    GleamThinDownBox,
    GleamRoundUpBox,
    GleamRoundDownBox,
    FreeBoxType,
}
public enum FltkBrowserScrollbarKind
{
    None = 0,
    Horizontal = 1,
    Vertical = 2,
    Both = 3,
    AlwaysOn = 4,
    HorizontalAlways = 5,
    VerticalAlways = 6,
    BothAlways = 7,
}
public enum FltkBrowserType
{
    Normal = 0,
    Select = 1,
    Hold = 2,
    Multi = 3,
}
public enum FltkButtonType
{
    Normal = 0,
    Toggle = 1,
    Radio = 102,
    Hidden = 3,
}
public enum FltkCallbackTrigger
{
    Never = 0,
    Changed = 1,
    NotChanged = 2,
    Release = 4,
    ReleaseAlways = 6,
    EnterKey = 8,
    EnterKeyAlways = 10,
    EnterKeyChanged = 11,
}
public enum FltkChartType
{
    Bar = 0,
    HorizontalBar = 1,
    Line = 2,
    Fill = 3,
    Spike = 4,
    Pie = 5,
    SpecialPie = 6,
}
public enum FltkClockType
{
    Square = 0,
    Round = 1,
}
public enum FltkCounterType
{
    Normal = 0,
    Simple = 1,
}
public enum FltkCursorKind
{
    Default = 0,
    Arrow = 35,
    Cross = 66,
    Wait = 76,
    Insert = 77,
    Hand = 31,
    Help = 47,
    Move = 27,

    NorthSouth = 78,
    WestEast = 79,
    NorthWestSouthEast = 80,
    NorthEastSouthWest = 81,
    North = 70,
    NorthEast = 69,
    East = 49,
    SouthEast = 8,
    South = 9,
    SouthWest = 7,
    West = 36,
    NorthWest = 68,

    None = 255
}
public enum FltkDamageType
{
    /// <summary>
    /// A child needs to be redrawn.
    /// </summary>
    Child = 0x01,
    /// <summary>
    /// The window was exposed.
    /// </summary>
    Expose = 0x02,
    /// <summary>
    /// The Fl_Scroll widget was scrolled.
    /// </summary>
    Scroll = 0x04,
    /// <summary>
    /// The overlay planes need to be redrawn.
    /// </summary>
    Overlay = 0x08,
    /// <summary>
    /// First user-defined damage bit.
    /// </summary>
    User1 = 0x10,
    /// <summary>
    /// Second user-defined damage bit.
    /// </summary>
    User2 = 0x20,
    /// <summary>
    /// Everything needs to be redrawn.
    /// </summary>
    All = 0x80
}
public enum FltkDialType
{
    Normal = 0,
    Line = 1,
    Fill = 2,
}
public enum FltkDragType
{
    None = -2,
    StartDnd = -1,
    Char = 0,
    Word = 1,
    Line = 2,
}
public enum FltkEvent : int
{
    Unknown = -1,
    None = 0,
    Push,
    Released,
    Enter,
    Leave,
    Drag,
    Focus,
    Unfocus,
    KeyDown,
    KeyUp,
    Close,
    Move,
    Shortcut,
    Deactivate,
    Activate,
    Hide,
    Show,
    Paste,
    SelectionClear,
    MouseWheel,
    DndEnter,
    DndDrag,
    DndLeave,
    DndRelease,
    ScreenConfigChanged,
    Fullscreen,
    ZoomGesture,
    Resize
}
public enum FltkFlexType
{
    Row = 0,
    Column,
}
public enum FltkFont
{
    Helvetica = 0,
    HelveticaBold = 1,
    HelveticaItalic = 2,
    HelveticaBoldItalic = 3,
    Courier = 4,
    CourierBold = 5,
    CourierItalic = 6,
    CourierBoldItalic = 7,
    Times = 8,
    TimesBold = 9,
    TimesItalic = 10,
    TimesBoldItalic = 11,
    Symbol = 12,
    Screen = 13,
    ScreenBold = 14,
    Zapfdingbats = 15,
}
public enum FltkInputType : int
{
    Normal = 0,
    Float = 1,
    Int = 2,
    Multiline = 4,
    Secret = 5,
    Input = 7,
    Readonly = 8,
    Wrap = 16
}
public enum FltkKeyType
{
    Fl_Key_None = 0,
    Button = 0xfee8,
    BackSpace = 0xff08,
    Tab = 0xff09,
    IsoKey = 0xff0c,
    Enter = 0xff0d,
    Pause = 0xff13,
    ScrollLock = 0xff14,
    Escape = 0xff1b,
    Kana = 0xff2e,
    Eisu = 0xff2f,
    Yen = 0xff30,
    JISUnderscore = 0xff31,
    Home = 0xff50,
    Left = 0xff51,
    Up = 0xff52,
    Right = 0xff53,
    Down = 0xff54,
    PageUp = 0xff55,
    PageDown = 0xff56,
    End = 0xff57,
    Print = 0xff61,
    Insert = 0xff63,
    Menu = 0xff67,
    Help = 0xff68,
    NumLock = 0xff7f,
    KP = 0xff80,
    KPEnter = 0xff8d,
    KPLast = 0xffbd,
    F1 = 0xffbd + 1,
    F2 = 0xffbd + 2,
    F3 = 0xffbd + 3,
    F4 = 0xffbd + 4,
    F5 = 0xffbd + 5,
    F6 = 0xffbd + 6,
    F7 = 0xffbd + 7,
    F8 = 0xffbd + 8,
    F9 = 0xffbd + 9,
    F10 = 0xffbd + 10,
    F11 = 0xffbd + 11,
    F12 = 0xffbd + 12,
    FLast = 0xffe0,
    ShiftL = 0xffe1,
    ShiftR = 0xffe2,
    ControlL = 0xffe3,
    ControlR = 0xffe4,
    CapsLock = 0xffe5,
    MetaL = 0xffe7,
    MetaR = 0xffe8,
    AltL = 0xffe9,
    AltR = 0xffea,
    Delete = 0xffff,
}
public enum FltkLabelType : int
{
    Normal = 0,
    None,
    Shadow,
    Engraved,
    Embossed,
    Multi,
    Icon,
    Image,
    FreeType,
}
public enum FltkLineStyle
{
    Solid = 0,
    Dash,
    Dot,
    DashDot,
    DashDotDot,
    CapFlat = 100,
    CapRound = 200,
    CapSquare = 300,
    JoinMiter = 1000,
    JoinRound = 2000,
    JoinBevel = 3000,
}
public enum FltkMenuButtonType
{
    Popup1 = 1,
    Popup2,
    Popup12,
    Popup3,
    Popup13,
    Popup23,
    Popup123,
}
[Flags]
public enum FltkMenuFlag : int
{
    Normal = 0,
    Inactive = 1,
    Toggle = 2,
    Value = 4,
    Radio = 8,
    Invisible = 0x10,
    SubmenuPointer = 0x20,
    Submenu = 0x40,
    MenuDivider = 0x80,
    MenuHorizontal = 0x100
}
[Flags]
public enum FltkModeKind
{
    Rgb = 0,
    Index = 1,
    Double = 2,
    Accum = 4,
    Alpha = 8,
    Depth = 16,
    Stencil = 32,
    Rgb8 = 64,
    MultiSample = 128,
    Stero = 256,
    FakeSingle = 512,
    OpenGl3 = 1024
}
public enum FltkOutputType
{
    Normal = 8,
    Multiline = 12,
}
public enum FltkPackType : int
{
    Vertical = 0,
    Horizontal = 1
}
public enum FltkRgbScaling
{
    Nearest = 0,
    Bilinear,
}
public enum FltkRowSelectMode
{
    None = 0,
    Single,
    Multi,
}
public enum FltkScrollbarType
{
    Vertical = 0,
    Horizontal = 1,
    VerticalFill = 2,
    HorizontalFill = 3,
    VerticalNice = 4,
    HorizontalNice = 5,
}
public enum FltkScrollType : int
{
    None = 0,
    Horizontal = 1,
    Vertical = 2,
    Both = 3,
    AlwaysOn = 4,
    HorizontalAlways = 5,
    VerticalAlways = 6,
    BothAlways = 7
}
public enum FltkShortcutKind
{
    None = 0,
    Shift = 0x00010000,
    CapsLock = 0x00020000,
    Ctrl = 0x00040000,
    Alt = 0x00080000,
    Meta = 0x00400000,
    Button1 = 0x01000000,
    Button2 = 0x02000000,
    Button3 = 0x04000000,
    Buttons = 0x7f000000,
}
public enum FltkSliderType
{
    Vertical = 0,
    Horizontal = 1,
    VerticalFill = 2,
    HorizontalFill = 3,
    VerticalNice = 4,
    HorizontalNice = 5,
}
public enum FltkTextCursorKind
{
    Normal = 0,
    Caret,
    Dim,
    Block,
    Heavy,
    Simple,
}
public enum FltkWindowType
{
    Normal = 240,
    Double = 241,
}
public enum FltkWrapMode
{
    None,
    AtColumn,
    AtPixel,
    AtBounds,
}