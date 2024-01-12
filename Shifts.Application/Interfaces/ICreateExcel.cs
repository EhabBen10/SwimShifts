using ClosedXML.Excel;

namespace Shifts.Application.Interfaces;
public interface ICreateExcel
{
    Task<byte[]> CreateWaterSampleExcel(DateTime startDate, DateTime endDate);
}
