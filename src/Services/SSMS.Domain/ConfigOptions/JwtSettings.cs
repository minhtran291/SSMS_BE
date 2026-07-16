namespace SSMS.Domain.ConfigOptions
{
    public class JwtSettings
    {
        public const string SectionName = "JwtSettings";
        public string Authority { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string MetadataAddress { get; set; } = string.Empty;
        public string JwksUri { get; set; } = string.Empty;
    }
}
