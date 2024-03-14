using CSharpFunctionalExtensions;
using Demo.Domain;
using Demo.Domain.Contracts;
using Demo.Domain.Models;
using Demo.Infrastructure.DbContext;
using Demo.Infrastructure.ErrorObject;
using Microsoft.EntityFrameworkCore;

namespace Demo.Infrastructure.Managers;

public class StatementManager : IStatementManager
{
    private readonly ApplicationDbContext _context;
    
    public StatementManager(ApplicationDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Создание заявления
    /// </summary>
    /// <param name="request">DTO с данными для заявления</param>
    /// <returns>Возвращается результат и данные  или объект ошибки с сообщением</returns>
    public async Task<Result<CreateStatementResponse,IError>> CreateStatement(CreateStatementRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.CarNumber))
        {
            return Result.Failure<CreateStatementResponse,IError>(new BadRequestError("Номер автомобиля не может быть пустым или состоять из пробелов"));
        }

        if (string.IsNullOrWhiteSpace(request.Description))
        {
            return Result.Failure<CreateStatementResponse,IError>(new BadRequestError("Описание не может быть пустым или состоять из пробелов"));
        }

        var statement = new Statement
        {
            StatementId = Guid.NewGuid(),
            CarNumber = request.CarNumber,
            Description = request.Description,
            UserId = request.UserId
        };

        await _context.Statements.AddAsync(statement);
        await _context.SaveChangesAsync();

        return Result.Success<CreateStatementResponse, IError>(new CreateStatementResponse(statement.StatementId, statement.CarNumber, statement.Description));
    }
    
    /// <summary>
    /// Получение всех заявлений пользователя
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns>Возвращаем результат и список с заявлениями</returns>
    public async Task<Result<List<GetAllStatementsByUserIDResponse>>> GetAllStatementsByUserId(Guid userId)
    {
        var statements = await _context.Statements.Where(s=> s.UserId == userId).Join(_context.Users,
                statement => statement.UserId,
                user => user.UserId,
                ((statement, user) => new { statement, user }))
            .Join(_context.Statuses,
                combine => combine.statement.StatusId,
                status => status.StatusId, (combine, status) => new GetAllStatementsByUserIDResponse(
                    combine.statement.StatementId,
                    combine.statement.UserId,
                    combine.user.Fullname,
                    combine.statement.CarNumber,
                    combine.statement.Description,
                    status.StatusName)).ToListAsync();
        
        return Result.Success<List<GetAllStatementsByUserIDResponse>>(statements);
    }
    
    /// <summary>
    /// Получение абсолютно всех заявлений
    /// </summary>
    /// <returns>Возвращаем результат и список всех заявлений</returns>
    public async Task<Result<List<GetAllStatementsResponse>>> GetAllStatements()
    {
        var statements = await _context.Statements.Join(_context.Users,
                statement => statement.UserId,
                user => user.UserId,
                ((statement, user) => new { statement, user }))
            .Join(_context.Statuses,
                combine => combine.statement.StatusId,
                status => status.StatusId, (combine, status) => new GetAllStatementsResponse(
                    combine.statement.StatementId,
                    combine.user.Fullname,
                    combine.statement.CarNumber,
                    combine.statement.Description,
                    status.StatusName)).ToListAsync();

        return Result.Success(statements);
    }
    
    /// <summary>
    /// Обновление статуса заявления по id
    /// </summary>
    /// <param name="request">DTO c данными для обновления (id заявления и id статуса)</param>
    /// <returns>Возвращаем результат и обновленное заявление</returns>
    public async Task<Result<Statement,IError>> UpdateStatementStatusById(UpdateStatementStatusRequest request)
    {
        var statement = await _context.Statements.FirstOrDefaultAsync(s => s.StatementId == request.StatementId);
        if (statement is null)
        {
            return Result.Failure<Statement,IError>(new NotFoundError("Заявление не найдено"));
        }

        statement.StatusId = request.StatusId;
        await _context.SaveChangesAsync();
        return Result.Success<Statement,IError>(statement);
    }
}