class Menu
{
    public static void DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine("Mindfulness App");
        Console.WriteLine("1. Breathing Exercise");
        Console.WriteLine("2. Reflection Exercise");
        Console.WriteLine("3. Listing Exercise");
        Console.WriteLine("4. Exit");
        Console.Write("Choose an option: ");
    }

    public static Activity SelectActivity(int choice)
    {
        return choice switch
        {
            1 => new BreathingActivity(),
            2 => new ReflectionActivity(),
            3 => new ListingActivity(),
            _ => null
        };
    }
}