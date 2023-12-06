using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Microsoft.AspNetCore.Mvc;
using Shifts.Application.Interfaces;
using Shifts.Application.Models;

namespace Shifts.Presentation.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ShiftsController : ControllerBase
{
    public IGoogleSheetsService _googleSheetsService;
    public IGoogleCalendarService _googleCalendarService;
    public IGoogleAuthService _googleAuthService;

    public ShiftsController(IGoogleSheetsService googleSheetsService, IGoogleCalendarService googleCalendarService, IGoogleAuthService googleAuthService)
    {
        _googleSheetsService = googleSheetsService;
        _googleCalendarService = googleCalendarService;
        _googleAuthService = googleAuthService;
    }

    [HttpGet(Name = "getshifts")]
    public async Task<List<Shift>> Get()
    {
        UserCredential credential = await _googleAuthService.AuthorizeAsync();
        IList<IList<object>> objects = _googleSheetsService.ReadDataFromGoogleSheet(credential);
        List<Shift> shifts =  _googleSheetsService.FindShifts(objects,"Ehab");
        List<Event> events =  _googleCalendarService.events(shifts);
        await _googleCalendarService.CreateEvents(events, credential);
        return null;
    }
}
