using FLTKSharp.Core;

namespace FLTKSharp.Example;

public class HelloWorldExample
{
    private readonly FLWindow _window;
    private readonly FLButton _label;

    public HelloWorldExample()
    {
        _window = new(340, 180, "hello");
        _label = new FLButton(20, 40, 300, 100, "Hello, world!");
         _label.PressBoxType = FltkBoxType.UpBox;
        _label.LabelSize = 36;
        _label.LabelFont = 1 | 2; // HELVETICA_BOLD_ITALIC
        _label.LabelType = FltkLabelType.Shadow;
        _window.End();
    }

    public void Show()
    {
        _window.Show();
    }

    public void Hide()
    {
        _window.Hide();
    }
}