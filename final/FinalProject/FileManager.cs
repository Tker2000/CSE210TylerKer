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