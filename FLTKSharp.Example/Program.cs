using FLTKSharp.Core;
using System.Drawing;

FLTK.Initialize();
FLTK.ThreadLock();
var window = new FLWindow(320, 240, "Example Window");
var btn = new FLButton(3, 3, 73, 23, "Hello!");
btn.MouseDown += (sender, data) =>
{
    Console.WriteLine(string.Join(Environment.NewLine, new[]
    {
        "".PadRight(16, '-') + " Mouse Down",
        $"{nameof(data.X)}: {data.X}",
        $"{nameof(data.Y)}: {data.Y}",
        $"{nameof(data.Button)}: {(int)data.Button} ({data.Button})",
        $"{nameof(data.EventState)}: {(int)data.EventState} ({data.EventState})"
    }));
};
btn.MouseUp += (sender, data) =>
{
    Console.WriteLine(string.Join(Environment.NewLine, new[]
    {
        "".PadRight(16, '-') + " Mouse Up",
        $"{nameof(data.X)}: {data.X}",
        $"{nameof(data.Y)}: {data.Y}",
        $"{nameof(data.Button)}: {(int)data.Button} ({data.Button})",
        $"{nameof(data.EventState)}: {(int)data.EventState} ({data.EventState})"
    }));
};
btn.SetColor(Color.LightSkyBlue);
window.End();
window.Show();
FLTK.Run();