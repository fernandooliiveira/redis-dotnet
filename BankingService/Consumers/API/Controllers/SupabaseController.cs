using System.Net;
using API.Models;
using API.Supabase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Supabase.Gotrue;

namespace API.Controllers;

[ApiController]
[Route("supabase")]
public class SupabaseController : Controller
{
    private readonly ISupabaseService _service;


    public SupabaseController(ISupabaseService service)
    {
        _service = service;
    }

    [HttpPost("/create")]
    public async Task<Session?> CreateUser(SupabaseUser user)
    {
        var us = await _service.CreateUser(user);
        return us;
    }
    
    [HttpPost("/login")]
    public async Task<Session?> LogIn(SupabaseUser user)
    {
        var us = await  _service.UserLogin(user);
        return us;
    }
    
    [HttpPost("/reset")]
    public async Task<string?> LogIn(string email)
    {
        var us = await  _service.ResetPassword(email);
        return us ? "Email send: " + email : "Error: Email " + email + " not found";
    }
    
    [HttpGet("/uuid")]
    [Authorize]
    public string GetUuidAuth()
    {
        return Guid.NewGuid().ToString();
    }
}