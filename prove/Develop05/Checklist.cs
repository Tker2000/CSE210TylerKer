class ChecklistGoal : Goal
{
    private int _target;
    private int _count;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int target, int bonus) : base(name, description, points)
    {
        _target = target;
        _count = 0;
        _bonus = bonus;
    }

    public override int RecordEvent()
    {
        _count++;
        return _count == _target ? _points + _bonus : _points;
    }

    public override string GetStatus() => _count >= _target ? "[X]" : $"[{_count}/{_target}]";
}
