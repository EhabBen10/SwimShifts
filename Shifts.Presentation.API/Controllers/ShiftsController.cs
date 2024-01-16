using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
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
    public async Task<IActionResult> Download(IGoogleAuthProvider auth)
    {
        GoogleCredential cred = await auth.GetCredentialAsync();
        var service = new DriveService(new BaseClientService.Initializer
        {
            HttpClientInitializer = cred
        });
        var files = await service.Files.List().ExecuteAsync();
        var fileNames = files.Files.Select(x => x.Name).ToList();
        return Ok(fileNames);
    }
}
