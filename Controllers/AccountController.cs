using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project.Services;
using System.Security.Claims;
using System.Threading.Tasks;

[Route("account")]
public class AccountController : Controller
{
    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("google-login")]
    public IActionResult GoogleLogin()
    {
        var redirectUrl = Url.Action("GoogleResponse");
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("google-response")]
    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (result?.Principal != null)
        {
            var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new Claim(claim.Type, claim.Value)).ToList();

            // Log received claims
            System.Console.WriteLine("Claims:");
            foreach (var claim in claims)
            {
                System.Console.WriteLine($"{claim.Type}: {claim.Value}");
            }

            var token = TokenService.GetToken(claims);
            var tokenString = TokenService.WriteToken(token);

            // Log generated token
            System.Console.WriteLine($"Generated Token: {tokenString}");

            Response.Cookies.Append("token", tokenString, new CookieOptions { HttpOnly = true });

            return Redirect("/index.html");
        }

        System.Console.WriteLine("Authentication failed");
        return Redirect("/login.html");
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/login.html");
    }
}
