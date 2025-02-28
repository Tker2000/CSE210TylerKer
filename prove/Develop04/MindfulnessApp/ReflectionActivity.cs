using System;
using System.Collections.Generic;

class ReflectionActivity : Activity
{
    private static readonly List<string> _prompts = new()
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private static readonly List<string> _questions = new()
    {
        "Why was this experience meaningful to you?",
        "How did you feel when it was complete?",
        "What could you learn from this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity()
    {
        _name = "Reflection Exercise";
        _description = "This activity helps you reflect on times you showed strength and resilience.";
    }

    protected override void PerformActivity()
    {
        Random rnd = new();
        Console.WriteLine(GetRandomPrompt());
        ShowAnimation(5);
        int elapsed = 0;
        while (elapsed < _duration)
        {
            Console.WriteLine(GetRandomQuestion());
            ShowAnimation(5);
            elapsed += 5;
        }
    }

    private string GetRandomPrompt() => _prompts[new Random().Next(_prompts.Count)];
    private string GetRandomQuestion() => _questions[new Random().Next(_questions.Count)];
}
