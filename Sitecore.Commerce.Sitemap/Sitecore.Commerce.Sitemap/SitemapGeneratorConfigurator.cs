using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sitecore.DependencyInjection;
using Sitecore.XA.Feature.SiteMetadata.Sitemap;

namespace Sitecore.Commerce.Sitemap
{
    public class SitemapGeneratorConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.Replace(ServiceDescriptor.Transient<ISitemapGenerator, SitemapGenerator>());
        }
    }
}