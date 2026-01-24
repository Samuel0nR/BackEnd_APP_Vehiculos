namespace API_VehiclesAPP.Configurations
{
    public class JwtConfig
    {
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Issuer { get; set; } = "My-Mobi-App";
        public string Audience { get; set; } = "My-Mobi-App";
    }
}
