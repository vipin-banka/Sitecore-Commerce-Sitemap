using Sitecore.Commerce.Engine.Connect;
using Sitecore.Commerce.Engine.Connect.Interfaces;
using Sitecore.Commerce.Engine.Connect.Search;
using Sitecore.Commerce.XA.Foundation.Common.Context;
using Sitecore.Commerce.XA.Foundation.Common.Utils;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Exceptions;
using Sitecore.Data;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sitecore.Commerce.Sitemap
{
    public class SearchManager
    {
        public SearchManager(IStorefrontContext storefrontContext)
        {
            this.StorefrontContext = storefrontContext;
        }

        protected IStorefrontContext StorefrontContext { get; private set; }

        public virtual IList<Item> GetAllCategoriesAndProducts()
        {
            List<Item> objList = new List<Item>();
            try
            {
                ISearchIndex index = CommerceTypeLoader.CreateInstance<ICommerceSearchManager>().GetIndex();
                ID startNavigationCategoryId = this.StorefrontContext.CurrentStorefront.GetStartNavigationCategory();
                using (IProviderSearchContext searchContext = index.CreateSearchContext())
                {
                    objList = searchContext.GetQueryable<CommerceSearchResultItem>()
                        .Where(item => item.ItemId != startNavigationCategoryId)
                        .Where(item => item.Paths.Contains(startNavigationCategoryId))
                        .Where(item => item.CommerceSearchItemType.Equals(Constants.ItemType.Category) || item.CommerceSearchItemType.Equals(Constants.ItemType.Product))
                        .Where(item => item.Language == Context.Language.Name)
                        .Where(item => item[Constants.Fields.LatestVersion].Equals(Constants.Values.LatestVersion))
                        .Select(p => p.GetItem())
                        .ToList()
                        .Where(item => item != null)
                        .ToList();
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                Helpers.LogErrorMessage(this.StorefrontContext.GetSystemMessage("Search Index Directory Not Found Error", true), (object)ex, (Exception)ex);
            }
            catch (IndexNotFoundException ex)
            {
                Helpers.LogErrorMessage(this.StorefrontContext.GetSystemMessage("Search Index Not Found Error", true), (object)ex, (Exception)ex);
            }
            catch (Exception ex)
            {
                Helpers.LogErrorMessage(this.StorefrontContext.GetSystemMessage("Catalog Configuration Error", true), (object)ex, ex);
            }

            return objList;
        }
    }
}