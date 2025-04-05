// UserInterface class
public class UserInterface
{
    public void DisplayMainMenu()
    {
        Console.WriteLine("1. Add Transaction\n2. View Summary\n3. Set Budget\n4. Exit");
    }

    public Transaction PromptTransactionEntry()
    {
        Console.Write("Type (income/expense): ");
        var type = Console.ReadLine();
        Console.Write("Amount: ");
        var amount = decimal.Parse(Console.ReadLine());
        Console.Write("Date (yyyy-mm-dd): ");
        var date = DateTime.Parse(Console.ReadLine());
        Console.Write("Description: ");
        var description = Console.ReadLine();

        if (type.ToLower() == "income")
        {
            Console.Write("Source: ");
            var source = Console.ReadLine();
            return new Income { Amount = amount, Date = date, Description = description, Source = source };
        }
        else
        {
            Console.Write("Category: ");
            var category = Console.ReadLine();
            return new Expense { Amount = amount, Date = date, Description = description, Category = category };
        }
    }

    public void DisplaySummary(string summary)
    {
        Console.WriteLine(summary);
    }

    public (string, decimal) PromptCategoryBudget()
    {
        Console.Write("Category: ");
        var category = Console.ReadLine();
        Console.Write("Limit: ");
        var amount = decimal.Parse(Console.ReadLine());
        return (category, amount);
    }
}