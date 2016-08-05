using System;
using System.Collections.Generic;
using Nest;
using PerfTest.ElasticsearchLib.Model;

namespace PerfTest.ElasticsearchLib
{
    public class ActivityEventRepository
    {
        private readonly IElasticsearch _elasticSearch;

        public ActivityEventRepository(IElasticsearch elasticSearch)
        {
            _elasticSearch = elasticSearch;
        }

        public void AddOrUpdate<T>(T model) where T : BaseModel
        {
            _elasticSearch.Index(model.Id, model);
        }

        public void InsertBulk<T>(List<T> models) where T : BaseModel
        {
            var operations = new List<IBulkOperation>();

            foreach (var model in models)
            {
                operations.Add(new BulkIndexOperation<T>(model));
            }

            var response = _elasticSearch.Bulk(operations);
        }

        public ActivityEvent FindCustomerByCustomerId(string id)
        {
            return _elasticSearch.GetById<ActivityEvent>(id);
        }

        public ActivityEventResult FindByDateRange(DateTime startDate, DateTime endDate, int startIndex, int take)
        {

            // url: https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/date-range-query-usage.html
            var result = _elasticSearch.Search<ActivityEvent>(s => s
                .From(startIndex)
                .Take(take)
                .Query(q =>
                    q.DateRange(r =>
                        r.Field(f => f.StartDate)
                        .GreaterThanOrEquals(startDate)
                        .LessThanOrEquals(endDate)
                        )
                ));

            return new ActivityEventResult
            {
                Events = result.Documents,
                TotalHits = result.Total
            };
        }
    }
}
