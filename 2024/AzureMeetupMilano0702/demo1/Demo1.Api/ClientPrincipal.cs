namespace Demo1.Api;

internal record ClientPrincipal
{
    public string IdentityProvider { get; init; } = string.Empty;

    public string UserId { get; init; } = string.Empty;

    public string UserDetails { get; init; } = string.Empty;

    public string[] UserRoles { get; init; } = [];
}
