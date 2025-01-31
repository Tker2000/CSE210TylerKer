// Stretch: I added a feature that will tell you what journals have already been saved so that you know which ones you can load in when you're ready


using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Journal _journal = new Journal();
        
        while (true)
        {
            Console.WriteLine("\nJournal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string _choice = Console.ReadLine();

            switch (_choice)
            {
                case "1":
                    _journal.WriteEntry();
                    break;
                case "2":
                    _journal.DisplayJournal();
                    break;
                case "3":
                    Console.Write("Enter filename to save to: ");
                    string _saveFilename = Console.ReadLine();
                    _journal.SaveToFile(_saveFilename);
                    _journal.RefreshSavedJournals();
                    break;
                case "4":
                    if (_journal.ListSavedJournals())
                    {
                        Console.Write("Enter filename to load from: ");
                        string _loadFilename = Console.ReadLine();
                        _journal.LoadFromFile(_loadFilename);
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
    private List<Entry> _entries = new List<Entry>();
    private List<string> _prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    private HashSet<string> _savedJournals = new HashSet<string>();

    public void WriteEntry()
    {
        Random random = new Random();
        string _prompt = _prompts[random.Next(_prompts.Count)];

        Console.WriteLine($"\nPrompt: {_prompt}");
        Console.Write("Your response: ");
        string _response = Console.ReadLine();

        Entry _newEntry = new Entry(_prompt, _response, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        _entries.Add(_newEntry);
        Console.WriteLine("Entry saved.");
    }

    public void DisplayJournal()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("\nThe journal is empty.");
            return;
        }

        Console.WriteLine("\nJournal Entries:");
        foreach (Entry _entry in _entries)
        {
            Console.WriteLine($"\nDate: {_entry._Date}");
            Console.WriteLine($"Prompt: {_entry._Prompt}");
            Console.WriteLine($"Response: {_entry._Response}");
        }
    }

    public void SaveToFile(string _filename)
    {
        try
        {
            string _fullfilename = _filename + ".txt";
            using (StreamWriter _writer = new StreamWriter(_fullfilename))
            {
                foreach (Entry _entry in _entries)
                {
                    _writer.WriteLine($"{_entry._Date}|{_entry._Prompt}|{_entry._Response}");
                }
            }
            Console.WriteLine("Journal saved successfully.");
            _savedJournals.Add(_filename);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while saving: {ex.Message}");
        }
    }

    public void LoadFromFile(string _filename)
    {
        try
        {
            if (!File.Exists(_filename))
            {
                Console.WriteLine("File not found.");
                return;
            }

            _entries.Clear();
            string[] _lines = File.ReadAllLines(_filename);
            foreach (string _line in _lines)
            {
                string[] _parts = _line.Split('|');
                if (_parts.Length == 3)
                {
                    _entries.Add(new Entry(_parts[1], _parts[2], _parts[0]));
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
        if (_savedJournals.Count == 0)
        {
            Console.WriteLine("No saved journals found.");
            return false;
        }

        foreach (string _journal in _savedJournals)
        {
            Console.WriteLine(_journal);
        }
        return true;
    }

    public void RefreshSavedJournals()
    {
        try
        {
            string[] _files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.txt");
            _savedJournals.Clear();
            foreach (string _file in _files)
            {
                _savedJournals.Add(Path.GetFileName(_file));
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
    public string _Prompt { get; }
    public string _Response { get; }
    public string _Date { get; }

    public Entry(string _prompt, string _response, string _date)
    {
        _Prompt = _prompt;
        _Response = _response;
        _Date = _date;
    }
}
