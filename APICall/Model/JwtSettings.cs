namespace APICall.Model
{
    public class JwtSettings : IJwtSettings
    {
        public JwtSettings()
        {
        }
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int MinutesToExpiration { get; set; }
    }
}
