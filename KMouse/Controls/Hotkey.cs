using System.Text;
using System.Windows.Input;

namespace KMouse.Controls;

public class Hotkey
{
    public Key Key { get; }

    public ModifierKeys Modifiers { get; }

    public Hotkey(Key key, ModifierKeys modifiers)
    {
        Key = key;
        Modifiers = modifiers;
    }

    public override string ToString()
    {
        var str = new StringBuilder();

        if (Modifiers.HasFlag(ModifierKeys.Control))
        {
            if (str.Length > 0)
            {
                str.Append(" + ");
            }
            str.Append(" Ctrl ");
        }

        if (Modifiers.HasFlag(ModifierKeys.Shift))
        {
            if (str.Length > 0)
            {
                str.Append(" + ");
            }
            str.Append(" Shift ");
        }

        if (Modifiers.HasFlag(ModifierKeys.Alt))
        {
            if (str.Length > 0)
            {
                str.Append(" + ");
            }
            str.Append(" Alt ");
        }

        if (Modifiers.HasFlag(ModifierKeys.Windows))
        {
            if (str.Length > 0)
            {
                str.Append(" + ");
            }
            str.Append(" Win ");
        }

        if (Key != Key.LeftCtrl &&
            Key != Key.RightCtrl &&
            Key != Key.LeftAlt &&
            Key != Key.RightAlt &&
            Key != Key.LeftShift &&
            Key != Key.RightShift &&
            Key != Key.LWin &&
            Key != Key.RWin &&
            Key != Key.Clear &&
            Key != Key.OemClear &&
            Key != Key.Apps)
        {
            str.Append(Key);
        }

        return str.ToString();
    }
}