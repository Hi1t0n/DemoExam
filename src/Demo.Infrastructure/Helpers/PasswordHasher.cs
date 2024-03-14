using Demo.Domain;
using System.Security.Cryptography;

namespace Demo.Infrastructure.Helpers;

public class PasswordHasher : IPasswordHasher
{
    /// <summary>
    /// Хэширование пароля
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <returns>Хэш пароля</returns>
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }
    
    /// <summary>
    /// Проверка пароля и хэша из бд
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <param name="hashedPassword">Хэш пароя</param>
    /// <returns>
    /// true - если пароли совпадают
    /// false - если пароли не совпадают
    /// </returns>
    public bool Verify(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }
}