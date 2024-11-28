using BlazorUIWars.Core;

namespace BlazorUIWars.App.Models;

public class ExpenseReportListModel
{
    public int Total { get; set; }

    public ExpenseReportListItem[] Items { get; set; } = [];

    public record ExpenseReportListItem(
        Guid Id,
        string Title,
        DateOnly Date,
        ExpenseReport.ExpenseType Type,
        decimal Amount);
}
