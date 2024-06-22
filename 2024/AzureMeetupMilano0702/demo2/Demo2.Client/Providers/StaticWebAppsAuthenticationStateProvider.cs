using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Demo2.Client.Providers;

public class StaticWebAppsAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IHttpClientFactory _httpClientFactory;

    public StaticWebAppsAuthenticationStateProvider(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("AuthClient");

            var userInfo = await httpClient.GetFromJsonAsync<UserInformation>(".auth/me");
            var clientPrincipal = userInfo!.ClientPrincipal;

            if (clientPrincipal is not null)
            {
                var identity = new ClaimsIdentity(clientPrincipal.IdentityProvider);
                if (!string.IsNullOrEmpty(clientPrincipal.UserId))
                {
                    identity.AddClaim(new(ClaimTypes.NameIdentifier, clientPrincipal.UserId));
                }
                if (!string.IsNullOrEmpty(clientPrincipal.UserDetails))
                {
                    identity.AddClaim(new(ClaimTypes.Name, clientPrincipal.UserDetails));
                }

                if (clientPrincipal.UserRoles.Length != 0)
                {
                    identity.AddClaims(clientPrincipal.UserRoles
                        .Where(r => !r.Equals("anonymous", StringComparison.InvariantCultureIgnoreCase))
                        .Select(r => new Claim(ClaimTypes.Role, r)));
                }

                return new AuthenticationState(new ClaimsPrincipal(identity));
            }

            return new AuthenticationState(new ClaimsPrincipal());
        }
        catch
        {
            return new AuthenticationState(new ClaimsPrincipal());
        }
    }

    internal class UserInformation
    {
        public ClientPrincipal? ClientPrincipal { get; init; }
    }

    internal record ClientPrincipal
    {
        public string IdentityProvider { get; init; } = string.Empty;

        public string UserId { get; init; } = string.Empty;

        public string UserDetails { get; init; } = string.Empty;

        public string[] UserRoles { get; init; } = [];
    }
}
