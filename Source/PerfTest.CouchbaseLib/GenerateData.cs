using System;
using System.Collections.Generic;
using Couchbase;
using Couchbase.Core;
using PerfTest.CouchbaseLib.Model;

namespace PerfTest.CouchbaseLib
{
    public class GenerateData
    {
        private Cluster _cluster;

        public GenerateData()
        {
            var config = new CouchbaseConfiguration();
            _cluster = new Cluster(config.GetConfiguration());
        }


        private readonly Random gen = new Random();
        public DateTime GenerateDate()
        {
            DateTime start = new DateTime(2010, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }

        public void BulkInsert()
        {
            using (IBucket bucket = _cluster.OpenBucket(CouchbaseConfiguration.BUCKETNAME))
            {
                var batchCount = 5000;
                for (int x = 0; x < batchCount; x++)
                {
                    var items = new Dictionary<string, ActivityEvent>();

                    for (int i = 1; i <= 1000; i++)
                    {
                        var date = GenerateDate();

                        var item = new ActivityEvent()
                        {
                            EventName = $"Event {x}#{i}",
                            Location = $"Göteborg-{x}-{i}",
                            StartDate = date,
                            EndDate = date.AddHours(1),
                            Organizer = $"Test Testsson the {x}{i}"
                        };
                        items.Add(item.Key, item);
                    }

                    var multiUpsert = bucket.Upsert(items);
                    //foreach (var item in multiUpsert)
                    //{
                    //    Assert.IsTrue(item.Value.Success);
                    //}
                }
            }
        }



        // Check out
        // url: http://blog.couchbase.com/2016/may/couchbase-with-windows-.net-part-4-linq2couchbase
        // url: https://github.com/couchbaselabs/Linq2Couchbase
		// url: http://blog.couchbase.com/facet/Topic/.NET
        public List<ActivityEvent> GetEventsByDate()
        {
            using (IBucket bucket = _cluster.OpenBucket(CouchbaseConfiguration.BUCKETNAME))
            {
                // url: http://developer.couchbase.com/documentation/server/current/n1ql/n1ql-language-reference/createprimaryindex.html
                // url: http://developer.couchbase.com/documentation/server/current/n1ql/n1ql-language-reference/datefun.html

                //string queryIndex = "CREATE PRIMARY INDEX `perf-test-bucket-primary-index` ON `perf-test-bucket` USING GSI;";
                // &startkey=[2013,4,16]&endkey=[2013,4,24]
                string query = $"SELECT r.eventName, r.startDate, r.id, r.`key`, r.endDate, r.type, r.version FROM `{CouchbaseConfiguration.BUCKETNAME}` r " +
                               $"WHERE r.type = 'event' AND r.startDate > '2016-01-01' LIMIT 500";
                // $"r WHERE r.type = 'event' AND r.startDate < '2015-01-01'";
                // WHERE r.type = 'event'
                // STR_TO_MILLIS("2014-02-01")
                /*
                 # paging
                    SELECT fname, age 
                    FROM tutorial
                        WHERE age > 30
                    LIMIT 2
                    OFFSET 2
                 */
                var result = bucket.Query<ActivityEvent>(query);
                return result.Rows;
            }
        }
    }
}
