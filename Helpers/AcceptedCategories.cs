using System.Collections.Immutable;

namespace ZenkoAPI.Helpers
{
    public static class AcceptedCategories
    {
        // could get this from a database for custom categories later on.
        public static ImmutableList<string> categories = ["general", "eating out", "bills", "entertainment", "transport", "shopping", "groceries", "subscriptions", "debt"];
    }
}
