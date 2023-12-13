using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Shifts.Application.Interfaces;
using Shifts.Application.Models;

namespace Shifts.Presentation.GraphQL.Queries;

[ExtendObjectType("Query")]
public class EventsQuery
{
    public async Task<List<Event>> Get(IGoogleSheetsService _googleSheetsService, IGoogleCalendarService _googleCalendarService, IGoogleAuthService _googleAuthService)
    {
        UserCredential credential = await _googleAuthService.AuthorizeAsync();
        IList<IList<object>> objects = _googleSheetsService.ReadDataFromGoogleSheet(credential);
        List<Shift> shifts =  _googleSheetsService.FindShifts(objects,"Ehab");
        List<Event> events =  _googleCalendarService.events(shifts);
        return events;

    }
}