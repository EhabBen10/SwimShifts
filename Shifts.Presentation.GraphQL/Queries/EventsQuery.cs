using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Shifts.Application.Interfaces;
using Shifts.Application.Models;

namespace Shifts.Presentation.GraphQL.Queries;

[ExtendObjectType("Query")]
public class EventsQuery
{
    List<string> names = new List<string>
    {
    "Mathilde",
    "Rasmus",
    "Bo",
    "Camilla",
    "Caroline",
    "Clara",
    "Ehab",
    "Jonas",
    "Linea",
    "Mads",
    "Magnus T",
    "Rudi",
    "Cecilie",
    "Jakob M",
    "Jakob B",
    "Signe",
    "Christina",
    "Halfdan",
    "Kira",
    "Torben",
    "Annette",
    "Ümmühan",
    "Gülseren",
    "Sascha",
    "Mikkel",
    "Kille"
    };
    public async Task<List<Event>> Get(IGoogleSheetsService _googleSheetsService, IGoogleCalendarService _googleCalendarService, IGoogleAuthService _googleAuthService, string employeeName)
    {
        if (employeeName != null)
        {
            UserCredential credential = await _googleAuthService.AuthorizeAsync();
            IList<IList<object>> objects = _googleSheetsService.ReadDataFromGoogleSheet(credential);
            List<Shift> shifts = _googleSheetsService.FindShifts(objects, employeeName);
            List<Event> events = _googleCalendarService.events(shifts);
            return events;
        }
        else
        {
            throw new ArgumentNullException("name can not be null");
        }

    }

    public async Task<List<Event>> GetAllShifts(IGoogleSheetsService _googleSheetsService, IGoogleCalendarService _googleCalendarService, IGoogleAuthService _googleAuthService)
    {
        UserCredential credential = await _googleAuthService.AuthorizeAsync();
        IList<IList<object>> objects = _googleSheetsService.ReadDataFromGoogleSheet(credential);
        List<Shift> shifts = new List<Shift>();
        List<Event> events = new List<Event>();
        foreach (var name in names)
        {
            shifts = _googleSheetsService.FindShifts(objects, name);
            events.AddRange(_googleCalendarService.events(shifts));

        }
        return events;
    }

}