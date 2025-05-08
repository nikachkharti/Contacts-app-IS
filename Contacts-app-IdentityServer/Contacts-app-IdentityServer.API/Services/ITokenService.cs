namespace Contacts_app_IdentityServer.API.Services
{
    public interface ITokenService
    {
        /// <summary>
        /// Get access token
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        Task<string> GetAccessTokenAsync(string username, string password);

        /// <summary>
        /// Get refresh token
        /// </summary>
        /// <param name="refreshToken">Refresh token</param>
        /// <returns></returns>
        Task<string> RefreshTokenAsync(string refreshToken);
    }
}
