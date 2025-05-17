using System.Text.Json.Serialization;

namespace SmartSearches.Web.Models;

public record IndexDescriptor
{
    [JsonPropertyName("chunk_id")]
    public string ChunkId { get; init; } = string.Empty;

    [JsonPropertyName("parent_id")]
    public string ParentId { get; init; } = string.Empty;

    [JsonPropertyName("chunk")]
    public string Chunk { get; init; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
}
