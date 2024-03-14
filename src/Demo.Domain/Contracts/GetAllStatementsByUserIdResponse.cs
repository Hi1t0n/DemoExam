namespace Demo.Domain.Contracts;

public record GetAllStatementsByUserIDResponse(Guid StatementId, Guid UserId, string UserFullname, string CarNumber, string Description, string Status);