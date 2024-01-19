using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Microsoft.AspNetCore.Authorization;
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

    public ICreateExcel _createExcel;

    public ShiftsController(IGoogleSheetsService googleSheetsService, IGoogleCalendarService googleCalendarService, IGoogleAuthService googleAuthService, ICreateExcel createExcel)
    {
        _googleSheetsService = googleSheetsService;
        _googleCalendarService = googleCalendarService;
        _googleAuthService = googleAuthService;
        _createExcel = createExcel;
    }

    [HttpGet(Name = "getshifts")]
    public async Task<List<Shift>> Get()
    {
        UserCredential credential = await _googleAuthService.AuthorizeAsync();
        IList<IList<object>> objects = _googleSheetsService.ReadDataFromGoogleSheet(credential);
        List<Shift> shifts = _googleSheetsService.FindShifts(objects, "Ehab");
        List<Event> events = _googleCalendarService.events(shifts, "fe");
        await _googleCalendarService.CreateEvents(events, credential);
        return null;
    }
    [Authorize]
    [HttpGet("download")]
    public async Task<FileResult> Download(CancellationToken ct)
    {
        var file = await _createExcel.CreateWaterSampleExcel(DateTime.Now, DateTime.Now);

        return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "products.xlsx");
    }
}
