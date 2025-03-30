// Abstract Transaction class
public abstract class Transaction
{
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
}

// Income class
public class Income : Transaction
{
    public string Source { get; set; }
}

// Expense class
public class Expense : Transaction
{
    public string Category { get; set; }
}

// TransactionManager class
public class TransactionManager
{
    private List<Transaction> _transactions = new List<Transaction>();

    public void AddTransaction(Transaction t) => _transactions.Add(t);
    public void RemoveTransaction(Transaction t) => _transactions.Remove(t);
    public List<Transaction> GetTransactionsByMonth(DateTime month) =>
        _transactions.Where(t => t.Date.Month == month.Month && t.Date.Year == month.Year).ToList();
    public decimal GetTotalIncome() =>
        _transactions.OfType<Income>().Sum(i => i.Amount);
    public decimal GetTotalExpenses() =>
        _transactions.OfType<Expense>().Sum(e => e.Amount);
    public List<Expense> GetTransactionsByCategory(string category) =>
        _transactions.OfType<Expense>().Where(e => e.Category == category).ToList();
    public List<Transaction> GetAllTransactions() => _transactions;
}

// BudgetSummary class
public class BudgetSummary
{
    private TransactionManager _manager;

    public BudgetSummary(TransactionManager manager)
    {
        _manager = manager;
    }

    public string GenerateMonthlySummary(DateTime month)
    {
        var transactions = _manager.GetTransactionsByMonth(month);
        var income = transactions.OfType<Income>().Sum(i => i.Amount);
        var expenses = transactions.OfType<Expense>().Sum(e => e.Amount);
        return $"Summary for {month.ToString("MMMM yyyy")}:\nIncome: {income:C}\nExpenses: {expenses:C}\nBalance: {(income - expenses):C}";
    }

    public Dictionary<string, decimal> GenerateCategorySummary()
    {
        return _manager.GetAllTransactions()
                        .OfType<Expense>()
                        .GroupBy(e => e.Category)
                        .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));
    }
}

// GoalTracker class
public class GoalTracker
{
    private Dictionary<string, decimal> _goals = new();
    private Dictionary<string, decimal> _progress = new();

    public void SetGoal(string name, decimal amount)
    {
        _goals[name] = amount;
        if (!_progress.ContainsKey(name)) _progress[name] = 0;
    }

    public void UpdateProgress(string name, decimal amount)
    {
        if (_progress.ContainsKey(name))
            _progress[name] += amount;
    }

    public bool IsGoalMet(string name) => _progress.ContainsKey(name) && _progress[name] >= _goals[name];
}

// FileManager class
public class FileManager
{
    public void SaveData(string filepath, List<Transaction> transactions)
    {
        var lines = transactions.Select(t =>
        {
            var type = t is Income ? "Income" : "Expense";
            var details = t is Income i ? i.Source : ((Expense)t).Category;
            return $"{type},{t.Amount},{t.Date},{t.Description},{details}";
        });
        File.WriteAllLines(filepath, lines);
    }

    public List<Transaction> LoadData(string filepath)
    {
        var transactions = new List<Transaction>();
        if (!File.Exists(filepath)) return transactions;

        foreach (var line in File.ReadAllLines(filepath))
        {
            var parts = line.Split(',');
            var type = parts[0];
            var amount = decimal.Parse(parts[1]);
            var date = DateTime.Parse(parts[2]);
            var description = parts[3];
            var detail = parts[4];

            if (type == "Income")
                transactions.Add(new Income { Amount = amount, Date = date, Description = description, Source = detail });
            else
                transactions.Add(new Expense { Amount = amount, Date = date, Description = description, Category = detail });
        }
        return transactions;
    }
}

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

// BudgetCategory class
public class BudgetCategory
{
    private Dictionary<string, decimal> _categoryLimits = new();
    private Dictionary<string, decimal> _categorySpending = new();

    public void SetLimit(string category, decimal amount) => _categoryLimits[category] = amount;

    public void UpdateSpending(string category, decimal amount)
    {
        if (!_categorySpending.ContainsKey(category))
            _categorySpending[category] = 0;
        _categorySpending[category] += amount;
    }

    public bool IsLimitExceeded(string category)
    {
        return _categoryLimits.ContainsKey(category) &&
               _categorySpending.ContainsKey(category) &&
               _categorySpending[category] > _categoryLimits[category];
    }
}

// ReportGenerator class
public class ReportGenerator
{
    public string GenerateSpendingReport(Dictionary<string, decimal> categorySummary)
    {
        return string.Join("\n", categorySummary.Select(kvp => $"{kvp.Key}: {kvp.Value:C}"));
    }

    public List<string> GenerateOverspendingAlerts(BudgetCategory budget)
    {
        var alerts = new List<string>();
        foreach (var category in budget.GetType().GetField("_categoryLimits", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(budget) as Dictionary<string, decimal>)
        {
            if (budget.IsLimitExceeded(category.Key))
                alerts.Add($"Warning: {category.Key} limit exceeded!");
        }
        return alerts;
    }

    public void ExportToFile(string path, string report)
    {
        File.WriteAllText(path, report);
    }
}

// Program class
public class Program
{
    static void Main()
    {
        var transactionManager = new TransactionManager();
        var fileManager = new FileManager();
        var ui = new UserInterface();
        var summary = new BudgetSummary(transactionManager);
        var budget = new BudgetCategory();
        var report = new ReportGenerator();

        transactionManager = new TransactionManager();
        var path = "transactions.txt";
        foreach (var t in fileManager.LoadData(path))
            transactionManager.AddTransaction(t);

        bool running = true;
        while (running)
        {
            ui.DisplayMainMenu();
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var transaction = ui.PromptTransactionEntry();
                    transactionManager.AddTransaction(transaction);
                    if (transaction is Expense e)
                        budget.UpdateSpending(e.Category, e.Amount);
                    break;
                case "2":
                    Console.Write("Enter month (yyyy-mm): ");
                    var date = DateTime.Parse(Console.ReadLine() + "-01");
                    var s = summary.GenerateMonthlySummary(date);
                    var catSummary = summary.GenerateCategorySummary();
                    var fullReport = s + "\n" + report.GenerateSpendingReport(catSummary);
                    ui.DisplaySummary(fullReport);
                    break;
                case "3":
                    var (cat, amt) = ui.PromptCategoryBudget();
                    budget.SetLimit(cat, amt);
                    break;
                case "4":
                    fileManager.SaveData(path, transactionManager.GetAllTransactions());
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}
