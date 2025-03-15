class NegativeGoal : Goal
{
    private int _penalty;

    public NegativeGoal(string name, string description, int penalty) : base(name, description, -penalty)
    {
        _penalty = penalty;
    }

    public override int RecordEvent() => -_penalty;

    public override string GetStatus() => "[-]";
}
