class BreathingActivity : Activity
{
    public BreathingActivity()
    {
        _name = "Breathing Exercise";
        _description = "This activity helps you relax by guiding your breathing in and out slowly.";
    }

    protected override void PerformActivity()
    {
        int elapsed = 0;
        while (elapsed < _duration)
        {
            Console.WriteLine("Breathe in...");
            ShowAnimation(3);
            elapsed += 3;

            Console.WriteLine("Breathe out...");
            ShowAnimation(3);
            elapsed += 3;
        }
    }
}
