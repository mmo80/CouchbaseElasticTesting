using System;
using System.Collections.Generic;
using Nest;

namespace PerfTest.ElasticsearchLib
{
    public interface IElasticsearch
    {
        void Index<T>(Guid id, T model) where T : class;
        ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector) where T : class;
        T GetById<T>(string id) where T : class;
        IBulkResponse Bulk(List<IBulkOperation> operations);
    }
}