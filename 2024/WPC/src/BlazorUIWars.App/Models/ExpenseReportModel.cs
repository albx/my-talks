using BlazorUIWars.Core;
using System.ComponentModel.DataAnnotations;

namespace BlazorUIWars.App.Models;

public class ExpenseReportModel
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public DateTime? Date { get; set; }

    public string? Notes { get; set; }

    [Required]
    public ExpenseReport.ExpenseType Type { get; set; }

    public decimal Amount { get; set; }

    public string? Location { get; set; }
}
