using Microsoft.Azure.Management.ResourceGraph.Models;

namespace App.Builders
{
    public interface IQueryRequestBuilder
    {
        QueryRequest Build(QueryRequestOptions queryOptions = null);
    }
}
