using BlazorUIWars.App.Models;

namespace BlazorUIWars.App.Services;

public interface IExpenseReportService
{
    Task<ExpenseReportListModel> GetExpenseReportsAsync();
    Task<ExpenseReportModel> GetExpenseReportAsync(Guid id);
    Task<Guid> CreateExpenseReportAsync(ExpenseReportModel model);
    Task UpdateExpenseReportAsync(Guid id, ExpenseReportModel model);
    Task DeleteExpenseReportAsync(Guid id);
}
