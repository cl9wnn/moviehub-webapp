using System.Security.Claims;

namespace API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid? GetUserId(this ClaimsPrincipal user)
    {
        var currentUserIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(currentUserIdString, out Guid userId) ? userId : null;
    }
}