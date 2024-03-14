using CSharpFunctionalExtensions;
using Demo.Domain.Contracts;
using Demo.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Demo.Domain;

public interface IStatementManager
{
    public Task<Result<CreateStatementResponse,IError>> CreateStatement(CreateStatementRequest request);
    public Task<Result<List<GetAllStatementsByUserIDResponse>>> GetAllStatementsByUserId(Guid userId);
    public Task<Result<List<GetAllStatementsResponse>>> GetAllStatements(); 
    public Task<Result<Statement,IError>> UpdateStatementStatusById(UpdateStatementStatusRequest request);
}