using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Shifts.Application.Models;

namespace Shifts.Application.Interfaces
{
    public interface IGoogleSheetsService
    {
         IList<IList<object>> ReadDataFromGoogleSheet(UserCredential credential);

        List<Shift> FindShifts(IList<IList<object>> values, string nameToSearch);

    }
}
