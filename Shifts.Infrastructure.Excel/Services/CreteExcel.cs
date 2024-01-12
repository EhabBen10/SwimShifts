using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using Shifts.Application.Interfaces;
using Shifts.Application.Models;

namespace Shifts.Infrastructure.Excel.Services;
public class CreteExcel : ICreateExcel
{
    private readonly IApplicationDbContext applicationDbContext;

    public CreteExcel(IApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public async Task<byte[]> CreateWaterSampleExcel(DateTime startDate, DateTime endDate)
    {
        var workbook = new XLWorkbook();
        startDate = startDate.Date;
        endDate = endDate.Date.AddDays(1).AddTicks(-1);

        var currentMonth = new DateTime(startDate.Year, startDate.Month, 1);
        var employees = await applicationDbContext.Employees
            .Include(e => e.WaterSamples)
            .Where(e => e.WaterSamples.Any(ws => ws.WaterSampleTime >= startDate && ws.WaterSampleTime <= endDate))
            .ToListAsync();
        var worksheet = workbook.Worksheets.Add(currentMonth.ToString("MMMM"));
        var currentRow = 2;
        List<Tuple<WaterSample, string>> waterSamples = new List<Tuple<WaterSample, string>>();

        foreach (Employee employee in employees)
        {
            if (employee.Name != null)
            {
                var employeeWaterSamples = employee.WaterSamples?
                    .Where(ws => ws.WaterSampleTime >= startDate && ws.WaterSampleTime <= endDate)
                    .Select(ws => new Tuple<WaterSample, string>(ws, employee.Name));

                waterSamples.AddRange(employeeWaterSamples);
            }

        }

        waterSamples = waterSamples.OrderBy(ws => ws.Item1.WaterSampleTime).ToList();

        foreach (var watersample in waterSamples)
        {
            if (currentMonth.Month != watersample.Item1.WaterSampleTime.Value.Month)
            {
                currentMonth = watersample.Item1.WaterSampleTime.Value;
                worksheet = workbook.Worksheets.Add(currentMonth.ToString("MMMM"));
                currentRow = 2;
            }
            worksheet.Cell(currentRow, 1).Value = watersample.Item1.FritKlor;
            worksheet.Cell(currentRow, 2).Value = watersample.Item1.Bundklor;
            worksheet.Cell(currentRow, 3).Value = watersample.Item1.Differace;
            worksheet.Cell(currentRow, 4).Value = watersample.Item1.Ph;
            worksheet.Cell(currentRow, 5).Value = watersample.Item1.AutoFritKlor;
            worksheet.Cell(currentRow, 6).Value = watersample.Item1.AutoPH;
            worksheet.Cell(currentRow, 7).Value = watersample.Item1.WaterSampleTime.Value.ToString("dd/MM/yyyy");
            worksheet.Cell(currentRow, 8).Value = watersample.Item1.WaterSampleTime.Value.ToString("HH:mm");
            worksheet.Cell(currentRow, 9).Value = watersample.Item2;

            currentRow++;
        }
        foreach (var worksheet1 in workbook.Worksheets)
        {
            worksheet1.Cell(1, 1).Value = "FritKlor";
            worksheet1.Cell(1, 2).Value = "Bundklor";
            worksheet1.Cell(1, 3).Value = "Differace";
            worksheet1.Cell(1, 4).Value = "Ph";
            worksheet1.Cell(1, 5).Value = "AutoFritKlor";
            worksheet1.Cell(1, 6).Value = "AutoPH";
            worksheet1.Cell(1, 7).Value = "Date";
            worksheet1.Cell(1, 8).Value = "Time";
            worksheet1.Cell(1, 9).Value = "Name";
        }
        using (var stream = new MemoryStream())
        {
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return content;
        }
    }


}
