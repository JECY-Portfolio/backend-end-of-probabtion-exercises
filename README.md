# Event Scheduler and Conflict Detector

This Project ia an ASP.Net core Web Api that allows users to:
1. Schedule events by specifying title, description, date etc.
2. Detect time conflicts between events
3. Perform CRUD operations (Create, Read, Update and Deleted)
4. Store events in memory

# Features
1. Add new event
2. Update an existing event
3. Get all events or a single event
4. Detect conflicts when events overlaps
5. Delete an event
6. Uses DTOs(Data Transfer Object) to simplify data exchange
7. Clean seperation of concerns using Interfaces and Services

# How to Model Recurring Events
Recurring events are events that repeat overtime, like daily tasks or weekely meetings.
You can set how often the event reports - daily, weekly, or monthly.
You can also choose how many times it should repeat.

To model recurring event, add a RecurrentType(e.g, daily, monthly) and an optional RepeatCount property to the event, so the system knows how many times the event should repeat.

# Technologies used
 C#, Asp.Net Web Core API, Visual studio

