namespace Demo.Domain.Contracts;

public record CreateStatementRequest(Guid UserId, string CarNumber, string Description);