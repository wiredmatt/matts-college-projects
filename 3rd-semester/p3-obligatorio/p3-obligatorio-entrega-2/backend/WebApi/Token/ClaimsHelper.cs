using System.Security.Claims;

namespace WebApi.Token;

public class DatosToken
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}

public static class ClaimsHelper
{
    public static DatosToken GetTokenClaims(HttpContext httpContext)
    {
        if (httpContext.User == null)
            throw new UnauthorizedAccessException("El token no es válido");

        var userIdClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        var emailClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
        var roleClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

        if (userIdClaim == null || emailClaim == null || roleClaim == null)
        {
            throw new UnauthorizedAccessException("El token fue malformado");
        }

        return new DatosToken
        {
            UserId = userIdClaim.Value,
            Email = emailClaim.Value,
            Role = roleClaim.Value
        };
    }
}