using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Shifts.Application.Models;

public interface IGoogleCalendarService
{
    Task CreateEvents(List<Event> shifts, UserCredential credential);
    List<Event> events(List<Shift> shifts, string imgUrl);
}
