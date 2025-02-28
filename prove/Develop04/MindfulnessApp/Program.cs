using System;

class Program
{
    private static Activity _activity;
    static void Main()
    {
        while (true)
        {
            Menu.DisplayMenu();
            int choice = int.Parse(Console.ReadLine() ?? "4");
            _activity = Menu.SelectActivity(choice);
            if (_activity == null) return;
            _activity.StartActivity();
        }
    }
}
