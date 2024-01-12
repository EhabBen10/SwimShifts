using Shifts.Application.Models;

public interface IExportToDB
{
    Task<Employee> ExportWaterSamples(string name, string imgUrl, decimal? fritKlor, decimal? bundklor, decimal? differace, decimal? ph, decimal? autoFritKlor, decimal? autoPH, DateTime? waterSampleTime);
}