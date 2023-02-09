using API.Models;
using Supabase.Interfaces;
using Supabase.Gotrue;
using Supabase.Realtime;
using Supabase.Storage;
using Client = Supabase.Client;
using Constants = Supabase.Gotrue.Constants;

namespace API.Supabase;

public class SupabaseService : ISupabaseService
{
    private readonly Client _client;
    private readonly ILogger<SupabaseService> _logger;

    public SupabaseService(Client client, ILogger<SupabaseService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<Session?> CreateUser(SupabaseUser user)
    {
        _logger.LogInformation("Create User:");
        var session = await _client.Auth.SignUp(
            user.Email, 
            user.Password
        );
        return session;
    }

    public async Task<Session?> UserLogin(SupabaseUser user)
    {
        _logger.LogInformation("Log In:");
        var session = await _client.Auth.SignIn(user.Email, user.Password);
        return session;
    }

    public async Task<bool> ResetPassword(string email)
    {
        _logger.LogInformation("Reset Password:");
        var confirm = await _client.Auth.ResetPasswordForEmail(email);
        return confirm;
    }

    public Task<SupabaseUser> UserLogOut(SupabaseUser user)
    {
        throw new NotImplementedException();
    }
}