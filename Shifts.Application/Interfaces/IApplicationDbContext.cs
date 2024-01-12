using Microsoft.EntityFrameworkCore;
using Shifts.Application.Models;

namespace Shifts.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<WaterSample> WaterSamples { get; set; }

    DbSet<Employee> Employees { get; set; }


    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}