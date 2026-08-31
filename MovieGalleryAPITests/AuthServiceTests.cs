using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Moq;
using MovieGalleryAPI.DTO.Request;
using MovieGalleryAPI.Model;
using MovieGalleryAPI.Services.Auth;

using Xunit;

namespace MovieGalleryAPI.Tests;

public class AuthServiceTests
{
    private readonly Mock<ITokenService> _tokenServiceMock = new();
    //mocking jwt assigning logic

    [Fact]
    public async Task LoginAsync_WithCorrectCredentials_ReturnsToken()
    {
        using var context = TestDbContextFactory.Create();

        var hasher = new PasswordHasher<User>();
        var user = context.Users.First(u => u.Username == "demo");
        user.PasswordHash = hasher.HashPassword(user, "Demo123!");
        await context.SaveChangesAsync();

        _tokenServiceMock
            .Setup(t => t.GenerateToken(It.IsAny<User>()))
            .Returns(("fake-jwt-token", DateTime.UtcNow.AddHours(1)));

        var service = new AuthService(context, _tokenServiceMock.Object);

        var result = await service.LoginAsync(new LoginRequestDTO { Username = "demo", Password = "Demo123!" });

        result.Should().NotBeNull();
        result!.Token.Should().Be("fake-jwt-token");
        result.Username.Should().Be("demo");
    }

    //test invalid login password
    [Fact]
    public async Task LoginAsync_WithWrongPassword_ReturnsNull()
    {
        using var context = TestDbContextFactory.Create();
        var hasher = new PasswordHasher<User>();
        var user = context.Users.First(u => u.Username == "demo");
        user.PasswordHash = hasher.HashPassword(user, "Demo123!");
        await context.SaveChangesAsync();

        var service = new AuthService(context, _tokenServiceMock.Object);

        var result = await service.LoginAsync(new LoginRequestDTO { Username = "demo", Password = "WrongPassword" });

        result.Should().BeNull();
        _tokenServiceMock.Verify(t => t.GenerateToken(It.IsAny<User>()), Times.Never);
    }

    //test invalid username
    [Fact]
    public async Task LoginAsync_WithUnknownUsername_ReturnsNull()
    {
        using var context = TestDbContextFactory.Create();
        var service = new AuthService(context, _tokenServiceMock.Object);

        var result = await service.LoginAsync(new LoginRequestDTO { Username = "nobody", Password = "whatever" });

        result.Should().BeNull();
    }

    //test sign up wiht existing username
    [Fact]
    public async Task RegisterAsync_WithNewUsername_CreatesUserAndReturnsToken()
    {
        using var context = TestDbContextFactory.Create();
        _tokenServiceMock
            .Setup(t => t.GenerateToken(It.IsAny<User>()))
            .Returns(("fake-jwt-token", DateTime.UtcNow.AddHours(1)));

        var service = new AuthService(context, _tokenServiceMock.Object);

        var result = await service.RegisterAsync(new RegisterRequestDTO { Username = "newuser", Password = "Password1" });

        result.Success.Should().BeTrue();
        result.Response!.Token.Should().Be("fake-jwt-token");
        context.Users.Should().ContainSingle(u => u.Username == "newuser");
    }

    //testing chech duplicate username before any write
    [Fact]
    public async Task RegisterAsync_WithExistingUsername_ReturnsFailureAndDoesNotDuplicate()
    {
        using var context = TestDbContextFactory.Create();
        var service = new AuthService(context, _tokenServiceMock.Object);

        var result = await service.RegisterAsync(new RegisterRequestDTO { Username = "demo", Password = "Password1" });

        result.Success.Should().BeFalse();
        result.ErrorMessage.Should().NotBeNullOrEmpty();
        context.Users.Count(u => u.Username == "demo").Should().Be(1);
        _tokenServiceMock.Verify(t => t.GenerateToken(It.IsAny<User>()), Times.Never);
    }
}
