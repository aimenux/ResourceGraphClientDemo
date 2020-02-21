using System;
using Microsoft.Azure.Management.ResourceGraph.Models;

namespace App.Providers
{
    public interface IResourceGraphProvider : IDisposable
    {
        QueryResponse QueryResources(QueryRequest query);
    }
}
