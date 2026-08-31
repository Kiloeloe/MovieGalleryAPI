using MovieGalleryAPI.Model;

namespace MovieGalleryAPI.Services.Auth;

public interface ITokenService
{
    (string token, DateTime expiresAtUtc) GenerateToken(User user);
}
