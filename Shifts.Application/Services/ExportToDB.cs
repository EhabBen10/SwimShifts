using Shifts.Application.Models;
using Shifts.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Shifts.Application.Services
{
    public class ExportToDB : IExportToDB
    {
        private readonly IApplicationDbContext _context;

        public ExportToDB(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> ExportWaterSamples(string name, string imgUrl, decimal? fritKlor, decimal? bundklor, decimal? differace, decimal? ph, decimal? autoFritKlor, decimal? autoPH, DateTime? waterSampleTime)
        {
            Employee? employeeInDb = await _context.Employees
                    .Include(e => e.WaterSamples)
                    .FirstOrDefaultAsync(e => e.Name == name);

            if (employeeInDb == null)
            {
                employeeInDb = new Employee
                {
                    Name = name,
                    imgUrl = imgUrl,
                    WaterSamples = new List<WaterSample>()
                };
                _context.Employees.Add(employeeInDb);
            }

            var waterSample = new WaterSample
            {
                FritKlor = fritKlor,
                Bundklor = bundklor,
                Differace = differace,
                Ph = ph,
                AutoFritKlor = autoFritKlor,
                AutoPH = autoPH,
                WaterSampleTime = waterSampleTime,
            };

            employeeInDb.WaterSamples.Add(waterSample);

            await _context.SaveChangesAsync();

            return employeeInDb;
        }
    }
}