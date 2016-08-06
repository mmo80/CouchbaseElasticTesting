using System;
using System.Diagnostics;
using GenerateDataCouch = PerfTest.CouchbaseLib.GenerateData;
using GenerateDataElastic = PerfTest.ElasticsearchLib.GenerateData;

namespace PerfTest.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PerfTestQueries();
            //BulkInsert();

            Console.ReadLine();
        }


        private static void PerfTestQueries()
        {
            CouchbaseTests();
            ElasticsearchTests();
        }

        private static void ElasticsearchTests()
        {
            Console.WriteLine("START - Elasticsearch");

            Stopwatch watch = Stopwatch.StartNew();

            var elasticLib = new GenerateDataElastic();
            var result = elasticLib.GetEventsByDate();

            watch.Stop();

            Console.WriteLine($"list.Count: {result.TotalHits}");

            Console.WriteLine("elasticLib.GetEventsByDate() took:");
            Console.WriteLine($"elapsedMs:{watch.ElapsedMilliseconds}");
            Console.WriteLine($"elapsedSeconds:{watch.Elapsed.TotalSeconds}");
        }

        private static void CouchbaseTests()
        {
            Console.WriteLine("START - Couchbase");

            Stopwatch watch = Stopwatch.StartNew();

            var couchbaseLib = new GenerateDataCouch();
            int total;
            var list = couchbaseLib.GetEventsByDate(out total);

            Console.WriteLine($"total: {total}");
            Console.WriteLine($"list.Count: {list.Count}");

            watch.Stop();

            Console.WriteLine("couchbaseLib.GetEventsByDate() took:");
            Console.WriteLine($"elapsedMs:{watch.ElapsedMilliseconds}");
            Console.WriteLine($"elapsedSeconds:{watch.Elapsed.TotalSeconds}");
        }

        private static void BulkInsert()
        {
            BulkInsertCouchbase();
            BulkInsertElasticsearch();
        }

        private static void BulkInsertElasticsearch()
        {
            Console.WriteLine("START - Elasticsearch");
            var watch = Stopwatch.StartNew();

            var elasticLib = new GenerateDataElastic();
            elasticLib.BulkInsert();

            watch.Stop();

            Console.WriteLine("elasticLib.BulkInsert() took:");
            Console.WriteLine($"elapsedMs:{watch.ElapsedMilliseconds}");
            Console.WriteLine($"elapsedSeconds:{watch.Elapsed.TotalSeconds}");
        }

        private static void BulkInsertCouchbase()
        {
            Console.WriteLine("START - Couchbase");
            var watch = Stopwatch.StartNew();

            var couchbaseLib = new GenerateDataCouch();
            couchbaseLib.BulkInsert();

            watch.Stop();

            Console.WriteLine("couchbaseLib.BulkInsert() took:");
            Console.WriteLine($"elapsedMs:{watch.ElapsedMilliseconds}");
            Console.WriteLine($"elapsedSeconds:{watch.Elapsed.TotalSeconds}");
        }
    }
}
