using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Routing;

namespace Our.Umbraco.Extensions.Routing.Helpers
{
    public class DomainHelper
    {
        public Domain GetDomainByUri(UmbracoContext umbracoContext, Uri uri)
        {
            var baseDomains = GetDomainsByUri(umbracoContext, uri);

            return baseDomains.FirstOrDefault();
        }

        public IEnumerable<Domain> GetDomainsByUri(UmbracoContext umbracoContext, Uri uri)
        {
            var domains = umbracoContext.Domains.GetAll(false);

            var domainsAndUris = domains.Select(x => new DomainAndUri(x, uri));

            var uriWithSlash = uri.EndPathWithSlash();

            var baseDomains = GetBaseDomains(domainsAndUris, uriWithSlash);

            if (baseDomains.Any() == false)
            {
                baseDomains = GetBaseDomains(domainsAndUris, uriWithSlash.WithoutPort());
            }

            return baseDomains;
        }

        private IEnumerable<DomainAndUri> GetBaseDomains(IEnumerable<DomainAndUri> domainsAndUris, Uri uri)
        {
            return domainsAndUris.Where(x => x.Uri.EndPathWithSlash().IsBaseOf(uri) == true);
        }

        public IPublishedContent GetContentByDomain(UmbracoContext umbracoContext, Domain domain)
        {
            if (domain.ContentId < 1)
            {
                return null;
            }

            return umbracoContext.Content.GetById(domain.ContentId);
        }
    }
}