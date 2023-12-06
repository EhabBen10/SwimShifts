using Google.Apis.Auth.OAuth2;

public interface IGoogleAuthService
{
    Task<UserCredential> AuthorizeAsync();
}