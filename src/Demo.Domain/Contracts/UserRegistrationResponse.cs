namespace Demo.Domain.Contracts;

public record UserRegistrationResponse(string Login, string FullName, string Email, string PhoneNumber);