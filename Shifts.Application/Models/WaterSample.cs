using System.ComponentModel.DataAnnotations;

namespace Shifts.Application.Models;

public class WaterSample
{
    public int Id { get; set; }
    public decimal? FritKlor { get; set; }
    public decimal? Bundklor { get; set; }
    public decimal? Differace { get; set; }
    public decimal? Ph { get; set; }
    public decimal? AutoFritKlor { get; set; }
    public decimal? AutoPH { get; set; }
    public DateTime? WaterSampleTime { get; set; }

}
