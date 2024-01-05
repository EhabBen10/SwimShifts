using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Shifts.Application.Models;

namespace Shifts.Presentation.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class ImportToCalender
    {
        public async Task<bool> ImportToGoogleCalenderAsync(IGoogleCalendarService _googleCalendarService, IGoogleAuthService _googleAuthService, List<Shift> shifts)
        {
            UserCredential credential = await _googleAuthService.AuthorizeAsync();
            List<Event> events = _googleCalendarService.events(shifts, null);
            await _googleCalendarService.CreateEvents(events, credential);
            return true;
        }
    }
}
