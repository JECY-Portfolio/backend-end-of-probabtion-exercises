using event_scheduler_and_conflict_detector_api.Event.Domain.Enums;

namespace event_scheduler_and_conflict_detector_api.Event.Application.Dtos
{
    public class EventUpdateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }

        public string Location { get; set; }
        public List<string> Attendees { get; set; }
        public EventType EventType { get; set; }
    }
}

