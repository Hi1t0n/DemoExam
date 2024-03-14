using CSharpFunctionalExtensions;
using Demo.Domain;
using Demo.Domain.Contracts;
using Demo.Domain.Models;
using Demo.Infrastructure.DbContext;
using Demo.Infrastructure.ErrorObject;
using Microsoft.EntityFrameworkCore;

namespace Demo.Infrastructure.Managers;

public class AuthManager : IAuthManager
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ICheckData _checkData;
    
    public AuthManager(ApplicationDbContext context, IPasswordHasher passwordHasher, ICheckData checkData)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _checkData = checkData;
    }
    
    /// <summary>
    /// Регистрация
    /// </summary>
    /// <param name="request">DTO с данными пользователя для регистрации</param>
    /// <returns>Возвращается результат и данные пользователя или объект ошибки с сообщением</returns>
    public async Task<Result<UserRegistrationResponse, IError>> Registration(UserRegistrationRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Login))
        {
            return Result.Failure<UserRegistrationResponse, IError>(new BadRequestError("Логин не может быть пустым или состоять из пробелов"));
        }
        
        if (await _checkData.CheckLogin(request.Login))
        {
            return Result.Failure<UserRegistrationResponse, IError>(new BadRequestError("Логин уже занят"));
        }
        
        if (string.IsNullOrWhiteSpace(request.Email))
        {
            return Result.Failure<UserRegistrationResponse, IError>(new BadRequestError("Почта не может быть пустой или состоять из пробелов"));
        }
    
        if (await _checkData.CheckEmail(request.Email))
        {
            return Result.Failure<UserRegistrationResponse, IError>(new BadRequestError("Почта уже занята"));
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            return Result.Failure<UserRegistrationResponse, IError>(new BadRequestError("Пароль не должен быть пустым или состоять из пробелов"));
        }

        if (request.Password.Length < 6)
        {
            return Result.Failure<UserRegistrationResponse, IError>(new BadRequestError("Длина пароля должна быть не менее 6 символов"));
        }

        if (string.IsNullOrWhiteSpace(request.PhoneNumber))
        {
            return Result.Failure<UserRegistrationResponse, IError>(new BadRequestError("Номер телефона не может быть пустым или состоять из пробелов"));
        }

        if (await _checkData.CheckPhoneNumber(request.PhoneNumber))
        {
            return Result.Failure<UserRegistrationResponse, IError>(new BadRequestError("Номер телефона уже занят"));
        }

        if (string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName))
        {
            return Result.Failure<UserRegistrationResponse, IError>(new BadRequestError("Имя или фамилия не могут быть пустыми или состоять из пробелов"));
        }

        var user = new User
        {
            UserId = new Guid(),
            Login = request.Login,
            Password = _passwordHasher.HashPassword(request.Password),
            Email = request.Email,
            Fullname = $"{request.FirstName} {request.LastName} {request.Patronymic}",
            PhoneNumber = request.PhoneNumber
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return Result.Success<UserRegistrationResponse, IError>(
            new UserRegistrationResponse(user.Login, user.Fullname, user.Email, user.PhoneNumber));
    }
    
    /// <summary>
    /// Вход
    /// </summary>
    /// <param name="request">DTO с данными для входа</param>
    /// <returns>Возвращается результат и данные пользователя или объект ошибки с сообщением</returns>
    public async Task<Result<UserLoginResponse, IError>> Login(UserLoginRequest request)
    {
        var user = await _context.Users.Where(u => u.Login == request.Login).Join(_context.Roles,
            user => user.RoleId,
            role => role.RoleId,
            (user, role) => new
            {
                user.UserId,
                user.Fullname,
                user.Password,
                role.RoleName
            }).FirstOrDefaultAsync();

        if (user is null)
        {
            return Result.Failure<UserLoginResponse, IError>(new UnauthorizedError("Неверный логин или пароль"));
        }

        if (!_passwordHasher.Verify(request.Password, user!.Password))
        {
            return Result.Failure<UserLoginResponse, IError>(new UnauthorizedError("Неверный логин или пароль"));
        }

        return Result.Success<UserLoginResponse, IError>(new UserLoginResponse(user.UserId, user.Fullname,
            user.RoleName));

    }
}