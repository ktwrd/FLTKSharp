using FLTKSharp.Core;

FLTK.Initialize();
FLTK.ThreadLock();
var window = new FLWindow(320, 240, "Example Window");
var btn = new FLButton(3, 3, 73, 23, "Hello!");
btn.Handle((eventId) =>
{
    if (eventId == 1)
    {
        Console.WriteLine($"[btn] clicked!");
        return true;
    }
    return false;
});
btn.SetColor(255, 255, 255);
window.End();
window.Show();
FLTK.Run();