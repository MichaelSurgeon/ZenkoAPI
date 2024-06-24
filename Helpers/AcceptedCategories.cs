using System.Collections.Immutable;

namespace ZenkoAPI.Helpers
{
    public static class AcceptedCategories
    {
        public static ImmutableList<string> categories = ImmutableList.Create<string>
        (
            "general",
            "eating out",
            "bills",
            "entertainment",
            "transport",
            "shopping",
            "groceries",
            "subscriptions",
            "debt"
        );
    }
}
