namespace Demo.Domain.Contracts;

public record UserRegistrationRequest(string Login, string Password, string FirstName, string LastName, string? Patronymic  ,string Email, string PhoneNumber);