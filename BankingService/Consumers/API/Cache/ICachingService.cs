namespace API.Cache;

public interface ICachingService
{
    Task<bool> SetTokenAsync(string key, string value);

    Task<string?> GetTokenAsync(string key);
}