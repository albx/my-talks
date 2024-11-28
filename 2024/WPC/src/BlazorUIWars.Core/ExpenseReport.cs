namespace BlazorUIWars.Core;

public class ExpenseReport
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Notes { get; set; }

    public DateOnly Date { get; set; }

    public ExpenseType Type { get; set; }

    public decimal Amount { get; set; }

    public string? Location { get; set; }

    public enum ExpenseType
    {
        Food,
        Travel,
        Lodging,
        Other
    }
}
