using CSharpFunctionalExtensions;
using Demo.Domain;
using Demo.Domain.Contracts;
using Demo.Infrastructure.ErrorObject;
using Microsoft.AspNetCore.Http.HttpResults;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Demo.Host.Endpoints;

public static class AuthEndpoints
{
    public static WebApplication AddAuthEndpoints(this WebApplication application)
    {
        var authGroup = application.MapGroup("/api/auth");

        authGroup.MapPost(pattern: "/register", handler: Registration);
        authGroup.MapPost(pattern: "/login", handler: Login);

        return application;
    }
    
    /// <summary>
    /// Регистрация пользователя доступна по пути /api/auth/register
    /// </summary>
    /// <param name="request">DTO с данными пользователя</param>
    /// <param name="manager">Интерфейс с методами работы с бд</param>
    /// <returns>
    /// В случае если данные введеные пользователем не соответствуют требованиям, то вернется HTTP со статусом 400 и сообщением об ошибке
    /// В случае успеха вернется HTTP со статусом 200 и данными пользователя 
    /// </returns>
    private static async Task<IResult> Registration(UserRegistrationRequest request, IAuthManager manager)
    {
        var result = await manager.Registration(request);

        if (result.IsFailure)
        {
            if (result.Error is BadRequestError)
            {
                return Results.BadRequest(new
                {
                    error = result.Error.ErrorMessage
                });
            }
        }

        return Results.Ok(result.Value);
    }
    
    /// <summary>
    /// Авторизация пользователя доступна по пути /api/auth/login
    /// </summary>
    /// <param name="request">DTO с данными пользователя(Login, Password)</param>
    /// <param name="manager">Интерфейс с методами работы с бд</param>
    /// <returns>
    /// В случае если пришли не верные данные пользователя, то возвращается HTTP со статусом 400 и сообщением об ошибке
    /// В случае успеха возвращаются данные пользователя и HTTP со статусом 200 
    /// </returns>
    private static async Task<IResult> Login(UserLoginRequest request, IAuthManager manager)
    {
        var result = await manager.Login(request);

        if (result.IsFailure)
        {
            if (result.Error is UnauthorizedError)
            {
                return Results.BadRequest(new
                {
                    error = result.Error.ErrorMessage
                });
            }
        }

        return Results.Ok(result.Value);
    }
}