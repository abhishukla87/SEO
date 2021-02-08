using SEOInfo.Helper;
using SEOInfo.Service;
using System;

namespace SEOInfo.Factory
{
    public class SearchServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SearchServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ISearchService GetSearchService(string ServiceType)        
        {
            if (Enum.TryParse(ServiceType, out SearchServiceType value))
            {
                switch (value)
                {
                    case SearchServiceType.Google:
                        return (ISearchService)_serviceProvider.GetService(typeof(GoogleService));

                    case SearchServiceType.Bing:
                        return (ISearchService)_serviceProvider.GetService(typeof(BingService));
                    default:
                        throw new Exception("Service type not supported");
                }
            }
            else
            {
                throw new Exception("Service type not supported");
            }

        }

    }
}
