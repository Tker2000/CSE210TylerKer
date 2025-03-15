abstract class Goal
{
    public string _name { get; }
    public string _description { get; }
    public int _points { get; }

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public abstract int RecordEvent();
    public abstract string GetStatus();
}
