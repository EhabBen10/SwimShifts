namespace Shifts.Infrastructure.Sheets.Utility
{
    public class GoogleSheetsConfig
    {
        public Installed Installed { get; set; }
    }

    public class Installed
    {
        public string? ClientId { get; set; }
        public string? ProjectId { get; set; }
        public string? AuthUri { get; set; }
        public string? TokenUri { get; set; }
        public string? AuthProviderX509CertUrl { get; set; }
        public string? ClientSecret { get; set; }
        public string[]? RedirectUris { get; set; }
    }
}
