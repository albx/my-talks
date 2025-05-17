using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Models;
using SmartSearches.Web.Models;

namespace SmartSearches.Web.Components.Pages;
public partial class Home(SearchIndexClient searchIndexClient)
{
    private SearchModel model = new();

    private SearchResultItemModel[]? results;

    private bool searching;

    private string? errorMessage;

    private readonly string[] indexes = ["vector-1745239962364", "vector-1745240405873-small"];

    protected override void OnInitialized()
    {
        model.IndexName = indexes[0];
    }

    private async Task SearchAsync()
    {
        searching = true;
        errorMessage = null;

        try
        {
            var searchOptions = BuildSearchOptions(model);

            var searchClient = searchIndexClient.GetSearchClient(model.IndexName);
            var result = await searchClient.SearchAsync<IndexDescriptor>(
                model.Text, 
                searchOptions);

            results = result.Value.GetResults()
                .Select(r => new SearchResultItemModel
                {
                    Document = r.Document,
                    Score = r.Score,
                    RerankerScore = r.SemanticSearch?.RerankerScore,
                    Captions = r.SemanticSearch?.Captions?.Select(c => new CaptionDescriptor
                    {
                        Text = c.Text,
                        Highlights = c.Highlights
                    }).ToArray()
                }).ToArray() ?? [];
        }
        catch (Exception ex)
        {
            errorMessage = ex.ToString();
        }
        finally
        {
            searching = false;
        }
    }

    private SearchOptions BuildSearchOptions(SearchModel model)
    {
        var semanticConfigurationName = $"{model.IndexName}-semantic-configuration";
        var options = model.QueryType switch
        {
            SearchModel.QueryTypeEnum.Semantic => new SearchOptions
            {
                QueryType = SearchQueryType.Semantic,
                Size = 50,
                SemanticSearch = new SemanticSearchOptions
                {
                    SemanticConfigurationName = semanticConfigurationName
                },
            },
            SearchModel.QueryTypeEnum.Vector => new SearchOptions
            {
                QueryType = SearchQueryType.Semantic,
                Size = 50,
                VectorSearch = new VectorSearchOptions
                {
                    Queries =
                    {
                        new VectorizableTextQuery(model.Text)
                        {
                           Fields = { "text_vector" }
                        }
                    }
                }
            },
            _ => new SearchOptions
            {
                QueryType = SearchQueryType.Semantic,
                Size = 50,
                SemanticSearch = new SemanticSearchOptions
                {
                    SemanticConfigurationName = semanticConfigurationName
                },
                VectorSearch = new VectorSearchOptions
                {
                    Queries =
                    {
                        new VectorizableTextQuery(model.Text)
                        {
                           Fields = { "text_vector" }
                        }
                    }
                }
            }
        };

        return options;
    }
}
