using MovieGalleryAPI.DTO.Response;
namespace MovieGalleryAPI.DTO.Response;

public class RegisterResponse
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public LoginResponse? Response { get; set; }
}
