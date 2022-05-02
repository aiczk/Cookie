namespace Cookie.Model;

public class MenuItem
{
    public readonly string Label;
    public readonly char Ascii;

    public MenuItem(string label, char ascii)
    {
        Label = label;
        Ascii = ascii;
    }
}