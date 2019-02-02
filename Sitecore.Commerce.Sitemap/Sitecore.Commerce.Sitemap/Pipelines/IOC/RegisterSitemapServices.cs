using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sitecore.DependencyInjection;
using Sitecore.XA.Feature.SiteMetadata.Sitemap;

namespace Sitecore.Commerce.Sitemap.Pipelines.IOC
{
    public class RegisterSitemapServices : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.Replace(ServiceDescriptor.Transient<ISitemapGenerator, SitemapGenerator>());
        }
    }
}