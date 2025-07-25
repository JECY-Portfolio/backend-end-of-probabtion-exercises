﻿using event_scheduler_and_conflict_detector_api.Event.Domain.Enums;
using event_scheduler_and_conflict_detector_api.Event.Domain.Interfaces;

namespace event_scheduler_and_conflict_detector_api.Event.Domain.Models
{
    public class Event : IEvent
    {
        public Guid Id { get;  set; } = Guid.NewGuid();
        public required string Title  { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; }
        public List<string> Attendees { get; set; }
         public EventType EventType { get; set; }

        public Event(DateTime? startTime)
        {
            StartTime = startTime ?? DateTime.UtcNow;
            EndTime = StartTime.AddHours(1);
        }

        public Event()
        {
        }
    }
}
