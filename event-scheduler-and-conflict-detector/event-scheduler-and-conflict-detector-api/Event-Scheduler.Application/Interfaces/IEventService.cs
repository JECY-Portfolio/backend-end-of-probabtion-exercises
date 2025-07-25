using event_scheduler_and_conflict_detector_api.Event.Application.Dtos;
using event_scheduler_and_conflict_detector_api.Event.Domain.Models;

namespace event_scheduler_and_conflict_detector_api.Event.Application.Interfaces
{
    public interface IEventService
    {
        IEnumerable<Domain.Models.Event> GetAllEvent();
        Domain.Models.Event GetEventById(Guid id);
        IEnumerable<Domain.Models.Event> GetEventsByDate(DateTime date);
        IEnumerable<Domain.Models.Event> GetEventsInRanges(DateTime start, DateTime end);
        void AddEvent(EventCreateDto dto);
        void UpdateEvent(Guid id, EventUpdateDto dto);
        bool DeleteEvent(Guid id);
    }
}
