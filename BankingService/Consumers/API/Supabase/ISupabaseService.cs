using API.Models;
using Supabase.Gotrue;

namespace API.Supabase;

public interface ISupabaseService
{
    
    public Task<Session?> CreateUser(SupabaseUser user);
    
    public Task<Session?> UserLogin(SupabaseUser user);
    
    public Task<bool> ResetPassword(string email);
    
    public Task<SupabaseUser> UserLogOut(SupabaseUser user);
    
}