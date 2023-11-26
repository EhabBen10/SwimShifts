using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Shifts.Application.Models;

namespace Shifts.Application.Interfaces
{
    public interface IGoogleSheetsService
    {
         Task<IList<IList<object>>> ReadDataFromGoogleSheet();

        List<Shift> FindShifts(IList<IList<object>> values, string nameToSearch);

    }
}
