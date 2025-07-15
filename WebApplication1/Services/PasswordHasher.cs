using Isopoh.Cryptography.Argon2;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return Argon2.Hash(password);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return Argon2.Verify(hashedPassword, password);
    }
}