using System.ComponentModel.DataAnnotations;
using Demo.Domain;
using Demo.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Demo.Infrastructure.Helpers;

public class CheckData : ICheckData
{
    private readonly ApplicationDbContext _context;
    public CheckData(ApplicationDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Проверка существует ли введеный логин в базе данных
    /// </summary>
    /// <param name="login"></param>
    /// <returns>
    ///  true - если логин  существует
    ///  false - если логин не существует
    /// </returns>
    public async Task<bool> CheckLogin(string login)
    {
        var result = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        if (result is null)
        {
            return false;
        }
        return true;

    }
    
    /// <summary>
    /// Проверка существует ли введеный номер телефона в базе данных
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns>
    /// true - если телефон  найден
    ///  false - если телефон не найден
    /// </returns>
    public async Task<bool> CheckPhoneNumber(string phoneNumber)
    {
        var result = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);

        if (result is null)
        {
            return false;
        }

        return true;
    }
    
    /// <summary>
    /// проверка существует ли email в базе данных
    /// </summary>
    /// <param name="email"></param>
    /// <returns>
    /// true - если найден
    /// false - если не найден
    /// </returns>
    public async Task<bool> CheckEmail(string email)
    {
        var result = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (result is null)
        {
            return false;
        }

        return true;
    }
}