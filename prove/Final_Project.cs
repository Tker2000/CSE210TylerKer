// Budget Manager App - Base Project Structure

using System;
using System.Collections.Generic;

// Abstract Base Class
public abstract class Transaction
{
    public decimal Amount { get; protected set; }
    public DateTime Date { get; protected set; }
    public string Description { get; protected set; }

    public Transaction(decimal amount, DateTime date, string description)
    {
        Amount = amount;
        Date = date;
        Description = description;
    }

    public abstract string GetSummary();
}

// Income Transaction
public class Income : Transaction
{
    public string Source { get; private set; }

    public Income(decimal amount, DateTime date, string description, string source)
        : base(amount, date, description)
    {
        Source = source;
    }

    public override string GetSummary()
    {
        return $"[INCOME] {Date.ToShortDateString()} - {Source}: +${Amount} ({Description})";
    }
}

// Expense Transaction
public class Expense : Transaction
{
    public string Category { get; private set; }

    public Expense(decimal amount, DateTime date, string description, string category)
        : base(amount, date, description)
    {
        Category = category;
    }

    public override string GetSummary()
    {
        return $"[EXPENSE] {Date.ToShortDateString()} - {Category}: -${Amount} ({Description})";
    }
}

// Budget Category with Monthly Limit
public class BudgetCategory
{
    public string Name { get; private set; }
    public decimal MonthlyLimit { get; private set; }

    public BudgetCategory(string name, decimal limit)
    {
        Name = name;
        MonthlyLimit = limit;
    }
}

// Goal Tracker
public class GoalTracker
{
    public decimal GoalAmount { get; private set; }
    public decimal CurrentSaved { get; private set; }

    public GoalTracker(decimal goal)
    {
        GoalAmount = goal;
        CurrentSaved = 0;
    }

    public void AddSavings(decimal amount)
    {
        CurrentSaved += amount;
    }

    public string GetProgress()
    {
        return $"Saved ${CurrentSaved} of ${GoalAmount} goal ({(CurrentSaved / GoalAmount) * 100:F2}%)";
    }
}

// Transaction Manager
public class TransactionManager
{
    private List<Transaction> transactions = new List<Transaction>();

    public void AddTransaction(Transaction t)
    {
        transactions.Add(t);
    }

    public List<Transaction> GetAllTransactions()
    {
        return transactions;
    }

    public decimal GetBalance()
    {
        decimal balance = 0;
        foreach (var t in transactions)
        {
            balance += (t is Income) ? t.Amount : -t.Amount;
        }
        return balance;
    }
}

// Budget Summary
public class BudgetSummary
{
    public void DisplaySummary(List<Transaction> transactions)
    {
        decimal incomeTotal = 0, expenseTotal = 0;
        foreach (var t in transactions)
        {
            if (t is Income) incomeTotal += t.Amount;
            if (t is Expense) expenseTotal += t.Amount;
        }

        Console.WriteLine($"Total Income: ${incomeTotal}");
        Console.WriteLine($"Total Expenses: ${expenseTotal}");
        Console.WriteLine($"Net Balance: ${incomeTotal - expenseTotal}");
    }
}

// Report Generator
public class ReportGenerator
{
    public void PrintCategoryReport(List<Transaction> transactions)
    {
        var categoryTotals = new Dictionary<string, decimal>();

        foreach (var t in transactions)
        {
            if (t is Expense e)
            {
                if (!categoryTotals.ContainsKey(e.Category))
                    categoryTotals[e.Category] = 0;
                categoryTotals[e.Category] += e.Amount;
            }
        }

        Console.WriteLine("\n--- Expense Report by Category ---");
        foreach (var pair in categoryTotals)
        {
            Console.WriteLine($"{pair.Key}: ${pair.Value}");
        }
    }
}

// User Interface (simplified)
public class UserInterface
{
    private TransactionManager manager = new TransactionManager();
    private BudgetSummary summary = new BudgetSummary();
    private ReportGenerator report = new ReportGenerator();

    public void Run()
    {
        Console.WriteLine("Welcome to the Budget Manager App!");
        // Menu loop would go here (Add income, Add expense, View summary, etc.)
    }
}

// Entry Point
public class Program
{
    public static void Main()
    {
        var ui = new UserInterface();
        ui.Run();
    }
}
