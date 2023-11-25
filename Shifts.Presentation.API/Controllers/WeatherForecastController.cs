using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Microsoft.AspNetCore.Mvc;
using Shifts.Application.Interfaces;
using Shifts.Application.Models;
using Shifts.Infrastructure.Sheets.Services;

namespace Shifts.Presentation.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    public IGoogleSheetsService _googleSheetsService;

    public WeatherForecastController(IGoogleSheetsService googleSheetsService)
    {
        _googleSheetsService = googleSheetsService;
    }


    [HttpGet(Name = "getshifts")]
    public async Task<List<Shift>> Get()
    {
      await _googleSheetsService.ReadDataFromGoogleSheet();
        return null;
    }
}
