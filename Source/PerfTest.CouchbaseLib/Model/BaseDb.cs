using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfTest.CouchbaseLib.Model
{
    public abstract class BaseDb
    {
        protected BaseDb(string type)
        {
            CreateDate = DateTimeOffset.Now;
            Type = type;
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Type { get; }
        public DateTimeOffset CreateDate { get; }
        public int Version { get; } = 0;

        public string Key => $"{Type}_{Id}";
    }
}
