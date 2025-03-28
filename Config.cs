using Duende.IdentityServer.Models;

namespace LearnMicroservice.IDP;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            { };
    public static IEnumerable<ApiResource> ApiResource =>
        new ApiResource[]
            { };

    public static IEnumerable<Client> Clients =>
        new Client[]
            { };
}