using System.ComponentModel.DataAnnotations;

namespace Shifts.Application.Models;

public class Employee
{
    [Key]
    public int EmployeeId { get; set; }
    public string? Name { get; set; }
    public string? imgUrl { get; set; }
    public ICollection<WaterSample>? WaterSamples { get; set; }
}
