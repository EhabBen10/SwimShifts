using Google.Apis.Calendar.v3.Data;
using Shifts.Application.Models;

public interface IGoogleCalendarService
{
    Task CreateEvents(List<Event> shifts);
    List<Event> events(List<Shift> shifts);
}
