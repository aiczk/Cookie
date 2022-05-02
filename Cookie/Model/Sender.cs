namespace Cookie.Model;

public class Sender
{
    public string FirstName, FamilyName, Genre;
    public int MarkIndex;

    public Sender(string firstName, string familyName, string genre, int markIndex)
    {
        FirstName = firstName;
        FamilyName = familyName;
        Genre = genre;
        MarkIndex = markIndex;
    }
}