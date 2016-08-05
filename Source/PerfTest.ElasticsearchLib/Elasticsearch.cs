using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;

namespace PerfTest.ElasticsearchLib
{
    public class Elasticsearch : IElasticsearch
    {
        private const string DefaultIndex = "events-search";

        public Elasticsearch()
        {
        }

        public T GetById<T>(string id) where T : class
        {
            var model = GetClient().Get<T>(id);
            return model.Found ? model.Source : null;
        }

        public void Index<T>(Guid id, T model) where T : class
        {
            GetClient().Index(model, i => i.Id(id));
        }

        public IBulkResponse Bulk(List<IBulkOperation> operations)
        {
            var request = new BulkRequest()
            {
                Refresh = true,
                Consistency = Consistency.One,
                Operations = operations
            };

            IBulkResponse response = GetClient().Bulk(request);
            return response;
        }

        public ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector) where T : class
        {
            return GetClient().Search(searchSelector);
        }

        private ElasticClient GetClient()
        {
            var elasticsearchUrl = "http://localhost:9200";

            var node = new Uri(elasticsearchUrl);
            var settings = new ConnectionSettings(node)
                .DefaultIndex(DefaultIndex)
                ;

            var client = new ElasticClient(settings);

            return client;
        }
    }
}
