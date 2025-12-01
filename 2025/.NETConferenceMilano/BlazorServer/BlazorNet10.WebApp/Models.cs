using System.ComponentModel.DataAnnotations;

namespace BlazorNet10.WebApp;

[ValidatableType]
public class OrderModel
{
    public AddressInfo Address { get; set; } = new();

    public List<OrderLineItem> LineItems { get; set; } = [];
}

public record AddressInfo
{
    [Required]
    public string Address { get; set; } = string.Empty;

    [Required]
    public string City { get; set; } = string.Empty;

    [MinLength(5)]
    public string ZipCode { get; set; } = string.Empty;
}

public record OrderLineItem
{
    [Required]
    public string ItemName { get; set; } = string.Empty;

    [GreaterThanZero]
    public int Quantity { get; set; }
}

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class GreaterThanZeroAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (!int.TryParse(value?.ToString(), out int intValue))
        {
            return new ValidationResult("Invalid value");
        }

        if (intValue <= 0)
        {
            return new ValidationResult("Value must be greater than zero");
        }

        return ValidationResult.Success;
    }
}
