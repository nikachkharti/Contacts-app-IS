using Contacts_app_IdentityServer.API.Models;
using Contacts_app_IdentityServer.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Contacts_app_IdentityServer.API.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController(ITokenService tokenService) : ControllerBase
    {
        /// <summary>
        /// Get access token for admin, using keycloack
        /// </summary>
        /// <param name="request">Access token request model</param>
        /// <returns>IActionResult</returns>
        [HttpPost("admin")]
        public async Task<IActionResult> GetAdminAccessToken([FromBody] AccessTokenRequestDto request)
        {
            var result = await tokenService.GetAccessTokenAsync(request.UserName, request.Password);
            return Content(result, "application/json");
        }

        /// <summary>
        /// Get refresh token, using keycloack
        /// </summary>
        /// <param name="request">Refresh token request model</param>
        /// <returns>IActionResult</returns>
        [HttpPost("refresh")]
        public async Task<IActionResult> GetRefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            var result = await tokenService.RefreshTokenAsync(request.RefreshToken);
            return Content(result, "application/json");
        }
    }
}
