using System.Runtime.InteropServices.JavaScript;
using Demo.Domain;
using Demo.Domain.Contracts;
using Demo.Infrastructure.ErrorObject;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Demo.Host.Endpoints;

public static class StatementEndpoints
{
    public static WebApplication AddStatementEndpoints(this WebApplication application)
    {
        var statementGroup = application.MapGroup("/api/statement");
        
        statementGroup.MapGet(pattern: "/", handler: GetAllStatements);
        statementGroup.MapGet(pattern: "/{userId:guid}", handler: GetAllStatementsByUserId);
        statementGroup.MapPost(pattern: "/", handler: CreateStatement);
        statementGroup.MapPut(pattern: "/", handler: UpdateStatementStatus);
        
        return application;
    }
    
    /// <summary>
    /// Метод Endpoint Get для получений всех заявлений по пути /api/statement/
    /// </summary>
    /// <param name="manager">Интерфейс менеджера с методами работы с бд</param>
    /// <returns>HTTP со статусом 200 и списком всех заявлений</returns>
    private static async Task<IResult> GetAllStatements(IStatementManager manager)
    {
        var result = await manager.GetAllStatements();

        return Results.Ok(result.Value);
    }
    
    /// <summary>
    /// Метод Endpoint Get для получений всех заявлений определенного пользователя по пути /api/statement/{userId}
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="manager">Интерфейс менеджера с методами работы с бд</param>
    /// <returns>HTTP со статусом 200 и списком всех заявлений</returns>
    private static async Task<IResult> GetAllStatementsByUserId(Guid userId, IStatementManager manager)
    {
        var result = await manager.GetAllStatementsByUserId(userId);
        
        return Results.Ok(result.Value);
    }
    
    /// <summary>
    /// Метод Endpoint Post для создания заявления по пути /api/statement/
    /// </summary>
    /// <param name="request">DTO с данными заявления</param>
    /// <param name="manager">Интерфейс менеджера с методами работы с бд</param>
    /// <returns>
    /// Если метод manager вернет ошибку, то вернется HTTP со статусом 400 BadRequest и сообщение об ошибке
    /// В случае успеха вернется HTTP со статусом 200 Ok и данные заявления
    /// </returns>
    private static async Task<IResult> CreateStatement(CreateStatementRequest request, IStatementManager manager)
    {
        var result = await manager.CreateStatement(request);

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
    /// Метод Endpoint Put для создания заявления по пути /api/statement/
    /// </summary>
    /// <param name="request">DTO с данными (id заявления и id статус кода)</param>
    /// <param name="manager">Интерфейс менеджера с методами работы с бд</param>
    /// <returns>
    /// В случае если возвращается ошибка из manager, то возвращаем HTTP со статусом 404 NotFound и сообщение об ошибке
    /// В случае успеха возвращается HTTP 200 Ok и обновленные данные
    /// </returns>
    private static async Task<IResult> UpdateStatementStatus(UpdateStatementStatusRequest request,
        IStatementManager manager)
    {
        var result = await manager.UpdateStatementStatusById(request);
        if (result.IsFailure)
        {
            if (result.Error is NotFoundError)
            {
                return Results.NotFound(new
                {
                    error = result.Error.ErrorMessage
                });
            }
        }

        return Results.Ok(result.Value);
    }
}