using BlazorUIWars.App.Models;
using BlazorUIWars.Core;
using Microsoft.EntityFrameworkCore;

namespace BlazorUIWars.App.Services;

public class ExpenseReportService : IExpenseReportService
{
    private readonly ExpenseReportDbContext _context;

    public ExpenseReportService(ExpenseReportDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Guid> CreateExpenseReportAsync(ExpenseReportModel model)
    {
        var expenseReport = new ExpenseReport
        {
            Id = Guid.NewGuid(),
            Title = model.Title,
            Date = DateOnly.FromDateTime(model.Date!.Value),
            Notes = model.Notes,
            Type = model.Type,
            Amount = model.Amount,
            Location = model.Location
        };

        _context.ExpenseReports.Add(expenseReport);
        await _context.SaveChangesAsync();

        return expenseReport.Id;
    }

    public async Task DeleteExpenseReportAsync(Guid id)
    {
        var expenseReport = _context.ExpenseReports.Find(id);
        if (expenseReport is null)
        {
            throw new InvalidOperationException("Expense report not found");
        }

        _context.ExpenseReports.Remove(expenseReport);
        await _context.SaveChangesAsync();
    }

    public async Task<ExpenseReportModel> GetExpenseReportAsync(Guid id)
    {
        var expenseReport = await _context.ExpenseReports.FindAsync(id);
        if (expenseReport is null)
        {
            throw new InvalidOperationException("Expense report not found");
        }

        return new ExpenseReportModel
        {
            Title = expenseReport.Title,
            Date = expenseReport.Date.ToDateTime(TimeOnly.MinValue),
            Notes = expenseReport.Notes,
            Type = expenseReport.Type,
            Amount = expenseReport.Amount,
            Location = expenseReport.Location
        };
    }

    public async Task<ExpenseReportListModel> GetExpenseReportsAsync()
    {
        var expenseReports = await _context.ExpenseReports
            .OrderBy(e => e.Date)
            .Select(e => new ExpenseReportListModel.ExpenseReportListItem(
                e.Id,
                e.Title,
                e.Date,
                e.Type,
                e.Amount)).ToArrayAsync();

        return new ExpenseReportListModel
        {
            Total = expenseReports.Length,
            Items = expenseReports
        };
    }

    public async Task UpdateExpenseReportAsync(Guid id, ExpenseReportModel model)
    {
        var expenseReport = _context.ExpenseReports.Find(id);
        if (expenseReport is null)
        {
            throw new InvalidOperationException("Expense report not found");
        }

        expenseReport.Title = model.Title;
        expenseReport.Date = DateOnly.FromDateTime(model.Date!.Value);
        expenseReport.Notes = model.Notes;
        expenseReport.Type = model.Type;
        expenseReport.Amount = model.Amount;
        expenseReport.Location = model.Location;

        await _context.SaveChangesAsync();
    }
}
