namespace Cookie.Model;

public class MenuLabel
{
    public readonly string LabelName;
    public readonly char Ascii;

    public MenuLabel(string labelName, char ascii)
    {
        LabelName = labelName;
        Ascii = ascii;
    }
}