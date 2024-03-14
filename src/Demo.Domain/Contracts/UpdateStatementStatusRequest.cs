namespace Demo.Domain.Contracts;

public record UpdateStatementStatusRequest(Guid StatementId, Guid StatusId);