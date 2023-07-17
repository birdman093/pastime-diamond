using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Auth0.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace SignalRWebpack.Controllers
{
    /// <summary>
    /// Auth0 User State Management -- Access Tokens
    /// <br></br> Limitations: Refresh Tokens Not Implemented
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly int chatId = 1111111;

        public AccountController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Login()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var handler = new JwtSecurityTokenHandler();

            string Name = "";

            if (!string.IsNullOrEmpty(accessToken) && User != null)
            {
                var jwtToken = handler.ReadJwtToken(accessToken);
                var userId = jwtToken.Claims.First(claim => claim.Type == "sub").Value;
                Name = User.Identity.Name;
                string EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                string ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value;
            }

            ViewBag.Username = Name;

            return View("login");
        }

        [HttpGet("/error")]
        public IActionResult Error()
        {
            ViewBag.Username = "ERROR";
            return View("login");
        }

        [HttpPost("check-login")]
        public async Task CheckLogin()
        {
            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            var body = await reader.ReadToEndAsync();
            var data = JsonSerializer.Deserialize<Dictionary<string, string>>(body);

            if (data != null && data.ContainsKey("username") && data["username"] == "YES")
            {
                Response.Redirect($"/chat/{chatId}");
            }
            else
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
        }


        [HttpGet("/account")]
        public async Task Login(string returnUrl = "/")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .WithAudience("https://localhost:7178")
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        [Authorize]
        [HttpGet("/logout")]
        public async Task Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                // Indicate here where Auth0 should redirect the user after a logout.
                // Note that the resulting absolute Uri must be added to the
                // **Allowed Logout URLs** settings for the app.
                .WithRedirectUri("/")
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            return View(new
            {
                Name = User.Identity.Name,
                EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
            });
        }


        /// <summary>
        /// This is just a helper action to enable you to easily see all claims related to a user. It helps when debugging your
        /// application to see the in claims populated from the Auth0 ID Token
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Claims()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
