using FLTKSharp.Core;
using System.Drawing;
using NLog;
using System.Runtime.InteropServices;
using FLTKSharp.Example;
using static FLTKSharp.Core.CFltkNative;

var log = LogManager.GetLogger("Example");


FLTK.Initialize();
new HelloWorldExample().Show();
FLTK.Run();

/*FLTK.Initialize();
FLTK.ThreadLock();
var window = new FLWindow(0, 0, 320, 240, "Example Window");
window.Begin();
var btn = new FLButton(3, 3, 73, 23, "Hello!");
window.AddChild(btn);
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
log.Info("Displaying Window");
window.Show();
FLTK.Run();*/