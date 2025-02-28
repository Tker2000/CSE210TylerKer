using System;
using System.Collections.Generic;

class ListingActivity : Activity
{
    private static readonly List<string> _prompts = new()
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity()
    {
        _name = "Listing Exercise";
        _description = "This activity helps you reflect on the good things in life by listing them.";
    }

    protected override void PerformActivity()
    {
        Console.WriteLine(GetRandomPrompt());
        ShowAnimation(5);
        Console.WriteLine("Start listing items...");
        int count = 0;
        int elapsed = 0;
        while (elapsed < _duration)
        {
            Console.Write("Enter item: ");
            Console.ReadLine();
            count++;
            elapsed += 3;
        }
        Console.WriteLine($"You listed {count} items!");
    }

    private string GetRandomPrompt() => _prompts[new Random().Next(_prompts.Count)];
}
