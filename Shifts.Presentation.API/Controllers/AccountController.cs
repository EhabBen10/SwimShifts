using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace Shifts.Presentation.API.Controllers;

[ApiController]
[Microsoft.AspNetCore.Components.Route("[controller]")]
public class AccountController : Controller
{
    [HttpGet("login")]
    public IActionResult Login(string returnUrl)
    {
        return new ChallengeResult(
            GoogleDefaults.AuthenticationScheme,
            new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(LoginCallback), new { returnUrl })
            });
    }

    [HttpGet("logincallback")]
    public async Task<IActionResult> LoginCallback(string returnUrl)
    {
        var authenticateResult = await HttpContext.AuthenticateAsync("External");

        if (!authenticateResult.Succeeded)
            return BadRequest(); // TODO: Handle this better.

        var claimsIdentity = new ClaimsIdentity("Application");

        claimsIdentity.AddClaim(authenticateResult.Principal.FindFirst(ClaimTypes.NameIdentifier));
        claimsIdentity.AddClaim(authenticateResult.Principal.FindFirst(ClaimTypes.Email));

        await HttpContext.SignInAsync(
            "Application",
            new ClaimsPrincipal(claimsIdentity));

        return Redirect(returnUrl);
    }
}