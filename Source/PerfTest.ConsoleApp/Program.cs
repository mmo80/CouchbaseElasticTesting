using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfTest.CouchbaseLib;
using PerfTest.ElasticsearchLib;
using GenerateDataCouch = PerfTest.CouchbaseLib.GenerateData;
using GenerateDataElastic = PerfTest.ElasticsearchLib.GenerateData;

namespace PerfTest.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PerfTestQueries();
            //BulkInsertData();
        }


        private static void PerfTestQueries()
        {
            CouchbaseTests();
            //ElasticsearchTests();

            Console.ReadLine();
        }


        private static void ElasticsearchTests()
        {
            Console.WriteLine("START - Elasticsearch");

            Stopwatch watch = Stopwatch.StartNew();

            // the code that you want to measure comes here
            var elasticLib = new GenerateDataElastic();
            var result = elasticLib.GetEventsByDate();

            Console.WriteLine($"list.Count: {result.TotalHits}");

            watch.Stop();
            long elapsedMs = watch.ElapsedMilliseconds;
            double elapsedSeconds = watch.Elapsed.TotalSeconds;

            Console.WriteLine("elasticLib.GetEventsByDate() took:");
            Console.WriteLine($"elapsedMs:{elapsedMs}");
            Console.WriteLine($"elapsedSeconds:{elapsedSeconds}");
        }


        private static void CouchbaseTests()
        {
            Console.WriteLine("START - Couchbase");

            Stopwatch watch = Stopwatch.StartNew();

            // the code that you want to measure comes here
            var couchbaseLib = new GenerateDataCouch();
            var list = couchbaseLib.GetEventsByDate();

            Console.WriteLine($"list.Count: {list.Count}");

            watch.Stop();
            long elapsedMs = watch.ElapsedMilliseconds;
            double elapsedSeconds = watch.Elapsed.TotalSeconds;

            Console.WriteLine("couchbaseLib.GetEventsByDate() took:");
            Console.WriteLine($"elapsedMs:{elapsedMs}");
            Console.WriteLine($"elapsedSeconds:{elapsedSeconds}");
        }


        private static void BulkInsertData()
        {
            Stopwatch watch;
            long elapsedMs;
            double elapsedSeconds;

            //Console.WriteLine("START - Couchbase");

            //watch = Stopwatch.StartNew();

            //// the code that you want to measure comes here
            //var couchbaseLib = new GenerateDataCouch();
            //couchbaseLib.BulkInsert();

            //watch.Stop();
            //elapsedMs = watch.ElapsedMilliseconds;
            //elapsedSeconds = watch.Elapsed.TotalSeconds;


            //Console.WriteLine("couchbaseLib.BulkInsert() took:");
            //Console.WriteLine($"elapsedMs:{elapsedMs}");
            //Console.WriteLine($"elapsedSeconds:{elapsedSeconds}");


            Console.WriteLine("START - Elasticsearch");

            watch = Stopwatch.StartNew();

            // the code that you want to measure comes here
            var elasticLib = new GenerateDataElastic();
            elasticLib.BulkInsert();

            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            elapsedSeconds = watch.Elapsed.TotalSeconds;


            Console.WriteLine("elasticLib.BulkInsert() took:");
            Console.WriteLine($"elapsedMs:{elapsedMs}");
            Console.WriteLine($"elapsedSeconds:{elapsedSeconds}");




            Console.ReadLine();
        }
    }
}
