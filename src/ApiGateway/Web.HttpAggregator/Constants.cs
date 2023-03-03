namespace Web.HttpAggregator;

internal static class Constants
{
    internal static class Routes
    {
        internal const string ApiBaseRoute = "api/v{version:apiVersion}";
        public const string RestaurantBaseRoute = ApiBaseRoute + "/restaurant";
    }
}
