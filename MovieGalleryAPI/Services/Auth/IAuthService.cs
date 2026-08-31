using Microsoft.AspNetCore.Identity.Data;
using MovieGalleryAPI.DTO.Response;
using MovieGalleryAPI.DTO.Request;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequestDTO request);

    Task<RegisterResponse?> RegisterAsync(RegisterRequestDTO request);
}