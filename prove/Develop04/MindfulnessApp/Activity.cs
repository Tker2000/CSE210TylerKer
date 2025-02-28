using System;
using System.Threading;

abstract class Activity
{
    protected string _name;
    protected string _description;
    protected int _duration;

    public void StartActivity()
    {
        Console.Clear();
        Console.WriteLine($"Starting {_name}...");
        Console.WriteLine(_description);
        Console.Write("Enter duration (seconds): ");
        _duration = int.Parse(Console.ReadLine() ?? "30");
        Console.WriteLine("Prepare to begin...");
        ShowAnimation(3);
        PerformActivity();
        EndActivity();
    }

    protected void ShowAnimation(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write("\rStarting in: " + i + "   ");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }

    protected void EndActivity()
    {
        Console.WriteLine($"Good job! You completed {_name} for {_duration} seconds.");
        ShowAnimation(3);
    }

    protected abstract void PerformActivity();
}
