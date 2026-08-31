namespace MovieGalleryAPI.DTO.Response;

public class LoginResponse
{
    public string Token { get; set; }
    public DateTime ExpiresAtUTC { get; set; }
    public string Username { get; set; }
}
