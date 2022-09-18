namespace DogBreedsAPI.AuthorizationAuthentication
{
    public class TokenConfiguration
    {
        public string Secret { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpirationtimeInHours { get; set; }
    }
}
