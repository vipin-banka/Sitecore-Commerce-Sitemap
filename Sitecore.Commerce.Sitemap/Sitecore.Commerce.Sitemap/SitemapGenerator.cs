using Sitecore.Commerce.XA.Foundation.Common.Context;
using Sitecore.Data.Items;
using Sitecore.XA.Feature.SiteMetadata.Sitemap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Commerce.Sitemap
{
    public class SitemapGenerator : Sitecore.XA.Feature.SiteMetadata.Sitemap.SitemapGenerator
    {
        public SitemapGenerator(IStorefrontContext storefrontContext) : base()
        {
            this.StorefrontContext = storefrontContext;
        }

        private IStorefrontContext StorefrontContext { get; }

        protected override IList<Item> ChildrenSearch(Item homeItem)
        {
            var items = base.ChildrenSearch(homeItem);
            items = items.Concat(this.GetCategoryAndProducts()).ToList();
            return items;
        }

        protected virtual IList<Item> GetCategoryAndProducts()
        {
            var service = new SearchManager(this.StorefrontContext);
            var items = service.GetAllCategoriesAndProducts();
            return items;
        }

        protected override string GetFullLink(Item item, SitemapLinkOptions options)
        {
            if (item.Template.ID.ToString().Equals(Constants.Template.Category, StringComparison.OrdinalIgnoreCase))
            {
                return GetCanonicalUrl(this.StorefrontContext.CurrentStorefront.CategoryPageRootPath, item, options);
            }
            else if (item.Template.ID.ToString().Equals(Constants.Template.Product, StringComparison.OrdinalIgnoreCase))
            {
                return GetCanonicalUrl(this.StorefrontContext.CurrentStorefront.ProductPageRootPath, item, options);
            }

            return base.GetFullLink(item, options);
        }

        private string GetCanonicalUrl(string sectionName, Item item, SitemapLinkOptions options)
        {
            return string.Format("{0}{1}{2}/{3}/{4}/{5}", options.Scheme, Uri.SchemeDelimiter, options.TargetHostname, options.UrlOptions.Language, sectionName, item.Name);
        }
    }
}