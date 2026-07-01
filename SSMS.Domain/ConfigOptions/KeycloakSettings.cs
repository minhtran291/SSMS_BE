namespace SSMS.Domain.ConfigOptions
{
    public class KeycloakSettings
    {
        public string AuthServerUrl { get; set; } = string.Empty;
        public string Realm {  get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string AddminClientId { get; set; } = string.Empty;
        public string AdminClientSecret { get; set; } = string.Empty;
    }
}
