using System.Net.Http;
using JetBrains.Annotations;
using Refit;

namespace Megingjord.Core
{
    [PublicAPI]
    public static class VeChainThorApiClientFactory
    {
        public static IVeChainThorApi CreateClient(string hostUrl)
        {
            return RestService.For<IVeChainThorApi>
            (
                hostUrl,
                CreateSettings()
            );
        }
        
        public static IVeChainThorApi CreateClient(HttpClient httpClient)
        {
            return RestService.For<IVeChainThorApi>
            (
                httpClient,
                CreateSettings()
            );
        }

        private static RefitSettings CreateSettings()
        {
            // Intended for future use
            return new RefitSettings();
        }
    }
}