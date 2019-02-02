using System.Configuration;

namespace Sitecore.Commerce.Sitemap
{
    public static class Constants
    {      
        public static class Template
        {
            public const string Category = "{4C4FD207-A9F7-443D-B32A-50AA33523661}";
            public const string Product = "{225F8638-2611-4841-9B89-19A5440A1DA1}";
        }

        public static class ItemType
        {
            public const string Category = "Category";
            public const string Product = "SellableItem";
        }

        public static class Fields
        {
            public static string LatestVersion = "_latestversion";
        }

        public static class Values
        {
            public static string LatestVersion = "1";
        }
    }
}