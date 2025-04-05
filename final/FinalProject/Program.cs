using System;
using System.Collections.Generic;
using System.IO;

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
