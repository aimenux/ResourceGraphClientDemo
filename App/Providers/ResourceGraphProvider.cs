using Microsoft.Azure.Management.ResourceGraph;
using Microsoft.Azure.Management.ResourceGraph.Models;
using Microsoft.Rest;

namespace App.Providers
{
    public sealed class ResourceGraphProvider : IResourceGraphProvider
    {
        private readonly IResourceGraphClient _resourceGraphClient;

        public ResourceGraphProvider(ServiceClientCredentials credentials)
        {
            _resourceGraphClient = new ResourceGraphClient(credentials);
        }

        public QueryResponse QueryResources(QueryRequest query)
        {
            return _resourceGraphClient.Resources(query);
        }

        public void Dispose()
        {
            _resourceGraphClient?.Dispose();
        }
    }
}