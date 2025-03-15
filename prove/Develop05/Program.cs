// I added a few things to make this more fun and usable... 
// Save and Load Goals: Implemented file persistence using SaveGoals() and LoadGoals(). Goals and scores are stored in goals.txt and can be retrieved later.
// Leveling System:Players level up after reaching certain score thresholds. Each level-up occurs every 500 points.
// Negative Goals: Users can create goals that subtract points when completed. Useful for tracking and discouraging bad habits.



using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    private static List<Goal> _goals = new List<Goal>();
    private static int _score = 0;
    private const string SaveFile = "goals.txt";
    private static int _level = 1;
    private static int _nextLevelThreshold = 500;

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Score: {_score} | Level: {_level} (Next Level: {_nextLevelThreshold})");
            DisplayMenu();
            Console.Write("Choose an option: ");
            
            switch (Console.ReadLine())
            {
                case "1": CreateGoal(); break;
                case "2": RecordGoal(); break;
                case "3": ShowGoals(); break;
                case "4": SaveGoals(); break;
                case "5": LoadGoals(); break;
                case "6": return;
            }
        }
    }

    static void DisplayMenu()
    {
        Console.WriteLine("1. Create New Goal");
        Console.WriteLine("2. Record Goal Event");
        Console.WriteLine("3. Show Goals");
        Console.WriteLine("4. Save Goals");
        Console.WriteLine("5. Load Goals");
        Console.WriteLine("6. Exit");
    }

    static void CreateGoal()
    {
        Console.Write("Enter Goal Name: ");
        string name = Console.ReadLine();
        Console.Write("Enter Description: ");
        string description = Console.ReadLine();
        Console.Write("Enter Points: ");
        int points = int.Parse(Console.ReadLine());
        
        Console.WriteLine("1. Simple Goal\n2. Eternal Goal\n3. Checklist Goal\n4. Negative Goal");
        switch (Console.ReadLine())
        {
            case "1": _goals.Add(new SimpleGoal(name, description, points)); break;
            case "2": _goals.Add(new EternalGoal(name, description, points)); break;
            case "3":
                Console.Write("Enter Target Count: ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("Enter Bonus Points: ");
                int bonus = int.Parse(Console.ReadLine());
                _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                break;
            case "4":
                Console.Write("Enter Penalty Points: ");
                int penalty = int.Parse(Console.ReadLine());
                _goals.Add(new NegativeGoal(name, description, penalty));
                break;
        }
    }

    static void RecordGoal()
    {
        ShowGoals();
        Console.Write("Select a goal to record: ");
        int index = int.Parse(Console.ReadLine()) - 1;
        if (index >= 0 && index < _goals.Count)
        {
            _score += _goals[index].RecordEvent();
            CheckLevelUp();
        }
    }

    static void ShowGoals()
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetStatus()} {_goals[i]._name}");
        }
        Console.ReadLine();
    }

    static void SaveGoals()
    {
        using (StreamWriter writer = new StreamWriter(SaveFile))
        {
            writer.WriteLine(_score);
            foreach (Goal goal in _goals)
            {
                writer.WriteLine(goal.GetType().Name + "," + goal._name + "," + goal._description + "," + goal._points);
            }
        }
    }

    static void LoadGoals()
    {
        if (!File.Exists(SaveFile)) return;
        
        _goals.Clear();
        using (StreamReader reader = new StreamReader(SaveFile))
        {
            _score = int.Parse(reader.ReadLine());
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                string type = parts[0];
                string name = parts[1];
                string description = parts[2];
                int points = int.Parse(parts[3]);
                
                switch (type)
                {
                    case "SimpleGoal": _goals.Add(new SimpleGoal(name, description, points)); break;
                    case "EternalGoal": _goals.Add(new EternalGoal(name, description, points)); break;
                    case "ChecklistGoal":
                        int target = int.Parse(parts[4]);
                        int bonus = int.Parse(parts[5]);
                        _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                        break;
                    case "NegativeGoal":
                        int penalty = int.Parse(parts[4]);
                        _goals.Add(new NegativeGoal(name, description, penalty));
                        break;
                }
            }
        }
    }

    static void CheckLevelUp()
    {
        if (_score >= _nextLevelThreshold)
        {
            _level++;
            _nextLevelThreshold += 500;
            Console.WriteLine($"Congratulations! You've leveled up to Level {_level}!");
            Console.ReadLine();
        }
    }
}
