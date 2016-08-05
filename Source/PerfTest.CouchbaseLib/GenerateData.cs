using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.DictionaryAdapter;
using Couchbase;
using Couchbase.Configuration.Client;
using Couchbase.Core;
using Couchbase.Linq;
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


        public List<ActivityEvent> GetEventsByDate(out int total)
        {
            ClusterHelper.Initialize(new ClientConfiguration
            {
                Servers = new List<Uri> { new Uri("http://localhost:8091/") }
            });

            using (IBucket bucket = ClusterHelper.GetBucket(CouchbaseConfiguration.BUCKETNAME))
            {
                var date = new DateTime(2016, 1, 1);

                var context = new BucketContext(bucket);

                //var context = new BucketContext(bucket);
                var query = (from e in context.Query<ActivityEvent>()
                             where e.StartDate >= date
                             select e)
                             //.Skip(0)
                             .Take(500)
                             ;

                //total = query.Count();
                total = 0;
                //total = query.Count();
                //string queryIndex = "CREATE PRIMARY INDEX `perf-test-bucket-primary-index` ON `perf-test-bucket` USING GSI;";
                //var result = bucket.Query<ActivityEvent>(query);

                //var count = query.Count();
                var list = query.ToList();

                return list;
            }




        }
    }
}
