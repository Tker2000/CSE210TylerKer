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