namespace SSMS.Domain.ConfigOptions
{
    public class KeycloakSettings
    {
        public string AuthServerUrl { get; set; }
        public string Realm {  get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AddminClientId { get; set; }
        public string AdminClientSecret { get; set; }
    }
}
