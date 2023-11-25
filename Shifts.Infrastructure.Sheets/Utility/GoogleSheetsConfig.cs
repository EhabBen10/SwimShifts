using System.Text.Json.Serialization;

public class GoogleSheetsConfig
{
    [JsonPropertyName("installed")]
    public InstalledConfig? Installed { get; set; }

    public class InstalledConfig
    {
        [JsonPropertyName("client_id")]
        public string? ClientId { get; set; }

        [JsonPropertyName("project_id")]
        public string? ProjectId { get; set; }

        [JsonPropertyName("auth_uri")]
        public string? AuthUri { get; set; }

        [JsonPropertyName("token_uri")]
        public string? TokenUri { get; set; }

        [JsonPropertyName("auth_provider_x509_cert_url")]
        public string? AuthProviderX509CertUrl { get; set; }

        [JsonPropertyName("client_secret")]
        public string? ClientSecret { get; set; }

        [JsonPropertyName("redirect_uris")]
        public List<string>? RedirectUris { get; set; }
    }
}