// stretch: I added a few things. First, I added a way to add scriptures to a file, then allowed them to either keep practicing or try a new one. I also made sure that they could add a scripture at any time by typing "add".


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string _filename = "scriptures.txt";
        List<_scripture> scriptures = Load_scripturesFromFile(_filename);
        
        if (scriptures.Count == 0)
        {
            Console.WriteLine("No scriptures found in the file. You must add a scripture or quit.");
            while (true)
            {
                Console.WriteLine("Would you like to add a scripture? (yes/no)");
                string _choice = Console.ReadLine().ToLower();
                if (_choice == "no") return;
                if (_choice == "yes")
                {
                    Add_scriptureToFile(_filename);
                    scriptures = Load_scripturesFromFile(_filename);
                    break;
                }
            }
        }
        
        while (true)
        {
            Random rand = new Random();
            _scripture scripture = scriptures[rand.Next(scriptures.Count)];
            
            while (!scripture.AllWordsHidden())
            {
                Console.Clear();
                scripture.Display();
                Console.WriteLine("\nPress Enter to hide words, type 'add' to add a scripture, or type 'quit' to exit.");
                string _input = Console.ReadLine().ToLower();
                if (_input == "quit")
                    return;
                if (_input == "add")
                {
                    Add_scriptureToFile(_filename);
                    scriptures = Load_scripturesFromFile(_filename);
                    continue;
                }
                
                scripture.HideRandomWords(3);
            }
            
            Console.WriteLine("\nThe scripture is fully hidden. Would you like to try again with the same scripture or a new one? (same/new/quit)");
            string _choice = Console.ReadLine().ToLower();
            if (_choice == "quit") return;
        }
    }

    static List<_scripture> Load_scripturesFromFile(string _filename)
    {
        List<_scripture> scriptures = new List<_scripture>();
        if (File.Exists(_filename))
        {
            string[] lines = File.ReadAllLines(_filename);
            for (int i = 0; i < lines.Length; i += 2)
            {
                if (i + 1 < lines.Length)
                {
                    scriptures.Add(new _scripture(lines[i], lines[i + 1]));
                }
            }
        }
        return scriptures;
    }

    static void Add_scriptureToFile(string _filename)
    {
        Console.WriteLine("Enter the scripture _reference:");
        string _reference = Console.ReadLine();
        Console.WriteLine("Enter the scripture _text:");
        string _text = Console.ReadLine();
        
        using (StreamWriter sw = File.AppendText(_filename))
        {
            sw.WriteLine(_reference);
            sw.WriteLine(_text);
        }
    }
}

class _scripture
{
    private Reference _reference;
    private List<Word> words;
    
    public _scripture(string _referenceText, string _text)
    {
        _reference = new Reference(_referenceText);
        words = _text.Split(' ').Select(w => new Word(w)).ToList();
    }
    
    public void Display()
    {
        Console.Write(_reference.Text + " - ");
        foreach (var word in words)
        {
            Console.Write(word.Hidden ? "____ " : word.Text + " ");
        }
    }
    
    public void HideRandomWords(int count)
    {
        Random rand = new Random();
        var visibleWords = words.Where(w => !w.Hidden).ToList();
        if (visibleWords.Count == 0) return;

        for (int i = 0; i < count; i++)
        {
            if (visibleWords.Count == 0) break;
            var wordToHide = visibleWords[rand.Next(visibleWords.Count)];
            wordToHide.Hide();
            visibleWords.Remove(wordToHide);
        }
    }
    
    public bool AllWordsHidden()
    {
        return words.All(w => w.Hidden);
    }
}

class Reference
{
    public string Text { get; }
    
    public Reference(string _text)
    {
        Text = _text;
    }
}

class Word
{
    public string Text { get; }
    public bool Hidden { get; private set; }
    
    public Word(string _text)
    {
        Text = _text;
        Hidden = false;
    }
    
    public void Hide()
    {
        Hidden = true;
    }
}
