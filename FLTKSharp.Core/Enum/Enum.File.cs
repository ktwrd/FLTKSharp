namespace FLTKSharp.Core;

[Flags]
public enum FltkFileChooserType : int
{
    Single = 0,
    Multi = 1,
    Create = 2,
    Directory = 4,
}
[Flags]
public enum FltkFileDialogOptions
{
    NoOptions = 0,
    SaveAsConfirm = 1,
    NewFolder = 2,
    Preview = 4,
    UseFilterExtension = 8,
}
public enum FltkFileDialogType
{
    BrowseFile = 0,
    BrowseDir,
    BrowseMultiFile,
    BrowseMultiDir,
    BrowseSaveFile,
    BrowseSaveDir,
}
public enum FltkFileType
{
    Files = 0,
    Dirs,
}
