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

    public ShiftsController(IGoogleSheetsService googleSheetsService, IGoogleCalendarService googleCalendarService)
    {
        _googleSheetsService = googleSheetsService;
        _googleCalendarService = googleCalendarService;
    }

    [HttpGet(Name = "getshifts")]
    public async Task<List<Shift>> Get()
    {
        IList<IList<object>> objects = await _googleSheetsService.ReadDataFromGoogleSheet();
        List<Shift> shifts =  _googleSheetsService.FindShifts(objects,"Ehab");
        List<Event> events =  _googleCalendarService.events(shifts);
        //  _googleCalendarService.CreateEvents(events);
        return null;
    }
}
