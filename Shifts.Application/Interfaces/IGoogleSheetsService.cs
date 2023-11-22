using Google.Apis.Auth.OAuth2;
using Shifts.Application.Models;

namespace Shifts.Application.Interfaces
{
    public interface IGoogleSheetsService
    {
        IList<IList<object>> ReadDataFromGoogleSheet(UserCredential credential);

        List<Shift> Findvagter(IList<IList<object>> values, string nameToSearch);

    }
}
