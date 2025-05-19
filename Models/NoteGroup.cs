namespace TimeIsMoney.Models;

public class NoteGroup : List<Note>
{
    public string Date
    {
        get; set;
    }

    public NoteGroup(string date, List<Note> note) : base(note)
    {
        Date = date;
    }
}