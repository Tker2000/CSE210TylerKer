// Stretch: I added a feature that will tell you what journals have already been saved so that you know which ones you can load in when you're ready


using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        
        while (true)
        {
            Console.WriteLine("\nJournal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    journal.WriteEntry();
                    break;
                case "2":
                    journal.DisplayJournal();
                    break;
                case "3":
                    Console.Write("Enter filename to save to: ");
                    string saveFilename = Console.ReadLine();
                    journal.SaveToFile(saveFilename);
                    journal.RefreshSavedJournals();
                    break;
                case "4":
                    if (journal.ListSavedJournals())
                    {
                        Console.Write("Enter filename to load from: ");
                        string loadFilename = Console.ReadLine();
                        journal.LoadFromFile(loadFilename);
                    }
                    break;
                case "5":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}

class Journal
{
    private List<Entry> entries = new List<Entry>();
    private List<string> prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    private HashSet<string> savedJournals = new HashSet<string>();

    public void WriteEntry()
    {
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Count)];

        Console.WriteLine($"\nPrompt: {prompt}");
        Console.Write("Your response: ");
        string response = Console.ReadLine();

        Entry newEntry = new Entry(prompt, response, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        entries.Add(newEntry);
        Console.WriteLine("Entry saved.");
    }

    public void DisplayJournal()
    {
        if (entries.Count == 0)
        {
            Console.WriteLine("\nThe journal is empty.");
            return;
        }

        Console.WriteLine("\nJournal Entries:");
        foreach (Entry entry in entries)
        {
            Console.WriteLine($"\nDate: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"Response: {entry.Response}");
        }
    }

    public void SaveToFile(string filename)
    {
        try
        {
            string fullfilename = filename + ".txt";
            using (StreamWriter writer = new StreamWriter(fullfilename))
            {
                foreach (Entry entry in entries)
                {
                    writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
                }
            }
            Console.WriteLine("Journal saved successfully.");
            savedJournals.Add(filename);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while saving: {ex.Message}");
        }
    }

    public void LoadFromFile(string filename)
    {
        try
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found.");
                return;
            }

            entries.Clear();
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 3)
                {
                    entries.Add(new Entry(parts[1], parts[2], parts[0]));
                }
            }

            Console.WriteLine("Journal loaded successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while loading: {ex.Message}");
        }
    }

    public bool ListSavedJournals()
    {
        Console.WriteLine("\nSaved Journals:");
        RefreshSavedJournals();
        if (savedJournals.Count == 0)
        {
            Console.WriteLine("No saved journals found.");
            return false;
        }

        foreach (string journal in savedJournals)
        {
            Console.WriteLine(journal);
        }
        return true;
    }

    public void RefreshSavedJournals()
    {
        try
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.txt");
            savedJournals.Clear();
            foreach (string file in files)
            {
                savedJournals.Add(Path.GetFileName(file));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while refreshing saved journals: {ex.Message}");
        }
    }
}

class Entry
{
    public string Prompt { get; }
    public string Response { get; }
    public string Date { get; }

    public Entry(string prompt, string response, string date)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
    }
}
