using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Shifts.Application.Interfaces;

namespace Shifts.Presentation.API.Controllers;

[ApiController]
[Microsoft.AspNetCore.Components.Route("[controller]")]
public class FileController : ControllerBase
{
    public ICreateExcel _createExcel;

    public FileController(ICreateExcel createExcel)
    {
        _createExcel = createExcel;
    }
    [HttpGet("download")]
    public async Task<FileResult> Download(CancellationToken ct, DateTime startDate, DateTime endDate)
    {
        var file = await _createExcel.CreateWaterSampleExcel(startDate, endDate);

        return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{startDate}-{endDate}.xlsx");
    }


}