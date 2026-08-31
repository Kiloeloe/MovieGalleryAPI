using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieGalleryAPI.Data;
using MovieGalleryAPI.DTO.Response;
using MovieGalleryAPI.DTO.Request;
using MovieGalleryAPI.Model;

namespace MovieGalleryAPI.Services.Auth;

public class AuthService: IAuthService
{
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly PasswordHasher<User> _passwordHasher = new();

    public AuthService(AppDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequestDTO request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == request.Username);

        if (user is null)
            return null;

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (result == PasswordVerificationResult.Failed)
            return null;

        var (token, expiresAtUtc) = _tokenService.GenerateToken(user);

        return new LoginResponse
        {
            Token = token,
            ExpiresAtUTC = expiresAtUtc,
            Username = user.Username
        };
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterRequestDTO request)
    {
        var normalizedUsername = request.Username.Trim();

        var usernameTaken = await _context.Users
            .AnyAsync(u => u.Username == normalizedUsername);

        if (usernameTaken)
        {
            return new RegisterResponse
            {
                Success = false,
                ErrorMessage = "That username is already taken."
            };
        }

        var user = new User { Username = normalizedUsername };
        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var (token, expiresAtUtc) = _tokenService.GenerateToken(user);

        return new RegisterResponse
        {
            Success = true,
            Response = new LoginResponse
            {
                Token = token,
                ExpiresAtUTC = expiresAtUtc,
                Username = user.Username
            }
        };
    }
}
