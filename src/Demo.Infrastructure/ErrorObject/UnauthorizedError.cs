using Demo.Domain;

namespace Demo.Infrastructure.ErrorObject;

public class UnauthorizedError : IError
{
    public string ErrorMessage { get; }

    public UnauthorizedError(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}