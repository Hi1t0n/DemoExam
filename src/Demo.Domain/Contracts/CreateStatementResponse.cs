namespace Demo.Domain.Contracts;

public record CreateStatementResponse(Guid StatementId, string CarNumber, string Description);