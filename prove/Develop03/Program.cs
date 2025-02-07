using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Scripture scripture = new Scripture("Proverbs 3:5-6", "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight.");
        
        while (!scripture.AllWordsHidden())
        {
            Console.Clear();
            scripture.Display();
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");
            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
                break;
            
            scripture.HideRandomWords(3);
        
        }
    }
}

class Scripture
{
    private Reference reference;
    private List<Word> words;
    
    public Scripture(string referenceText, string text)
    {
        reference = new Reference(referenceText);
        words = text.Split(' ').Select(w => new Word(w)).ToList();
    }
    
    public void Display()
    {
        Console.Write(reference.Text + " - ");
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
    
    public Reference(string text)
    {
        Text = text;
    }
}

class Word
{
    public string Text { get; }
    public bool Hidden { get; private set; }
    
    public Word(string text)
    {
        Text = text;
        Hidden = false;
    }
    
    public void Hide()
    {
        Hidden = true;
    }
}
