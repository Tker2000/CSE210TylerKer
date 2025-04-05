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