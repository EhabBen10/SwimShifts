using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Options;
using Shifts.Application.Interfaces;

namespace Shifts.Infrastructure.GoogleAuth;
public class GoogleAuthService : IGoogleAuthService
{
    private GoogleSheetsConfig _googleSheetsConfig;

    public GoogleAuthService(IOptions<GoogleSheetsConfig> googleSheetsConfig)
    {
        _googleSheetsConfig = googleSheetsConfig.Value;
    }

    public async Task<UserCredential> AuthorizeAsync()
    {
        string[] scopes = { SheetsService.Scope.Drive, CalendarService.Scope.Calendar };
        var fileDataStore = new FileDataStore("Google.Apis.Auth", fullPath: false);
        UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets
                    {
                        ClientId = _googleSheetsConfig.client_id,
                        ClientSecret = _googleSheetsConfig.client_secret
                    },
                    scopes,
                    "user",
                    CancellationToken.None, fileDataStore);

        return credential;
    }

}