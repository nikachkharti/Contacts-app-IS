namespace Contacts_app_IdentityServer.API.Services
{
    public class TokenService(HttpClient httpClient, IConfiguration configuration) : ITokenService
    {
        public async Task<string> GetAccessTokenAsync(string username, string password)
        {
            var tokenEndpoint = configuration["Keycloak:TokenEndpoint"];

            var data = new Dictionary<string, string>
            {
                ["grant_type"] = "password", //Better to use client credentials instead of password
                ["client_id"] = configuration["Keycloak:ClientId"],
                ["client_secret"] = configuration["Keycloak:ClientSecret"],
                ["username"] = username,
                ["password"] = password
            };

            var response = await httpClient.PostAsync(tokenEndpoint, new FormUrlEncodedContent(data));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> RefreshTokenAsync(string refreshToken)
        {
            var tokenEndpoint = configuration["Keycloak:TokenEndpoint"];

            var data = new Dictionary<string, string>
            {
                ["grant_type"] = "refresh_token",
                ["client_id"] = configuration["Keycloak:ClientId"],
                ["client_secret"] = configuration["Keycloak:ClientSecret"],
                ["refresh_token"] = refreshToken
            };

            var response = await httpClient.PostAsync(tokenEndpoint, new FormUrlEncodedContent(data));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
