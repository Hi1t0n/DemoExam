namespace Demo.Domain.Contracts;

public record UserLoginResponse(Guid UserId, string Fullname, string Role);