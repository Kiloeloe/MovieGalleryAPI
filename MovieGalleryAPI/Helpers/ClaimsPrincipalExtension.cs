using System.Security.Claims;

namespace MovieGalleryAPI.Helpers;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal user)
    {
        var idClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idClaim is null || !int.TryParse(idClaim, out var id))
            throw new InvalidOperationException("User id claim is missing or invalid.");

        return id;
    }
}
