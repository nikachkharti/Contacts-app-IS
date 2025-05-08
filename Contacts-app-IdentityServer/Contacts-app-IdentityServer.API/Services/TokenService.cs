namespace Contacts_app_IdentityServer.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly HttpClient _httpClient;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _tokenEndpoint;

        public TokenService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _tokenEndpoint = configuration["Keycloak:TokenEndpoint"];
            _clientId = configuration["Keycloak:ClientId"];
            _clientSecret = configuration["Keycloak:ClientSecret"];
        }

        public async Task<string> GetAccessTokenAsync(string username, string password)
        {
            var data = new Dictionary<string, string>
            {
                ["grant_type"] = "password",
                ["client_id"] = _clientId,
                ["client_secret"] = _clientSecret,
                ["username"] = username,
                ["password"] = password
            };

            var response = await _httpClient.PostAsync(_tokenEndpoint, new FormUrlEncodedContent(data));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> RefreshTokenAsync(string refreshToken)
        {
            var data = new Dictionary<string, string>
            {
                ["grant_type"] = "refresh_token",
                ["client_id"] = _clientId,
                ["client_secret"] = _clientSecret,
                ["refresh_token"] = refreshToken
            };

            var response = await _httpClient.PostAsync(_tokenEndpoint, new FormUrlEncodedContent(data));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
