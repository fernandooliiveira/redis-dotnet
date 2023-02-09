using System.Globalization;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;

namespace API.Cache;

public class CachingService : ICachingService
{
    private readonly ConnectionMultiplexer _redis = ConnectionMultiplexer.Connect("localhost");
    private readonly IDatabase _db;

    public CachingService()
    {
        _db = _redis.GetDatabase();
    }

    public async Task<bool> SetTokenAsync(string key, string value)
    {
        TimeSpan expiredDate = DateTime.Now.Add(TimeSpan.FromSeconds(60)).TimeOfDay;
        Console.WriteLine(expiredDate);
        var isSet = await _db.StringSetAsync(
            key,
            value,
            expiredDate
        );
        return isSet;
    }

    public async Task<string?> GetTokenAsync(string key)
    {
        var redisResult = await _db.StringGetWithExpiryAsync(key);
        if (!redisResult.Value.IsNullOrEmpty && DateTime.Now.TimeOfDay < redisResult.Expiry)
        {
            return redisResult.Value.ToString();
        }
        await _db.KeyDeleteAsync("Token");
        return null;
    }
}