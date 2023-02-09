using System.Net;
using API.Cache;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("cache")]
public class CacheController : Controller
{
    private int _i = 0;
    private int private_ = 0;
    private readonly ICachingService _cache;

    public CacheController(ICachingService cache)
    {
        _cache = cache;
    }

    [HttpGet]
    public async Task<string> GetInCache()
    {
        Console.WriteLine("Cache");
        var cacheData = await _cache.GetTokenAsync("Token");
        if (cacheData != null)
        {
            return cacheData;
        }
        _i++;
        await _cache.SetTokenAsync("Token", _i.ToString());
        return _i.ToString();
    }
    
}