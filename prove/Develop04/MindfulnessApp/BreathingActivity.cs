// Stretch: I added animation to the breathing activity using a sinusoidal function to expand and contract text smoothly (fast at first, then slows down).



using System;
using System.Threading;

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
            Console.Clear();
            Console.WriteLine("Breathe in...");
            BreathingAnimation(true);  // Simulate inhale
            elapsed += 3;

            Console.Clear();
            Console.WriteLine("Breathe out...");
            BreathingAnimation(false); // Simulate exhale
            elapsed += 3;
        }
    }

    private void BreathingAnimation(bool inhale)
    {
        int steps = 10; // Number of animation frames
        int maxSize = 20; // Maximum expansion size
        int minSize = 3;  // Minimum contraction size

        for (int i = 0; i < steps; i++)
        {
            int size = inhale 
                ? minSize + (int)((maxSize - minSize) * (Math.Sin((Math.PI / 2) * (i / (double)steps))))
                : maxSize - (int)((maxSize - minSize) * (Math.Sin((Math.PI / 2) * (i / (double)steps))));

            Console.Clear();
            Console.WriteLine(new string('*', size)); // Expanding/contracting effect
            Thread.Sleep(200);
        }
    }
}
