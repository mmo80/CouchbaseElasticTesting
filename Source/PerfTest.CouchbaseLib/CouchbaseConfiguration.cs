using System;
using System.Collections.Generic;
using Couchbase.Configuration.Client;

namespace PerfTest.CouchbaseLib
{
    public class CouchbaseConfiguration
    {
        private ClientConfiguration config;

        internal static readonly string BUCKETNAME = "perf-test-bucket";

        // Url: http://docs.couchbase.com/developer/dotnet-2.1/configuring-the-client.html
        public ClientConfiguration GetConfiguration()
        {
            if (config == null)
            {
                config = new ClientConfiguration
                {
                    Servers = new List<Uri>
                    {
                        new Uri("http://127.0.0.1:8091/pools"),
                    },
                    //UseSsl = true,
                    //DefaultOperationLifespan = 1000,
                    BucketConfigs = new Dictionary<string, BucketConfiguration>
                    {
                        {
                            "my-bucket", new BucketConfiguration
                            {
                                BucketName = BUCKETNAME,
                                //seSsl = false,
                                //Password = "dev",
                                //DefaultOperationLifespan = 2000,
                                //PoolConfiguration = new PoolConfiguration
                                //{
                                //    MaxSize = 10,
                                //    MinSize = 5,
                                //    SendTimeout = 12000
                                //}
                            }
                        }
                    }
                };
            }
            return config;
        }
    }
}
