using System.ComponentModel.DataAnnotations;

namespace SmartSearches.Web.Models;

public class SearchModel
{
    [Required]
    public string IndexName { get; set; } = string.Empty;

    public QueryTypeEnum QueryType { get; set; } = QueryTypeEnum.Hybrid;

    [Required]
    public string Text { get; set; } = string.Empty;

    public enum QueryTypeEnum
    {
        Semantic,
        Vector,
        Hybrid
    }
}
