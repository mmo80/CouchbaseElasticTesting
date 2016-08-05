using System;
using System.Collections.Generic;
using PerfTest.ElasticsearchLib.Model;

namespace PerfTest.ElasticsearchLib
{
    public class GenerateData
    {
        private readonly ActivityEventRepository _repo;

        public GenerateData()
        {
            IElasticsearch elasticsearch = new Elasticsearch();
            _repo = new ActivityEventRepository(elasticsearch);
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
            for (int x = 0; x < 50; x++)
            {
                var list = new List<ActivityEvent>();
                for (int i = 1; i <= 100000; i++)
                {
                    var date = GenerateDate();

                    var activity = new ActivityEvent
                    {
                        Id = Guid.NewGuid(),
                        EventName = $"Event {x}#{i}",
                        StartDate = date,
                        EndDate = date.AddHours(1),
                        Location = $"Göteborg-{x}-{i}",
                        Organizer = $"Test Testsson the {x}{i}"
                    };

                    list.Add(activity);
                }

                _repo.InsertBulk(list);
            }
        }


        public ActivityEventResult GetEventsByDate()
        {
            var start = new DateTime(2016, 1, 1);
            var end = new DateTime(2017, 1, 1);

            ActivityEventResult result = _repo.FindByDateRange(start, end, 0, 500);

            return result;
        }
    }
}
