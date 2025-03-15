class SimpleGoal : Goal
{
    private bool _completed;

    public SimpleGoal(string name, string description, int points) : base(name, description, points)
    {
        _completed = false;
    }

    public override int RecordEvent()
    {
        if (!_completed)
        {
            _completed = true;
            return _points;
        }
        return 0;
    }

    public override string GetStatus() => _completed ? "[X]" : "[ ]";
}
