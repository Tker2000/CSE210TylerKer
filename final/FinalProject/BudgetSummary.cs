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
