using System.Collections.Generic;
using Microsoft.Azure.Management.ResourceGraph.Models;
using Microsoft.Extensions.Options;

namespace App.Builders
{
    public class QueryRequestBuilder : IQueryRequestBuilder
    {
        private readonly IOptions<Settings> _options;

        public QueryRequestBuilder(IOptions<Settings> options)
        {
            _options = options;
        }

        public QueryRequest Build(QueryRequestOptions queryOptions = null)
        {
            var settings = _options.Value;

            var subscriptions = new List<string>
            {
                settings.SubscriptionId
            };

            return new QueryRequest(subscriptions, settings.ResourceGraphQuery, queryOptions);
        }
    }
}