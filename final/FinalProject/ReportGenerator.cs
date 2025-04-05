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