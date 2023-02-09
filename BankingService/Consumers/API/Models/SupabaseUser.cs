namespace API.Models;

public class SupabaseUser
{
    public SupabaseUser(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; }
    public string Password { get; }

}