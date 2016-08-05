using System;

namespace PerfTest.CouchbaseLib.Model
{
    public class ActivityEvent : BaseDb
    {
        public ActivityEvent() : base("event")
        { }

        public string EventName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Organizer { get; set; }
    }
}
