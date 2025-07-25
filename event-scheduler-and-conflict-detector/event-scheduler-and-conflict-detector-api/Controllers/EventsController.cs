using System.Reflection;
using event_scheduler_and_conflict_detector_api.Event.Application.Dtos;
using event_scheduler_and_conflict_detector_api.Event.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace event_scheduler_and_conflict_detector_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("get-all-events")]
        public ActionResult GetAllEVent()
        {
            try
            {
                var events = _eventService.GetAllEvent();
                return Ok(events);
            }
            catch (Exception ex) 
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetEventById(Guid id)
        {
            try
            {
                var events = _eventService.GetEventById(id);
                return Ok(events);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("date")]
        public ActionResult GetEventsByDate(DateTime date)
        {
            
            var events = _eventService.GetEventsByDate(date);

            if(!events.Any())
            {
                return NotFound("No event found on the specified date");
            }

            return Ok(events);                     
        }

        [HttpGet("range")]
        public ActionResult GetEventsInRanges(DateTime start, DateTime end)
        {

            var events = _eventService.GetEventsInRanges(start, end);

            if (!events.Any())
            {
                return NotFound("No event found in the specified range");
            }

            return Ok(events);
        }

        [HttpPost("add-an-event")]
        public IActionResult AddEvent(EventCreateDto dto)
        {
            try
            {
                _eventService.AddEvent(dto);
                return Ok("Event added");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateEvent(Guid id, EventUpdateDto dto)
        {
            try
            {
                _eventService.UpdateEvent(id, dto);
                return Ok("Event updated");
            }

            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteEvent(Guid id)
        {
            var deleted = _eventService.DeleteEvent(id);
            if(!deleted)
            {
                return NotFound($"Event with the ID: {id} not found");
            }

            return Ok ($"Event with the ID: {id} deleted");
        }





    }
}
