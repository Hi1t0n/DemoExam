namespace Demo.Domain.Contracts;

public record GetAllStatementsResponse(Guid StatementId, string UserFullname, string CarNumber, string Description, string Status);