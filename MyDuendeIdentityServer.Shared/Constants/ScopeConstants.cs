using MyDuendeIdentityServer.Shared.Models;

namespace MyDuendeIdentityServer.Shared.Constants
{
    public static class ScopeConstants
    {
        public static readonly ScopeItem Api1Read = new ScopeItem(
            scopeName: "api1.read",
            scopeExplanation: "Read permission for API 1"
        );
        public static readonly ScopeItem Api1Write = new ScopeItem(
            scopeName: "api1.write",
            scopeExplanation: "Write permission for API 1"
        );
        public static readonly ScopeItem Api1Update = new ScopeItem(
            scopeName: "api1.update",
            scopeExplanation: "Update permission for API 1"
        );

        public static readonly ScopeItem Api2Read = new ScopeItem(
            scopeName: "api2.read",
            scopeExplanation: "Read permission for API 2"
        );
        public static readonly ScopeItem Api2Write = new ScopeItem(
            scopeName: "api2.write",
            scopeExplanation: "Write permission for API 2"
        );
        public static readonly ScopeItem Api2Update = new ScopeItem(
            scopeName: "api2.update",
            scopeExplanation: "Update permission for API 2"
        );
    }

}
