namespace MyDuendeIdentityServer.Shared.Models
{
    public class ScopeItem
    {
        public string ScopeName { get; init; }
        public string ScopeExplanation { get; init; }

        public ScopeItem(string scopeName, string scopeExplanation)
        {
            ScopeName = scopeName;
            ScopeExplanation = scopeExplanation;
        }
    }
}
