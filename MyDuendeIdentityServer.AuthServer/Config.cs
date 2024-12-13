using Duende.IdentityServer.Models;
using MyDuendeIdentityServer.Shared.Constants;

namespace MyDuendeIdentityServer.AuthServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>() {
                new ApiResource(ResourceConstants.ResourceApi1)
                {
                    Scopes={ScopeConstants.Api1Read.ScopeName,ScopeConstants.Api1Write.ScopeName, ScopeConstants.Api1Update.ScopeName },
                    ApiSecrets = new [] { new Secret("secretapi1".Sha256()) }
                },
                new ApiResource(ResourceConstants.ResourceApi2)
                {
                    Scopes={ScopeConstants.Api2Read.ScopeName, ScopeConstants.Api2Write.ScopeName, ScopeConstants.Api2Update.ScopeName },
                    ApiSecrets = new [] { new Secret("secretapi2".Sha256())}
                },
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {

            return new List<ApiScope>()
            {
                new ApiScope(ScopeConstants.Api1Read.ScopeName,ScopeConstants.Api1Read.ScopeExplanation),
                new ApiScope(ScopeConstants.Api1Write.ScopeName,ScopeConstants.Api1Write.ScopeExplanation),
                new ApiScope(ScopeConstants.Api1Update.ScopeName,ScopeConstants.Api1Update.ScopeExplanation),
                new ApiScope(ScopeConstants.Api2Read.ScopeName,ScopeConstants.Api2Read.ScopeExplanation),
                new ApiScope(ScopeConstants.Api2Write.ScopeName,ScopeConstants.Api2Write.ScopeExplanation),
                new ApiScope(ScopeConstants.Api2Update.ScopeName,ScopeConstants.Api2Update.ScopeExplanation),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>(){
                new Client()
                {
                    ClientId = "Client1",
                    ClientName = "Client 1 web app",
                    ClientSecrets = new[] {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {
                        ScopeConstants.Api1Read.ScopeName,
                        ScopeConstants.Api1Write.ScopeName,
                        ScopeConstants.Api1Update.ScopeName,
                    }
                },
                new Client()
                {
                    ClientId = "Client2",
                    ClientName = "Client 2 web app",
                    ClientSecrets = new[] {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {
                        ScopeConstants.Api1Read.ScopeName,
                        ScopeConstants.Api2Write.ScopeName,
                        ScopeConstants.Api2Update.ScopeName,
                    }
                }
            };
        }
    }
}
