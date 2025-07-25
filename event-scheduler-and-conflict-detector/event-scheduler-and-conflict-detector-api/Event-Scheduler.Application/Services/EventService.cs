using event_scheduler_and_conflict_detector_api.Event.Application.Dtos;
using event_scheduler_and_conflict_detector_api.Event.Application.Interfaces;

namespace event_scheduler_and_conflict_detector_api.Event_Scheduler.Application.Services
{
    public class EventService : IEventService
    {
        private static readonly Dictionary<Guid, Event.Domain.Models.Event> _events = new();

        public EventService()
        {
            var event1 = new Event.Domain.Models.Event(new DateTime(2025, 07, 25, 14, 0, 0))
            {
                Id = Guid.NewGuid(),
                Title = "Internship Probation",
                Description = "Initial",
                Location = "Meet",
                Attendees = new List<string> { "Ada", "Obi" },
                EventType = Event.Domain.Enums.EventType.Meeting
            };
            var event2 = new Event.Domain.Models.Event(new DateTime(2025, 07, 25, 17, 0, 0))
            {
                Id = Guid.NewGuid(),
                Title = "Doctors Appointment",
                Description = "Check up",
                Location = "Hospital",
                Attendees = new List<string> { "Jecy" },
                EventType = Event.Domain.Enums.EventType.Appointment
            };
            _events[event1.Id] = event1;
            _events[event2.Id] = event2;
        }

        private bool HasConflict(Event.Domain.Models.Event  newEvent)
        {
            return _events.Values.Any(e =>
            e.Location.Equals(newEvent.Location, StringComparison.OrdinalIgnoreCase) &&
            e.StartTime < newEvent.EndTime && newEvent.StartTime < e.EndTime
            );
        }

        public void AddEvent(EventCreateDto dto)
        {
            var newEvent = new Event.Domain.Models.Event(dto.StartTime)
            {
                Title = dto.Title,
                Description = dto.Description,
                Location = dto.Location,
                Attendees = dto.Attendees,
                EventType = dto.EventType,
                StartTime = dto.StartTime,
            };

            if(HasConflict(newEvent))
            {
                throw new InvalidOperationException("Event conflicts with an existing events at the same time and location");
            }
            _events[newEvent.Id] = newEvent;
        }

        public bool DeleteEvent(Guid id)
        {
            return _events.Remove(id);
        }

        public IEnumerable<Event.Domain.Models.Event> GetAllEvent()
        {
            var events = _events.Values;
            if(!events.Any())
            {
                throw new Exception("Events not found");
            }
            return events;
            
        }

        public Event.Domain.Models.Event GetEventById(Guid id)
        {
           if (!_events.TryGetValue(id, out var newEvent))
            {
                throw new KeyNotFoundException($"Event with the ID: {id} not found");
            }
           return newEvent;
        }

        public IEnumerable<Event.Domain.Models.Event> GetEventsByDate(DateTime date)
        {
            return _events.Values
                .Where(e=>e.StartTime.Date == date.Date)
                .ToList();
        }

        public IEnumerable<Event.Domain.Models.Event> GetEventsInRanges(DateTime start, DateTime end)
        {
            if (start > end)
                throw new ArgumentException("Start date must be earlier than end date");

            return _events.Values
                .Where (e => e.StartTime >= start && e.StartTime <= end)
                .ToList ();
        }

        public void UpdateEvent(Guid id, EventUpdateDto dto)
        {
            if(!_events.TryGetValue(id, out var events))
            {
                throw new KeyNotFoundException($"Event with the ID: {id} not found");
            }

            var updatedEvent = new Event.Domain.Models.Event(dto.StartTime)
            {
                Title = dto.Title,
                Description = dto.Description,
                Location = dto.Location,
                Attendees = dto.Attendees,
                EventType = dto.EventType,
                StartTime = dto.StartTime,
            };

            if (HasConflict(updatedEvent))
            {
                throw new InvalidOperationException("Event conflicts with an existing events at the same time and location");
            }
            _events[id] = updatedEvent;
       
        }
    }
 }

