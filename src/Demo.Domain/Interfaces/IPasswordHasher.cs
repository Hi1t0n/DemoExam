namespace Demo.Domain;

public interface IPasswordHasher
{
    public string HashPassword(string password);
    public bool Verify(string password, string hashedPassword);
}