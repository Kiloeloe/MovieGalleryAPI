# MovieGalleryAPI
ASP.NET Core Web API (.NET 10) built for the "Movie Gallery" take-home test.

## What's here

| Requirement | Where |
|---|---|
| .NET + EF Core | `src/MovieGalleryAPI`, `Data/AppDbContext.cs` |
| SQL Server | `appsettings.json` connection string |
| Swagger | enabled in `Program.cs`, with a Bearer token box |
| Unit tests | `src/MovieGalleryAPI.Tests` (xUnit + Moq + FluentAssertions) |
| Bearer auth on all APIs | `[Authorize]` on `MoviesController` / `FavoritesController`, JWT from `AuthController` |
| 6 APIs (3 GET / 3 POST) | see table below |

### The 6 required endpoints

| Method | Route | Purpose |
|---|---|---|
| POST | `/api/auth/login` | Authenticate, returns JWT |
| GET | `/api/movies/popular` | Popular movies list |
| GET | `/api/movies/search?keyword=` | Search movies |
| GET | `/api/movies/{id}` | Movie details |
| POST | `/api/favorites/{movieId}` | Add to favorites |
| POST | `/api/favorites/{movieId}/remove` | Remove from favorites |


## Prerequisites

- Visual Studio 2026 (18.9.2 was the version used to develop this) with the **ASP.NET and web development** workload
- .NET 10 SDK (if VS doesn't have it yet, install from https://dotnet.microsoft.com)
- SQL Server (LocalDB, Express, or full) + SQL Server Management Studio



## Setup

1. **Open** `MovieGalleryAPI.sln` in Visual Studio.
2. **Restore NuGet packages** (VS does this automatically on open; otherwise right-click
   the solution → *Restore NuGet Packages*). If a package version listed in the `.csproj`
   isn't available, bump it to the latest 9.x/10.x via the NuGet Package Manager — the
   APIs used haven't changed across those minor versions.
3. **Update the connection string** in `appsettings.json` to match your SQL Server
   instance, e.g.:
   ```
   Server=(localdb)\\SQLSERVER;Database=MovieGalleryDb;Trusted_Connection=True;TrustServerCertificate=True;
   ```
4. **Create the initial migration** (Package Manager Console, default project =
   `MovieGalleryAPI`):
   ```powershell
   Add-Migration InitialCreate
   ```
   (The app also calls `context.Database.Migrate()` on startup, so once the migration
   exists you don't need to run `Update-Database` manually — just hit Run.)
5. **Run** the project (F5). Swagger opens at `https://localhost:<port>/swagger`.
6. **Log in** via `POST /api/auth/login` with the seeded demo account:
   ```json
   { "username": "demo", "password": "Demo123!" }
   ```
   Copy the returned `token`, click **Authorize** in Swagger, paste it in (no need to
   type "Bearer " — Swagger adds that), and you can now call the protected endpoints.



## Running the tests

In Visual Studio: **Test → Run All Tests** (Test Explorer), or from the CLI:
```powershell
dotnet test
```

The test project uses EF Core's **InMemory** provider, so it needs no real database —
tests are fast and fully isolated from each other.
