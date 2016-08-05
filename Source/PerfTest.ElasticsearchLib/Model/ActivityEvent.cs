using System;
using System.Collections.Generic;
using Nest;

namespace PerfTest.ElasticsearchLib.Model
{
    public abstract class BaseModel
    {
        [String]
        public Guid Id { get; set; }
    }

    [ElasticsearchType(Name = "ActivityEvent", IdProperty = "Id")]
    public class ActivityEvent : BaseModel
    {
        [String]
        public string EventName { get; set; }

        [Date(Format = "yyyy-mm-dd HH:mm")]
        public DateTimeOffset StartDate { get; set; }

        [Date(Format = "yyyy-mm-dd HH:mm")]
        public DateTimeOffset EndDate { get; set; }

        [String]
        public string Description { get; set; }

        [String]
        public string Location { get; set; }

        [String]
        public string Organizer { get; set; }
    }


    public class ActivityEventResult
    {
        public IEnumerable<ActivityEvent> Events { get; set; }
        public long TotalHits { get; set; }
    }
}
